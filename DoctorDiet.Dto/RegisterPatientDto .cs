﻿using DoctorDiet.Models;
using System.ComponentModel.DataAnnotations;

namespace DoctorDiet.DTO
{
    public class RegisterPatientDto:RegisterAdminDto
    {

        public string Gender { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
      
        public List<Goal> Goal { get; set; }

        public DateTime BirthDate { get; set; }
        public string Diseases { get; set; }
        public List<NoEat> noEats { get; set; }
        public List<ActivityRate> ActivityRates { get; set; }

    }
}