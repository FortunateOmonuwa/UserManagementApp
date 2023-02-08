using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using NLog.Internal;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace UserManagementSystem
{
    public class UserManagementDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        // public ApartmentDBContext(DbContextOptions<ApartmentDBContext> dbContextOptions) : base(dbContextOptions) { }

        public UserManagementDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("jsconfig1.json");

            try
            {
                var configuration = builder.Build();
                if (configuration.GetConnectionString("DefaultConnection") == null)
                {
                    throw new InvalidOperationException("The connection string was not found in the configuration file.");
                }
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The configuration file was not found: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("The connection string was not initialized: " + ex.Message);
            }

            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //var configuration = builder.Build();


           
        }


    }
}
