using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class UserDTO
    {
        public String NameCompany { get; set; }
        public String CpfCnpj { get; set; }
        public String Age { get; set; }
        public String DDD { get; set; }
        public String Phone { get; set; }
        public String CellPhone { get; set; }
        public int Role { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
