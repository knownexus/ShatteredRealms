namespace ShatteredRealms.Domain.Entities.Telemetry;

public enum TelemetryEventType
{
    // Auth
    UserLoggedIn       = 1,
    UserLoggedOut      = 2,
    UserRegistered     = 3,

    // Users
    UserCreated        = 10,
    UserUpdated        = 11,
    UserDeleted        = 12,
    UserApproved       = 13,

    // Roles & Permissions
    RoleCreated        = 20,
    RoleUpdated        = 21,
    RoleDeleted        = 22,
    PermissionsUpdated = 23,

    // Characters
    CharacterCreated          = 30,
    CharacterUpdated          = 31,
    CharacterDeleted          = 32,
    CharacterXpAssigned       = 33,
    CharacterPositionAssigned = 34,

    // Wiki
    WikiPageCreated     = 40,
    WikiPageUpdated     = 41,
    WikiPageDeleted     = 42,
    WikiCategoryCreated = 43,

    // Forum
    ForumCategoryCreated = 50,
    ForumCategoryUpdated = 51,
    ForumCategoryDeleted = 52,
    ForumThreadCreated   = 53,
    ForumThreadUpdated   = 54,
    ForumThreadDeleted   = 55,
    ForumPostCreated     = 56,
    ForumPostUpdated     = 57,
    ForumPostDeleted     = 58,

    // Events
    EventCreated              = 70,
    EventUpdated              = 71,
    EventDeleted              = 72,
    EventRegistered           = 73,
    EventRegistrationCancelled = 74,

    // Announcements
    AnnouncementCreated = 80,
    AnnouncementUpdated = 81,
    AnnouncementDeleted = 82,
}
