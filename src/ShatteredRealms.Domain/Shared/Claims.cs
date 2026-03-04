namespace ShatteredRealms.Domain.Shared;

public static class Claims
{
    public static class Roles
    {
        public const string SystemId         = "00000000-0000-0000-0000-000000000001";
        public const string AdminId          = "00000000-0000-0000-0000-000000000002";
        public const string EventOrganizerId = "00000000-0000-0000-0000-000000000003";
        public const string UserId           = "00000000-0000-0000-0000-000000000004";

        public const string SystemName        = "System";
        public const string AdminName         = "Admin";
        public const string EventOrganizerName = "EventOrganizer";
        public const string UserName          = "User";

        public const string SystemDescription         = "Internal system role, unrestricted";
        public const string AdminDescription          = "Full administrative access";
        public const string EventOrganizerDescription = "Can manage characters, assign positions/experience";
        public const string UserDescription           = "Standard registered user";
    }

    // Constants are now ClaimValue strings - human-readable and stable.
    // RolePermissions below references these directly, so it needs no changes.
    public static class Permissions
    {
        public static class Users
        {
            public const string ViewOwn   = "Users.ViewOwn";
            public const string UpdateOwn = "Users.UpdateOwn";
            public const string View      = "Users.View";
            public const string Create    = "Users.Create";
            public const string Update    = "Users.Update";
            public const string Delete    = "Users.Delete";
        }

        public static class Characters
        {
            public const string ViewOwn          = "Characters.ViewOwn";
            public const string CreateOwn        = "Characters.CreateOwn";
            public const string Create           = "Characters.Create";
            public const string UpdateOwn        = "Characters.UpdateOwn";
            public const string View             = "Characters.View";
            public const string Update           = "Characters.Update";
            public const string Delete           = "Characters.Delete";
            public const string DeleteOwn        = "Characters.DeleteOwn";
            public const string AssignPosition   = "Characters.AssignPosition";
            public const string AssignExperience = "Characters.AssignExperience";
        }

        public static class Roles
        {
            public const string View   = "Role.View";
            public const string Create = "Role.Create";
            public const string Update = "Role.Update";
            public const string Delete = "Role.Delete";
            public const string Assign = "Role.Assign";
        }

        public static class PermissionControl
        {
            public const string View   = "PermissionControl.View";
            public const string Assign = "PermissionControl.Assign";
        }

        public static class Forum
        {
            public static class Category
            {
                public const string Create = "forum.category.create";
                public const string Update = "forum.category.edit";
                public const string Delete = "forum.category.delete";
            }

            public static class Thread
            {
                public const string Lock      = "forum.thread.lock";
                public const string Create    = "forum.thread.create";
                public const string Update    = "forum.thread.update";
                public const string Delete    = "forum.thread.delete";
                public const string Pin       = "forum.thread.pin";
                public const string UpdateOwn = "forum.thread.updateOwn";
                public const string DeleteOwn = "forum.thread.deleteOwn";
            }

            public static class Post
            {
                public const string Create    = "forum.post.create";
                public const string Update    = "forum.post.edit";
                public const string Delete    = "forum.post.delete";
                public const string UpdateOwn = "forum.post.editOwn";
                public const string DeleteOwn = "forum.post.deleteOwn";
            }
        }

        public static class Wiki
        {
            public static class Category
            {
                public const string Manage = "wiki.category.manage";
            }

            public static class Page
            {
                public const string Create    = "wiki.page.create";
                public const string Update    = "wiki.page.edit";
                public const string Delete    = "wiki.page.delete";
                public const string UpdateOwn = "wiki.page.editOwn";
                public const string DeleteOwn = "wiki.page.deleteOwn";
            }

        }
    }

    public sealed record PermissionDef(string ClaimValue, string Description, string Category)
    {
        public string Name => ClaimValue;
    }

