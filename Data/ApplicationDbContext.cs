using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication12.Models;

namespace WebApplication12.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication12.Models.Ljubimac> Ljubimac { get; set; }
        public DbSet<WebApplication12.Models.PostSkloništa> PostSkloništa { get; set; }
        public DbSet<WebApplication12.Models.PostUdruge> PostUdruge { get; set; }
        public DbSet<WebApplication12.Models.Posvajatelj> Posvajatelj { get; set; }
        public DbSet<WebApplication12.Models.Sklonište> Sklonište { get; set; }
        public DbSet<WebApplication12.Models.Udruga> Udruga { get; set; }
    }
}
