using DoctorDiet.Models;
using System.ComponentModel.DataAnnotations;

namespace DoctorDiet.DTO
{
    public class RegisterDoctorDto: RegisterAdminDto
    {
        public string Specialization { get; set; }
        public string Location { get; set; }

        public List<ContactInfo> ContactInfo { get; set; }


    }
}