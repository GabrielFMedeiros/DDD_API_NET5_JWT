using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class User: IdentityUser
    {
        [Display(Name = "Nome/Razão Social")]
        [Required, Column("NAME_COMPANY")]
        public String NameCompany { get; set; }
        [Display(Name = "CPF/CNPJ")]
        [Column("CPF_CNPJ")]
        [Required, MaxLength(14)]
        public String CpfCnpj { get; set; }
        [Display(Name = "Idade")]
        [Column("AGE")]
        [Required, MaxLength(4)]
        public String Age { get; set; }
        [Display(Name = "Código Postal")]
        [Column("DDD")]
        [Required, MaxLength(3)]
        public String DDD { get; set; }
        [Display(Name = "Telefone")]
        [Column("PHONE")]
        [MaxLength(8)]
        public String Phone { get; set; }
        [Display(Name = "Celular")]
        [Column("CELLPHONE")]
        [Required, MaxLength(9)]
        public String CellPhone { get; set; }

        [Display(Name =  "Tipo de usuario")]
        [Column("ROLE")]
        public RoleEnum Role { get; set; }

    }
}
