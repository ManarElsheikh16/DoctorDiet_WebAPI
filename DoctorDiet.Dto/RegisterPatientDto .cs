using DoctorDiet.Dto;
using DoctorDiet.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorDiet.DTO
{
    public class RegisterPatientDto:RegisterAdminDto
    {
        public string? PatientId { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public DateTime BirthDate { get; set; }
        public string Diseases { get; set; }
        public List<string> noEats { get; set; }
        public List<string> ActivityRates { get; set; }
        public List<string> Goal { get; set; }

    }
}