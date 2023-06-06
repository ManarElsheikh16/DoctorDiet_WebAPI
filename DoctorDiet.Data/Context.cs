using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiet.Models;
using DoctorDiet.Models.Interface;
using DoctorDiet.Data.Extentions;

namespace DoctorDiet.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {


        public Context() { }
        public Context(DbContextOptions dbContextOptions) : base(dbContextOptions) { }


        public DbSet<Admin> Admin { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Day> Day { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<NoEat> NoEat { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<CustomPlan> CustomPlans { get; set; }
        public DbSet<Plan> Plans { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<CustomPlanMealBridge> customPlanMealBridges { get; set; }
        public DbSet<PlanMealBridge> planMealBridges { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyGlobalFilter<IBaseModel<int>>(x => !x.IsDeleted);

            modelBuilder.ApplyGlobalFilter<IBaseModel<string>>(x => !x.IsDeleted);

        }

    }
}
