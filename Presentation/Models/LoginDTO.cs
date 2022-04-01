using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class LoginDTO
    {
        public int? Role { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
