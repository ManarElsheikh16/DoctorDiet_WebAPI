﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class DoctorNotesDTO
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public int DayId { get; set; }
        public string Text { get; set; }
    }
}
