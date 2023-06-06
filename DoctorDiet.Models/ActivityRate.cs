using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Models
{
    public class ActivityRate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Patient")]
        public string PatientID { get; set; }

        public virtual Patient Patient { get; set; }

    }
} 
