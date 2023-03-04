using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication
{
    public record AuthenticationResult(User user,string Token);
}