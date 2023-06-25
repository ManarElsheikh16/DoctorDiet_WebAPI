using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class GetPatientNotesDTO
    {
        public string patientId { get; set; }
        public int dayId { get; set; }
    }
}
