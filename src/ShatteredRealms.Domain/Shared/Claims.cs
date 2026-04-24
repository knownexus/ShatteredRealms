namespace ShatteredRealms.Domain.Shared;

public static class Claims
{
    public static class Roles
    {
        public const string SystemId         = "00000000-0000-0000-0000-000000000001";
        public const string AdminId          = "00000000-0000-0000-0000-000000000002";
        public const string EventOrganizerId = "00000000-0000-0000-0000-000000000003";
        public const string UserId           = "00000000-0000-0000-0000-000000000004";
        public const string AnalystId        = "00000000-0000-0000-0000-000000000005";
        public const string UnverifiedId     = "00000000-0000-0000-0000-000000000007";

        public const string SystemName         = "System";
        public const string AdminName          = "Admin";
        public const string AnalystName            = "Analyst";
        public const string EventOrganizerName = "EventOrganizer";
        public const string UserName           = "User";
        public const string UnverifiedName     = "Unverified";

        public const string SystemDescription         = "Internal system role, unrestricted";
        public const string AdminDescription          = "Full administrative access";
        public const string AnalystDescription        = "Report Access";
        public const string EventOrganizerDescription = "Can manage events, characters, assign positions/experience";
        public const string UserDescription           = "Standard registered user";
        public const string UnverifiedDescription     = "Newly registered user awaiting admin approval";
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
            public const string Approve   = "Users.Approve";
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

        public static class ActivityLog
        {
            public const string View =   "ActivityLog.View";
            public const string Update = "ActivityLog.Update";
            public const string Delete = "ActivityLog.Delete";
        }

        public static class Reports
        {
            public const string View = "Reports.View";
            public const string Create = "Reports.Create";
            public const string CreateAll = "Reports.CreateAll";
        }

        public static class Events
        {
            public const string View            = "Events.View";
            public const string Create          = "Events.Create";
            public const string Update          = "Events.Update";
            public const string Delete          = "Events.Delete";
            public const string Register        = "Events.Register";
            public const string ManageAttendees = "Events.ManageAttendees";
        }

        public static class Announcements
        {
            public const string View = "Announcements.View";
            public const string Create = "Announcements.Create";
            public const string Update = "Announcements.Update";
            public const string Delete = "Announcements.Delete";
        }

        public static class Documents
        {
            public const string View   = "Documents.View";
            public const string Upload = "Documents.Upload";
            public const string Delete = "Documents.Delete";
        }

        public static class PermissionControl
        {
            public const string View   = "PermissionControl.View";
            public const string Assign = "PermissionControl.Assign";
        }

        public static class Videos
        {
            public const string View = "Videos.View";
            public const string Create = "Videos.Create";
            public const string Update = "Videos.Update";
            public const string Delete = "Videos.Delete";
            public const string DeleteOwn = "Videos.DeleteOwn";
            public const string Approve = "Videos.Approve";
        }

        public static class Forum
        {
            public static class Category
            {
                public const string Create = "Forum.Category.Create";
                public const string Update = "Forum.Category.Update";
                public const string Delete = "Forum.Category.Delete";
            }

            public static class Thread
            {
                public const string Lock      = "Forum.Thread.Lock";
                public const string Create    = "Forum.Thread.Create";
                public const string Update    = "Forum.Thread.Update";
                public const string Delete    = "Forum.Thread.Delete";
                public const string Pin       = "Forum.Thread.Pin";
                public const string UpdateOwn = "Forum.Thread.UpdateOwn";
                public const string DeleteOwn = "Forum.Thread.DeleteOwn";
            }

            public static class Post
            {
                public const string Create    = "Forum.Post.Create";
                public const string Update    = "Forum.Post.Update";
                public const string Delete    = "Forum.Post.Delete";
                public const string UpdateOwn = "Forum.Post.UpdateOwn";
                public const string DeleteOwn = "Forum.Post.DeleteOwn";
            }
        }

        public static class Wiki
        {
            public static class Category
            {
                public const string Manage = "Wiki.Category.Manage";
            }

