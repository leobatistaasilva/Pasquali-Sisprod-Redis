using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;
using Pasquali.Sisprods.Domain.Entities;
using Pasquali.Sisprods.Infra.Data.Configuration.Mappings;
using Pasquali.Sisprods.Infra.Data.Configuration.Seeds;

namespace Pasquali.Sisprods.Infra.Data.Contexts
{
    public class SisprodsContext : DbContext
    {

        public SisprodsContext() : base("SisprodsDbConnection")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Client>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Cart>();

            //modelBuilder.Configurations.Add(new ClientMap());

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("InstructorID")
            //        .ToTable("CourseInstructor"));
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
