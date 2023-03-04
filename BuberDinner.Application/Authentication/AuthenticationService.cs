using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;

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

        public AuthenticationResult Login(string email, string password)
        {
            //check if user exist
            var user = _userRepository.GetUserByEmail(email);
            if (user is not User)
                throw new ArgumentException($"user with email {email} not exist");
            if (user.Password != password)
                throw new ArgumentException($"Invalid Password");

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //check if user already exist
            var existUuser = _userRepository.GetUserByEmail(email);
            if (existUuser is User)
                throw new Exception($"User whith {email} email already exist");
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