            public static class Page
            {
                public const string Create    = "Wiki.Page.Create";
                public const string Update    = "Wiki.Page.Update";
                public const string Delete    = "Wiki.Page.Delete";
                public const string UpdateOwn = "Wiki.Page.UpdateOwn";
                public const string DeleteOwn = "Wiki.Page.DeleteOwn";
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
          // index 44 - 49 - Videos
        , new PermissionDef(Permissions.Videos.View,     "View video",          "Videos")
        , new PermissionDef(Permissions.Videos.Create,   "Upload video",        "Videos")
        , new PermissionDef(Permissions.Videos.Update,   "Update video details","Videos")
        , new PermissionDef(Permissions.Videos.Delete,   "Delete video",        "Videos")
        , new PermissionDef(Permissions.Videos.DeleteOwn,"Delete own video",    "Videos")
        , new PermissionDef(Permissions.Videos.Approve,  "Approve video",       "Videos")
          // Index 50 - 52 - Activity Logs
        , new PermissionDef(Permissions.ActivityLog.View,    "View activity logs",   "ActivityLogs")
        , new PermissionDef(Permissions.ActivityLog.Update,  "Edit activity logs",   "ActivityLogs")
        , new PermissionDef(Permissions.ActivityLog.Delete,  "Delete activity logs", "ActivityLogs")
          // Index 53 - 55 - Reports
        , new PermissionDef(Permissions.Reports.View,      "View Reports",             "ActivityLogs")
        , new PermissionDef(Permissions.Reports.Create,    "Generate specific report", "ActivityLogs")
        , new PermissionDef(Permissions.Reports.CreateAll, "Generate all reports",     "ActivityLogs")
          // Index 56 - 59 - Announcements
        , new PermissionDef(Permissions.Announcements.View, "View announcements",           "Announcements")
        , new PermissionDef(Permissions.Announcements.Create, "Create a new announcements", "Announcements")
        , new PermissionDef(Permissions.Announcements.Update, "Edit an announcement",       "Announcements")
        , new PermissionDef(Permissions.Announcements.Delete, "Delete an announcement",     "Announcements")
          // Index 60 - Users (extended)
        , new PermissionDef(Permissions.Users.Approve, "Approve a pending user registration", "Users")
          // Index 61 - 66 - Events
        , new PermissionDef(Permissions.Events.View,            "View events",                    "Events")
        , new PermissionDef(Permissions.Events.Create,          "Create events",                  "Events")
        , new PermissionDef(Permissions.Events.Update,          "Edit any event",                 "Events")
        , new PermissionDef(Permissions.Events.Delete,          "Delete any event",               "Events")
        , new PermissionDef(Permissions.Events.Register,        "Mark self as going to an event", "Events")
        , new PermissionDef(Permissions.Events.ManageAttendees, "Remove attendees from an event", "Events")
          // Index 67 - 69 - Documents
        , new PermissionDef(Permissions.Documents.View,   "View and download documents", "Documents")
        , new PermissionDef(Permissions.Documents.Upload, "Upload documents",            "Documents")
        , new PermissionDef(Permissions.Documents.Delete, "Delete documents",            "Documents")
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
          , Permissions.Announcements.View
          , Permissions.Announcements.Create
          , Permissions.Announcements.Update
          , Permissions.Announcements.Delete
          , Permissions.Videos.View
          , Permissions.Videos.Create
          , Permissions.Videos.Update
          , Permissions.Videos.Delete
          , Permissions.Videos.Approve
          , Permissions.Events.View
          , Permissions.Events.Create
          , Permissions.Events.Update
          , Permissions.Events.Delete
          , Permissions.Events.Register
          , Permissions.Events.ManageAttendees
          , Permissions.Documents.View
          , Permissions.Documents.Upload
          , Permissions.Documents.Delete
        };

        public static readonly IReadOnlyList<string> Analyst = new[]
        {
              Permissions.Reports.View
            , Permissions.Reports.Create
            , Permissions.Reports.CreateAll
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
          , Permissions.Videos.View
          , Permissions.Videos.Create
          , Permissions.Videos.DeleteOwn
          , Permissions.Announcements.View
          , Permissions.Events.View
          , Permissions.Events.Register
          , Permissions.Documents.View
        };

        public static readonly IReadOnlyList<string> Unverified = new[]
        {
            Permissions.Users.ViewOwn
          , Permissions.Users.UpdateOwn
        };
    }
}