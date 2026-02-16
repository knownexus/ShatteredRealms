using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.Forum;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Entities.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<
        User,
        Role,
        string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        Permission,
        IdentityUserToken<string>
    >(options)
{
    public DbSet<RefreshToken>         RefreshToken         { get; set; }
    public DbSet<EmergencyContact>     EmergencyContact     { get; set; }
    public DbSet<UserEmergencyContact> UserEmergencyContact { get; set; }
    public DbSet<ForumCategory> ForumCategory { get; set; }
    public DbSet<ForumThread> ForumThread { get; set; }
    public DbSet<ForumPost> ForumPost { get; set; }
    public DbSet<WikiPage> WikiPage { get; set; }
    public DbSet<WikiRevision> WikiRevision { get; set; }
    public DbSet<WikiCategory> WikiCategory { get; set; }
    public DbSet<WikiPageCategory> WikiPageCategory { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Role>(e =>
        {
            e.ToTable("Role");
            e.HasMany(r => r.UserRoles)
             .WithOne(ur => ur.Role)
             .HasForeignKey(ur => ur.RoleId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(r => r.Permissions)
             .WithOne()
             .HasForeignKey(p => p.RoleId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Permission>(e =>
        {
            e.ToTable("AspNetRoleClaims");
            e.Property(p => p.Description).HasMaxLength(512);
            e.Property(p => p.Category).HasMaxLength(128);
        });

        builder.Entity<UserRole>(e =>
        {
            e.ToTable("AspNetUserRoles");
            e.HasOne(ur => ur.User)
             .WithMany(u => u.UserRoles)
             .HasForeignKey(ur => ur.UserId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ur => ur.Role)
             .WithMany(r => r.UserRoles)
             .HasForeignKey(ur => ur.RoleId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<RefreshToken>(e =>
        {
            e.ToTable("RefreshToken");
            e.HasKey(rt => rt.Id);
            e.HasOne(rt => rt.User)
             .WithMany(u => u.RefreshTokens)
             .HasForeignKey(rt => rt.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<EmergencyContact>(e =>
        {
            e.ToTable("EmergencyContact");
            e.HasKey(ec => ec.Id);
            e.Property(ec => ec.FirstName).IsRequired();
            e.Property(ec => ec.LastName).IsRequired();
            e.Property(ec => ec.EmailAddress).IsRequired();
            e.Property(ec => ec.PhoneNumber).IsRequired();
        });

        builder.Entity<UserEmergencyContact>(e =>
        {
            e.ToTable("UserEmergencyContact");
            e.HasKey(uec => new { uec.UserId, uec.EmergencyContactId });
            e.HasOne(uec => uec.User)
             .WithMany(u => u.UserEmergencyContact)
             .HasForeignKey(uec => uec.UserId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(uec => uec.EmergencyContact)
             .WithMany(ec => ec.UserEmergencyContact)
             .HasForeignKey(uec => uec.EmergencyContactId)
             .OnDelete(DeleteBehavior.Cascade);
        });
        ConfigureForum(builder);
        ConfigureWiki(builder);

        SeedRoles(builder);
        SeedPermissions(builder);
    }

    private static void ConfigureForum(ModelBuilder builder)
    {
        builder.Entity<ForumCategory>(e =>
        {
            e.ToTable("ForumCategory");
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired().HasMaxLength(128);
            e.Property(c => c.Description).HasMaxLength(512);
            e.HasOne(c => c.CreatedBy)
             .WithMany()
             .HasForeignKey(c => c.CreatedById)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(c => c.Threads)
             .WithOne(t => t.Category)
             .HasForeignKey(t => t.CategoryId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ForumThread>(e =>
        {
            e.ToTable("ForumThread");
            e.HasKey(t => t.Id);
            e.Property(t => t.Title).IsRequired().HasMaxLength(256);
            e.HasOne(t => t.Author)
             .WithMany()
             .HasForeignKey(t => t.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(t => t.Posts)
             .WithOne(p => p.Thread)
             .HasForeignKey(p => p.ThreadId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasQueryFilter(t => !t.IsDeleted);
        });

        builder.Entity<ForumPost>(e =>
        {
            e.ToTable("ForumPost");
            e.HasKey(p => p.Id);
            e.Property(p => p.Content).IsRequired();
            e.HasOne(p => p.Author)
             .WithMany()
             .HasForeignKey(p => p.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasQueryFilter(p => !p.IsDeleted);
        });
    }

    // -------------------------------------------------------------------------
    // Wiki
    // -------------------------------------------------------------------------

    private static void ConfigureWiki(ModelBuilder builder)
    {
        builder.Entity<WikiPage>(e =>
        {
            e.ToTable("WikiPage");
            e.HasKey(p => p.Id);
            e.Property(p => p.Title).IsRequired().HasMaxLength(256);
            e.Property(p => p.Slug).IsRequired().HasMaxLength(256);
            e.HasIndex(p => p.Slug).IsUnique();
            e.Property(p => p.CurrentContent).IsRequired();
            e.HasOne(p => p.Author)
             .WithMany()
             .HasForeignKey(p => p.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(p => p.Revisions)
             .WithOne(r => r.Page)
             .HasForeignKey(r => r.WikiPageId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasQueryFilter(p => !p.IsDeleted);
        });

        builder.Entity<WikiRevision>(e =>
        {
            e.ToTable("WikiRevision");
            e.HasKey(r => r.Id);
            e.Property(r => r.Content).IsRequired();
            e.Property(r => r.RevisionNote).HasMaxLength(512);
            e.HasOne(r => r.Editor)
             .WithMany()
             .HasForeignKey(r => r.EditorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<WikiCategory>(e =>
        {
            e.ToTable("WikiCategory");
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired().HasMaxLength(128);
            e.Property(c => c.Description).HasMaxLength(512);
        });

        builder.Entity<WikiPageCategory>(e =>
        {
            e.ToTable("WikiPageCategory");
            e.HasKey(wpc => new { wpc.WikiPageId, wpc.WikiCategoryId });
            e.HasOne(wpc => wpc.Page)
             .WithMany(p => p.Categories)
             .HasForeignKey(wpc => wpc.WikiPageId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(wpc => wpc.Category)
             .WithMany(c => c.Pages)
             .HasForeignKey(wpc => wpc.WikiCategoryId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }


    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
            new Role { Id = Claims.Roles.SystemId,        Name = Claims.Roles.SystemName,        NormalizedName = Claims.Roles.SystemName.ToUpper(),        Description = Claims.Roles.SystemDescription,        Priority = 100, IsSystem = true  },
            new Role { Id = Claims.Roles.AdminId,         Name = Claims.Roles.AdminName,         NormalizedName = Claims.Roles.AdminName.ToUpper(),         Description = Claims.Roles.AdminDescription,         Priority = 90,  IsSystem = true  },
            new Role { Id = Claims.Roles.EventOrganizerId,Name = Claims.Roles.EventOrganizerName,NormalizedName = Claims.Roles.EventOrganizerName.ToUpper(),Description = Claims.Roles.EventOrganizerDescription,Priority = 50,  IsSystem = false },
            new Role { Id = Claims.Roles.UserId,          Name = Claims.Roles.UserName,          NormalizedName = Claims.Roles.UserName.ToUpper(),          Description = Claims.Roles.UserDescription,          Priority = 10,  IsSystem = false }
        );
    }

    private static void SeedPermissions(ModelBuilder builder)
    {
        var catalog = Claims.PermissionCatalog
            .Select((def, i) => (def, catalogIndex: i + 1))
            .ToDictionary(x => x.def.ClaimValue);

        // Each role gets its own offset band so IDs never collide.
        // Band size of 100 supports up to 100 permissions per role - widen if needed.
        var roleMappings = new (string RoleId, IReadOnlyList<string> ClaimValues, int IdOffset)[]
        {
            (Claims.Roles.SystemId,        Claims.RolePermissions.System,         IdOffset: 100),
            (Claims.Roles.AdminId,         Claims.RolePermissions.Admin,          IdOffset: 200),
            (Claims.Roles.EventOrganizerId,Claims.RolePermissions.EventOrganizer, IdOffset: 300),
            (Claims.Roles.UserId,          Claims.RolePermissions.User,           IdOffset: 400),
        };

        var rows = roleMappings
            .SelectMany(role => role.ClaimValues.Select(claimValue =>
            {
                var entry = catalog[claimValue]; // throws if a typo slips in - good!
                return new Permission
                {
                    // Stable ID = roleOffset + 1-based catalog index
                    Id          = role.IdOffset + entry.catalogIndex,
                    RoleId      = role.RoleId,
                    ClaimType   = "permission",
                    ClaimValue  = claimValue,
                    Description = entry.def.Description,
                    Category    = entry.def.Category
                };
            }))
            .ToArray();

        builder.Entity<Permission>().HasData(rows);
    }
}