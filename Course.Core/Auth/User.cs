using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Auth
{
    public record User(
    int Id,
    string Username,
    string Name,
    string Email,
    string Password,
    string[] Roles
        
    );
}
