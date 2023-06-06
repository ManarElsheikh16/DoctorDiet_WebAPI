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
    public class Plan:IBaseModel<int>
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int CaloriesFrom { get; set; }
        public int CaloriesTo { get; set; }
        public int Duration { get; set; }
        public virtual List<Day> Days { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual List<PlanMealBridge> PlanMealBridges { get; set; }
        [ForeignKey("NoEat")]
        public int NoeatId { get; set; }
        public virtual NoEat NoEat { get; set; }
  

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
