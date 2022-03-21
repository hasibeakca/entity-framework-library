using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Context
{
   public class KutuphaneDbContext : DbContext //PAKETI KULLANABILMEK ICIN INDIRDIK K.DAL A GİR MANAGE NU GET BROWSER
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
            optionsBuilder.UseSqlServer("Server=LAPTOP-M227QJH7\\SQLEXPRESS;Database=KutuphaneDB;Trusted_Connection=True;");
        }
        public DbSet<Book> Books{ get; set; }
        public DbSet<Category>  Categories{ get; set; }
        public  DbSet<SubCategory>  SubCategories { get; set; }
    }
}
