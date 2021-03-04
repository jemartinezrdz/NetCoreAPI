using ApiExamen.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace ApiExamen.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Recibo> Recibo { get; set; }
    }
}
