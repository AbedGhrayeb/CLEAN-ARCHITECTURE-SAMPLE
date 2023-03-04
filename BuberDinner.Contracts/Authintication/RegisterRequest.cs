using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Contracts.Authintication
{
    public record RegisterRequest(string FirstName,string LastName,string Email,string Password);

}