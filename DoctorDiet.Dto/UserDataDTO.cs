using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class UserDataDTO
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public DateTime BirthDate { get; set; }

        public int MaxCalories { get; set; }
        public int MinCalories { get; set; }
  }
}