    /// <summary>
    /// Single source of truth for all permissions that exist in the system.
    /// NEVER reorder or remove entries - only ever append.
    /// The 1-based index is used to compute stable database IDs.
    /// </summary>
    public static readonly IReadOnlyList<PermissionDef> PermissionCatalog = new[]
    {
          // index 0-5  - Users
          new PermissionDef(Permissions.Users.Create,                "Create users",                      "Users")
        , new PermissionDef(Permissions.Users.ViewOwn,               "View own profile",                  "Users")
        , new PermissionDef(Permissions.Users.View,                  "View any user",                     "Users")
        , new PermissionDef(Permissions.Users.UpdateOwn,             "Update own profile",                "Users")
        , new PermissionDef(Permissions.Users.Update,                "Update any user",                   "Users")
        , new PermissionDef(Permissions.Users.Delete,                "Delete users",                      "Users")
          // index 6-15 - Character
        , new PermissionDef(Permissions.Characters.CreateOwn,        "Create own characters",             "Characters")
        , new PermissionDef(Permissions.Characters.Create,           "Create character",                  "Characters")
        , new PermissionDef(Permissions.Characters.ViewOwn,          "View own characters",               "Characters")
        , new PermissionDef(Permissions.Characters.View,             "View any character",                "Characters")
        , new PermissionDef(Permissions.Characters.UpdateOwn,        "Update own character name/nation",  "Characters")
        , new PermissionDef(Permissions.Characters.Update,           "Update any character",              "Characters")
        , new PermissionDef(Permissions.Characters.Delete,           "Delete any character",              "Characters")
        , new PermissionDef(Permissions.Characters.DeleteOwn,        "Delete own character",              "Characters")
        , new PermissionDef(Permissions.Characters.AssignPosition,   "Assign societal/military position", "Characters")
        , new PermissionDef(Permissions.Characters.AssignExperience, "Assign experience/level",           "Characters")
          // index 16-20 - Roles
        , new PermissionDef(Permissions.Roles.Create,                "Create roles",                      "Role")
        , new PermissionDef(Permissions.Roles.View,                  "View roles",                        "Role")
        , new PermissionDef(Permissions.Roles.Update,                "Update roles",                      "Role")
        , new PermissionDef(Permissions.Roles.Delete,                "Delete roles",                      "Role")
        , new PermissionDef(Permissions.Roles.Assign,                "Assign roles to users",             "Role")
          // index 21-22 - PermissionControl
        , new PermissionDef(Permissions.PermissionControl.View,      "View permissions",                  "PermissionControl")
        , new PermissionDef(Permissions.PermissionControl.Assign,    "Assign permissions to roles",       "PermissionControl")
          // index 23-37 - Forum
        , new PermissionDef(Permissions.Forum.Category.Create,  "Create forum categories",  "Forum")
        , new PermissionDef(Permissions.Forum.Category.Update,  "Update forum categories",  "Forum")
        , new PermissionDef(Permissions.Forum.Category.Delete,  "Delete forum categories",  "Forum")
        , new PermissionDef(Permissions.Forum.Thread.Create,    "Create forum threads",     "Forum")
        , new PermissionDef(Permissions.Forum.Thread.UpdateOwn, "Update own forum thread",  "Forum")
        , new PermissionDef(Permissions.Forum.Thread.Update,    "Update any forum thread",  "Forum")
        , new PermissionDef(Permissions.Forum.Thread.DeleteOwn, "Delete own forum thread",  "Forum")
        , new PermissionDef(Permissions.Forum.Thread.Delete,    "Delete any forum thread",  "Forum")
        , new PermissionDef(Permissions.Forum.Thread.Lock,      "Lock/unlock forum threads","Forum")
        , new PermissionDef(Permissions.Forum.Thread.Pin,       "Pin/unpin forum threads",  "Forum")
        , new PermissionDef(Permissions.Forum.Post.Create,      "Create posts in threads",  "Forum")
        , new PermissionDef(Permissions.Forum.Post.UpdateOwn,   "Update own forum post",    "Forum")
        , new PermissionDef(Permissions.Forum.Post.Update,      "Update any forum post",    "Forum")
        , new PermissionDef(Permissions.Forum.Post.DeleteOwn,   "Delete own forum post",    "Forum")
        , new PermissionDef(Permissions.Forum.Post.Delete,      "Delete any forum post",    "Forum")
          // index 38-43 - Wiki
        , new PermissionDef(Permissions.Wiki.Page.Create,    "Create wiki pages",            "Wiki")
        , new PermissionDef(Permissions.Wiki.Page.UpdateOwn, "Update own wiki page",         "Wiki")
        , new PermissionDef(Permissions.Wiki.Page.Update,    "Update any wiki page",         "Wiki")
        , new PermissionDef(Permissions.Wiki.Page.DeleteOwn, "Delete own wiki pages",        "Wiki")
        , new PermissionDef(Permissions.Wiki.Page.Delete,    "Delete wiki pages",            "Wiki")
        , new PermissionDef(Permissions.Wiki.Category.Manage,"Create/update wiki categories","Wiki")
    };

    public static class RolePermissions
    {
        private static readonly string[] All =
            PermissionCatalog.Select(p => p.ClaimValue).ToArray();

        public static readonly IReadOnlyList<string> System = All;
        public static readonly IReadOnlyList<string> Admin  = All;

        public static readonly IReadOnlyList<string> EventOrganizer = new[]
        {
            Permissions.Users.ViewOwn
          , Permissions.Users.UpdateOwn
          , Permissions.Characters.ViewOwn
          , Permissions.Characters.CreateOwn
          , Permissions.Characters.UpdateOwn
          , Permissions.Characters.DeleteOwn
          , Permissions.Characters.View
          , Permissions.Characters.Update
          , Permissions.Characters.AssignPosition
          , Permissions.Characters.AssignExperience
          , Permissions.Forum.Thread.Create
          , Permissions.Forum.Thread.UpdateOwn
          , Permissions.Forum.Thread.DeleteOwn
          , Permissions.Forum.Thread.Lock
          , Permissions.Forum.Thread.Pin
          , Permissions.Forum.Post.Create
          , Permissions.Forum.Post.UpdateOwn
          , Permissions.Forum.Post.DeleteOwn
          , Permissions.Wiki.Page.Create
          , Permissions.Wiki.Page.UpdateOwn
          , Permissions.Wiki.Page.DeleteOwn
        };

        public static readonly IReadOnlyList<string> User = new[]
        {
            Permissions.Users.ViewOwn
          , Permissions.Users.UpdateOwn
          , Permissions.Characters.ViewOwn
          , Permissions.Characters.CreateOwn
          , Permissions.Characters.UpdateOwn
          , Permissions.Characters.DeleteOwn
          , Permissions.Forum.Thread.Create
          , Permissions.Forum.Thread.UpdateOwn
          , Permissions.Forum.Post.Create
          , Permissions.Forum.Post.UpdateOwn
          , Permissions.Forum.Post.DeleteOwn
          , Permissions.Wiki.Page.Create
          , Permissions.Wiki.Page.UpdateOwn
          , Permissions.Wiki.Page.DeleteOwn
        };
    }
}