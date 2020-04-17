using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication12.Models
{
    public class Sklonište
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Naziv { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Tel { get; set; }
        public string Mail { get; set; }
        public string Web { get; set; }
        public ICollection<PostSkloništa> PostsSkloništa { get; set; }
        public ICollection<Ljubimac> Ljubimac { get; set; }
    }
}
