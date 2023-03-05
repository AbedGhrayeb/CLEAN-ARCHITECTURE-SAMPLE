using ErrorOr;

namespace BuberDinner.Domain.Common.Errors
{
    public static class Errors
    {
        public static class User
        {
            public static Error DublicateEmail => Error.Conflict(code: "User.DublicateEmail", description: "Email Already Exist!");
            public static Error InvalidCredential => Error.Conflict(code: "User.InvalidCredential", description: "Email or Password is Ivalid!");
        }
    }
}
