﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Models
{
    public class Patient : IBaseModel<string>
    {

        public Patient() { }

        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string? Subscribed { get; set; }
        public List<Goal> Goal { get; set; }
        public DateTime BirthDate { get; set; }
        public string Diseases { get; set; }
        public int? Calories { get; set; }

        //[ForeignKey("Doctor")]
        //public string DoctorId { get; set; }
        //public virtual Doctor Doctor { get; set; }


        public virtual List<NoEat> ?NoEat { get; set; }
        //not eat will handel at backend  contain string
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<CustomPlan>? CustomPlans { get; set; }
        public virtual List<ActivityRate>? ActivityRates { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }


    }
}
