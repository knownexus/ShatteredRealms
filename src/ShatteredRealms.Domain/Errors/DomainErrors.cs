using System.Net;
using ShatteredRealms.Domain.Shared;

namespace ShatteredRealms.Domain.Errors;

public static class DomainErrors
{
    public static class Member
    {
        public static readonly Error EmailAlreadyInUse = new(
            "Member.EmailAlreadyInUse",
            "The specified email is already in use",
            (int)HttpStatusCode.Conflict);

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "Member.NotFound",
            $"The member with the identifier {id} was not found.",
            (int)HttpStatusCode.NotFound);

        public static readonly Error InvalidCredentials = new(
            "Member.InvalidCredentials",
            "The provided credentials are invalid",
            (int)HttpStatusCode.Unauthorized);
    }

    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty",
            (int)HttpStatusCode.BadRequest);

        public static readonly Error TooLong = new(
            "Email.TooLong",
            "Email is too long",
            (int)HttpStatusCode.BadRequest);

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid",
            (int)HttpStatusCode.BadRequest);
    }

    public static class Password
    {
        public static readonly Error Empty = new("Password.Empty",
                                                 "Password is empty",
                                                 (int)HttpStatusCode.BadRequest);

        public static readonly Error TooLong = new("Password.TooLong",
                                                   "Password is too long",
                                                   (int)HttpStatusCode.BadRequest);

        public static readonly Error TooShort = new("Password.TooShort",
                                                   "Password is too short",
                                                   (int)HttpStatusCode.BadRequest);

        public static readonly Error RequireDigit = new("Password.RequireDigit",
                                                        "Password requires numerical digits",
                                                        (int)HttpStatusCode.BadRequest);

        public static readonly Error RequireLowercase = new("Password.RequireLowercase",
                                                        "Password requires lowercase characters",
                                                        (int)HttpStatusCode.BadRequest);

        public static readonly Error RequireUppercase = new ("Password.RequireUppercase",
                                                         "Password requires upper case characters",
                                                         (int)HttpStatusCode.BadRequest);

        public static readonly Error RequireNonAlphanumeric = new ("Password.RequireNonAlphanumeric",
                                                           "Password requires non-alphanumeric characters",
                                                           (int)HttpStatusCode.BadRequest);
    }

    public static class PhoneNumber
    {
        public static readonly Error Empty = new(
                                                 "PhoneNumber.Empty",
                                                 "PhoneNumber is empty",
                                                 (int)HttpStatusCode.BadRequest);

        public static readonly Error TooLong = new(
                                                   "PhoneNumber.TooLong",
                                                   "PhoneNumber is too long",
                                                   (int)HttpStatusCode.BadRequest);

        public static readonly Error TooShort = new(
                                                   "PhoneNumber.TooShort",
                                                   "PhoneNumber is too short",
                                                   (int)HttpStatusCode.BadRequest);

        public static readonly Error InvalidFormat = new(
                                                         "PhoneNumber.InvalidFormat",
                                                         "PhoneNumber format is invalid",
                                                         (int)HttpStatusCode.BadRequest);
    }

    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name is empty",
            (int)HttpStatusCode.BadRequest);

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "FirstName name is too long",
            (int)HttpStatusCode.BadRequest);
    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name is empty",
            (int)HttpStatusCode.BadRequest);

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            "Last name is too long",
            (int)HttpStatusCode.BadRequest);
    }

    public static class Permissions
    {
        public static readonly Error AlreadyExists = new(
                                                 "Permissions.AlreadyExists",
                                                 "This permission already exists",
                                                 (int)HttpStatusCode.Conflict);

        public static readonly Error NoPermissions = new(
                                                   "Permissions.NoPermissions",
                                                   "No permissions were returned",
                                                   (int)HttpStatusCode.NoContent);

    }

    public static class Authentication
    {
        public static readonly Error AlreadyExists = new(
                                                         "Authentication.AlreadyExists",
                                                         "User with this email already exists",
                                                         (int)HttpStatusCode.Conflict);

        public static readonly Error InvalidCredentials = new(
                                                              "Authentication.InvalidCredentials",
                                                              "Username or password were incorrect",
                                                              (int)HttpStatusCode.Unauthorized);

        public static readonly Error InvalidRefreshToken = new(
                                                               "Authentication.InvalidRefreshToken",
                                                               "Refresh Token was not valid",
                                                               (int)HttpStatusCode.Unauthorized);

        public static readonly Error EmailNotConfirmed = new(
                                                             "Authentication.EmailNotConfirmed",
                                                             "Please confirm your email address before signing in",
                                                             (int)HttpStatusCode.Forbidden);

        public static readonly Error NoRoles = new(
                                                   "Authentication.NoRoles",
                                                   "No Roles were returned",
                                                   (int)HttpStatusCode.NoContent);

    }

    public static class User
    {
        public static readonly Error AlreadyExists = new(
                                                         "User.AlreadyExists",
                                                         "User with this email already exists",
                                                         (int)HttpStatusCode.Conflict);

        public static readonly Error NotFound = new(
                                                     "User.NotFound",
                                                     "No User found with this email",
                                                     (int)HttpStatusCode.NotFound);

        public static readonly Error InvalidCredentials = new(
                                                              "User.InvalidCredentials",
                                                              "Username or password were incorrect",
                                                              (int)HttpStatusCode.Unauthorized);

        public static readonly Error RolesNotUpdated = new(
                                                   "User.RolesNotUpdated",
                                                   "No Roles were updated for user",
                                                   (int)HttpStatusCode.NotModified);

        public static readonly Error CannotDeleteSystemUser = new(
                                                                  "User.CannotDeleteSystemUser",
                                                                  "System user cannot be deleted",
                                                                  (int)HttpStatusCode.Forbidden);

        public static readonly Error CannotModifySystemUser = new(
                                                                  "User.CannotModifySystemUser",
                                                                  "System user cannot be changed",
                                                                  (int)HttpStatusCode.Forbidden);
    }

    public static class Role
    {
        public static readonly Error AlreadyExists = new(
                                                         "Role.AlreadyExists",
                                                         "Role with this name already exists",
                                                         (int)HttpStatusCode.Conflict);

        public static readonly Error NotFound = new(
                                                    "Role.NotFound",
                                                    "No Role found with this id",
                                                    (int)HttpStatusCode.NotFound);


        public static readonly Error RolesNotUpdated = new(
                                                           "Role.RoleNotUpdated",
                                                           "No Role was updated",
                                                           (int)HttpStatusCode.NotModified);

        public static readonly Error CannotDeleteSystemRole = new(
                                                                  "Role.CannotDeleteSystemRole",
                                                                  "System role cannot be deleted",
                                                                  (int)HttpStatusCode.Forbidden);
        public static readonly Error CannotModifySystemRole = new(
                                                                  "Role.CannotModifySystemRole",
                                                                  "System role cannot be changed",
                                                                  (int)HttpStatusCode.Forbidden);

        public static readonly Error CannotAssignSystemRole = new(
                                                                  "Role.CannotAssignSystemRole",
                                                                  "System role cannot be assigned",
                                                                  (int)HttpStatusCode.Forbidden);
    }

    public static class Forum
    {
        public static readonly Error CategoryNotFound = new("Forum.CategoryNotFound"
                                                          , "Category not found"
                                                          , (int)HttpStatusCode.NotFound);
        public static readonly Error ThreadNotFound = new("Forum.ThreadNotFound"
                                                        , "Thread not found"
                                                        , (int)HttpStatusCode.NotFound);
        public static readonly Error PostNotFound = new("Forum.PostNotFound"
                                                      , "Post not found"
                                                      , (int)HttpStatusCode.NotFound);
        public static readonly Error ThreadLocked = new("Forum.ThreadLocked"
                                                      , "This thread is locked"
                                                      , (int)HttpStatusCode.Forbidden);

        public static readonly Error CannotEditOthers = new("Forum.CannotEditOthers"
                                                          , "You cannot edit others' posts"
                                                          , (int)HttpStatusCode.Forbidden);
    }

    public static class Wiki
    {
        public static readonly Error PageNotFound = new("Wiki.PageNotFound"
                                                      , "Wiki page not found"
                                                      , (int)HttpStatusCode.NotFound);

        public static readonly Error SlugAlreadyExists = new("Wiki.SlugAlreadyExists"
                                                           , "A page with that title already exists"
                                                           , (int)HttpStatusCode.Conflict);

        public static readonly Error CannotEditOthers = new("Wiki.CannotEditOthers"
                                                          , "You cannot edit others' wiki pages"
                                                          , (int)HttpStatusCode.Forbidden);

        public static readonly Error PageCreationFailed = new("Wiki.PageCreationFailed"
                                                           , "Failed to create wiki page"
                                                           , (int)HttpStatusCode.InternalServerError);
    }
}
