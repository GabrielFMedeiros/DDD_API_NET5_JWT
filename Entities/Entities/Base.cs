using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Base
    {
        [Display(Name = "Código")]
        [Column("ID")]
        public int ID { get; set; }

        [Display(Name =  "Nome")]
        [Column("NAME")]
        public String NAME { get; set; }
    }
}
