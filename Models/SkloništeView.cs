using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication12.Models
{
    public class SkloništeView
    {
        public IEnumerable<Sklonište> Skloništa { get; set; }
        public IEnumerable<PostSkloništa> PostSkloništa { get; set; }
    }
}
