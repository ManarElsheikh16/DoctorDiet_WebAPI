using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class GoalDTO
    {
        public string Name { get; set; }

        public string? PatientId { get; set; }
    }
}
