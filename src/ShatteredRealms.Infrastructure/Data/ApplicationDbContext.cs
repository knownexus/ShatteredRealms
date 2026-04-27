using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShatteredRealms.Domain.Entities;
using ShatteredRealms.Domain.Entities.ActivityLog;
using ShatteredRealms.Domain.Entities.Announcement;
using ShatteredRealms.Domain.Entities.Document;
using ShatteredRealms.Domain.Entities.Event;
using ShatteredRealms.Domain.Entities.Character;
using ShatteredRealms.Domain.Entities.Forum;
using ShatteredRealms.Domain.Entities.Telemetry;
using ShatteredRealms.Domain.Entities.User;
using ShatteredRealms.Domain.Entities.Wiki;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<
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
    public DbSet<RefreshToken> RefreshToken { get; set; }
    public DbSet<ActivityLog> ActivityLog { get; set; }
    public DbSet<EmergencyContact> EmergencyContact { get; set; }
    public DbSet<UserEmergencyContact> UserEmergencyContact { get; set; }
    public DbSet<ForumCategory> ForumCategory { get; set; }
    public DbSet<ForumThread> ForumThread { get; set; }
    public DbSet<ForumPost> ForumPost { get; set; }
    public DbSet<WikiPage> WikiPage { get; set; }
    public DbSet<WikiRevision> WikiRevision { get; set; }
    public DbSet<WikiCategory> WikiCategory { get; set; }
    public DbSet<WikiPageCategory> WikiPageCategory { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<EventAttendee> EventAttendee { get; set; }
    public DbSet<Announcement> Announcement { get; set; }
    public DbSet<Document> Document { get; set; }
    public DbSet<Character> Character { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<TelemetryEvent> TelemetryEvent { get; set; }
    public DbSet<AnalyticsFlagRule> AnalyticsFlagRule { get; set; }
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

        builder.Entity<ActivityLog>(e =>
        {
            e.ToTable("ActivityLog");
            e.HasKey(al => al.Id);
            e.Property(al => al.Description).HasMaxLength(1024);
            e.HasOne(al => al.User)
             .WithMany(u => u.ActivityLogs)
             .HasForeignKey(al => al.UserId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(al => al.Character)
             .WithMany()
             .HasForeignKey(al => al.CharacterId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.NoAction);
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
        ConfigureEvents(builder);
        ConfigureAnnouncements(builder);
        ConfigureDocuments(builder);
        ConfigureCharacters(builder);
        ConfigurePositions(builder);
        ConfigureTelemetry(builder);

        SeedRoles(builder);
        SeedPermissions(builder);
        SeedPositions(builder);
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


    private static void ConfigureEvents(ModelBuilder builder)
    {
        builder.Entity<Event>(e =>
        {
            e.ToTable("Event");
            e.HasKey(ev => ev.Id);
            e.Property(ev => ev.Title).IsRequired().HasMaxLength(256);
            e.Property(ev => ev.Description).IsRequired();
            e.Property(ev => ev.BannerImagePath).HasMaxLength(512);
            e.Property(ev => ev.Location).HasMaxLength(512);
            e.HasOne(ev => ev.CreatedBy)
             .WithMany()
             .HasForeignKey(ev => ev.CreatedById)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasMany(ev => ev.Attendees)
             .WithOne(a => a.Event)
             .HasForeignKey(a => a.EventId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasQueryFilter(ev => !ev.IsDeleted);
        });

        builder.Entity<EventAttendee>(e =>
        {
            e.ToTable("EventAttendee");
            e.HasKey(a => new { a.EventId, a.UserId });
            e.HasOne(a => a.User)
             .WithMany()
             .HasForeignKey(a => a.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureAnnouncements(ModelBuilder builder)
    {
        builder.Entity<Announcement>(e =>
        {
            e.ToTable("Announcement");
            e.HasKey(a => a.Id);
            e.Property(a => a.Title).IsRequired().HasMaxLength(256);
            e.Property(a => a.Body).IsRequired();
            e.HasOne(a => a.Author)
             .WithMany()
             .HasForeignKey(a => a.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(a => a.LinkedEvent)
             .WithMany()
             .HasForeignKey(a => a.LinkedEventId)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(false);
            e.HasQueryFilter(a => !a.IsDeleted);
        });
    }

    private static void ConfigureDocuments(ModelBuilder builder)
    {
        builder.Entity<Document>(e =>
        {
            e.ToTable("Document");
            e.HasKey(d => d.Id);
            e.Property(d => d.OriginalFileName).IsRequired().HasMaxLength(512);
            e.Property(d => d.StoredFileName).IsRequired().HasMaxLength(512);
            e.Property(d => d.ContentType).IsRequired().HasMaxLength(256);
            e.HasOne(d => d.UploadedBy)
             .WithMany()
             .HasForeignKey(d => d.UploadedById)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasQueryFilter(d => !d.IsDeleted);
        });
    }

    private static void ConfigureCharacters(ModelBuilder builder)
    {
        builder.Entity<Character>(e =>
        {
            e.ToTable("Character");
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired().HasMaxLength(128);
            e.Property(c => c.Nationality).IsRequired().HasMaxLength(128);
            e.Property(c => c.Faction).IsRequired().HasMaxLength(128);
            e.HasOne(c => c.Owner)
             .WithMany()
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(c => c.Position)
             .WithMany()
             .HasForeignKey(c => c.PositionId)
             .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private static void ConfigurePositions(ModelBuilder builder)
    {
        builder.Entity<Position>(e =>
        {
            e.ToTable("Position");
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).IsRequired().HasMaxLength(64);
            e.Property(p => p.Description).HasMaxLength(256);
        });
    }

    private static void ConfigureTelemetry(ModelBuilder builder)
    {
        builder.Entity<TelemetryEvent>(e =>
        {
            e.ToTable("TelemetryEvent");
            e.HasKey(t => t.Id);
            e.Property(t => t.ActorId).IsRequired().HasMaxLength(450);
            e.Property(t => t.ActorName).IsRequired().HasMaxLength(256);
            e.Property(t => t.ActorEmail).IsRequired().HasMaxLength(256);
            e.Property(t => t.ActorRole).IsRequired().HasMaxLength(128);
            e.Property(t => t.TargetId).HasMaxLength(450);
            e.Property(t => t.TargetName).HasMaxLength(512);
            e.Property(t => t.Details).HasMaxLength(1024);
            e.Property(t => t.FlagReason).HasMaxLength(512);
            e.Property(t => t.FlaggedById).HasMaxLength(450);
            e.Property(t => t.EventType).IsRequired();
            e.HasIndex(t => t.OccurredAt);
            e.HasIndex(t => t.ActorId);
            e.HasIndex(t => t.ActorName);
            e.HasIndex(t => t.EventType);
            e.HasIndex(t => t.IsFlagged);
        });

        builder.Entity<AnalyticsFlagRule>(e =>
        {
            e.ToTable("AnalyticsFlagRule");
            e.HasKey(r => r.Id);
            e.Property(r => r.Reason).IsRequired().HasMaxLength(512);
            e.Property(r => r.CreatedById).IsRequired().HasMaxLength(450);
            e.Property(r => r.TargetUserId).HasMaxLength(450);
            e.Property(r => r.ActorRole).HasMaxLength(128);
            e.HasIndex(r => r.RuleType);
            e.HasIndex(r => r.IsActive);
        });
    }

    private static void SeedPositions(ModelBuilder builder)
    {
        builder.Entity<Position>().HasData(
            new Position { Id =  1, Name = "Commoner",        Description = "An ordinary member of society with no special rank" },
            new Position { Id =  2, Name = "Freeman",         Description = "A free man with basic rights, not bound to a lord" },
            new Position { Id =  3, Name = "Merchant",        Description = "A trader dealing in goods and services" },
            new Position { Id =  4, Name = "Craftsman",       Description = "A skilled worker or artisan" },
            new Position { Id =  5, Name = "Monk",            Description = "A member of a religious order" },
            new Position { Id =  6, Name = "Priest",          Description = "A member of the clergy" },
            new Position { Id =  7, Name = "Man-at-Arms",     Description = "A professional soldier in service to a lord" },
            new Position { Id =  8, Name = "Archer",          Description = "A ranged combatant specialising in the bow" },
            new Position { Id =  9, Name = "Sergeant",        Description = "A mid-ranking military officer" },
            new Position { Id = 10, Name = "Squire",          Description = "A knight's attendant in training for knighthood" },
            new Position { Id = 11, Name = "Knight",          Description = "A mounted warrior granted a rank of honour" },
            new Position { Id = 12, Name = "Captain",         Description = "A commander of a military unit" },
            new Position { Id = 13, Name = "Steward",         Description = "An official responsible for managing an estate" },
            new Position { Id = 14, Name = "Sheriff",         Description = "A royal officer responsible for law and order" },
            new Position { Id = 15, Name = "Baron",           Description = "A member of the lowest order of the nobility" }
        );
    }

    private static void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(
            new Role { Id = Claims.Roles.SystemId,         Name = Claims.Roles.SystemName,         NormalizedName = Claims.Roles.SystemName.ToUpper(),         ConcurrencyStamp = "b1000000-0000-0000-0000-000000000001", Description = Claims.Roles.SystemDescription,         Priority = 100, IsSystem = true  },
            new Role { Id = Claims.Roles.AdminId,          Name = Claims.Roles.AdminName,          NormalizedName = Claims.Roles.AdminName.ToUpper(),          ConcurrencyStamp = "b1000000-0000-0000-0000-000000000002", Description = Claims.Roles.AdminDescription,          Priority = 90,  IsSystem = false },
            new Role { Id = Claims.Roles.AnalystId,        Name = Claims.Roles.AnalystName,        NormalizedName = Claims.Roles.AnalystName.ToUpper(),        ConcurrencyStamp = "b1000000-0000-0000-0000-000000000005", Description = Claims.Roles.AnalystDescription,        Priority = 80,  IsSystem = false },
            new Role { Id = Claims.Roles.EventOrganizerId, Name = Claims.Roles.EventOrganizerName, NormalizedName = Claims.Roles.EventOrganizerName.ToUpper(), ConcurrencyStamp = "b1000000-0000-0000-0000-000000000003", Description = Claims.Roles.EventOrganizerDescription, Priority = 50,  IsSystem = false },
            new Role { Id = Claims.Roles.UserId,           Name = Claims.Roles.UserName,           NormalizedName = Claims.Roles.UserName.ToUpper(),           ConcurrencyStamp = "b1000000-0000-0000-0000-000000000004", Description = Claims.Roles.UserDescription,           Priority = 10,  IsSystem = false },
            new Role { Id = Claims.Roles.UnverifiedId,     Name = Claims.Roles.UnverifiedName,     NormalizedName = Claims.Roles.UnverifiedName.ToUpper(),     ConcurrencyStamp = "b1000000-0000-0000-0000-000000000007", Description = Claims.Roles.UnverifiedDescription,     Priority = 5,   IsSystem = false }
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
            (Claims.Roles.AnalystId,       Claims.RolePermissions.Analyst,        IdOffset: 300),
            (Claims.Roles.EventOrganizerId,Claims.RolePermissions.EventOrganizer, IdOffset: 400),
            (Claims.Roles.UserId,          Claims.RolePermissions.User,           IdOffset: 500),
            (Claims.Roles.UnverifiedId,    Claims.RolePermissions.Unverified,     IdOffset: 600),
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