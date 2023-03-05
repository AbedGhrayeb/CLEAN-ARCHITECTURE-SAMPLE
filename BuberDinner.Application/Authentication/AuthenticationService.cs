using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //check if user exist
            var user = _userRepository.GetUserByEmail(email);
            if (user is not User)
                return Errors.User.InvalidCredential;
            if (user.Password != password)
                return Errors.User.InvalidCredential;

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //check if user already exist
            var existUuser = _userRepository.GetUserByEmail(email);
            if (existUuser is User)
                return Errors.User.DublicateEmail;
            //ceate new user (generate userId)
            var user = new User { FirstName = firstName, LastName = lastName, Email = email, Password = password };
            //add user to list
            _userRepository.Add(user);
            //create jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}