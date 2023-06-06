using DoctorDiet.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Models
{
    public class ActivityRate:IBaseModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
} 
