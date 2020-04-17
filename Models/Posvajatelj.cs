using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication12.Models
{
    public class Posvajatelj
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Ime { get; set; }

        public String Prezime { get; set; }

        public String Mjesto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datum { get; set; }
        public String BrMob { get; set; }
        public String Email { get; set; }

        public int LjubimacId { get; set; }
        public Ljubimac Ljubimac { get; set; }
    }
}
