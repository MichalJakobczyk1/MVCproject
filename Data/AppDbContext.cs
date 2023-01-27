using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCproject.Models;
using System.Security.Cryptography;
using System.Xml;

namespace MVCproject.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RegularCustomer> RegularCustomers { get; set; }
    }
}
