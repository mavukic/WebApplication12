using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication12.Models
{
    public class Ljubimac
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Ime { get; set; }

        public string Opis { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datum { get; set; }
        public int? SkloništeId { get; set; }
        public Sklonište Sklonište { get; set; }
        [DisplayName("Lokacija životinje")]
        public String Mjesto { get; set; }

        public String Vrsta { get; set; }

        public int Godine { get; set; }



        public string Slika { get; set; }
    }
}
