using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Models
{
    public class CustomPlan: IBaseModel<int>
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public int CaloriesFrom { get; set; }
        public int CaloriesTo { get; set; }
        public virtual List<Day> Days { get; set; }
        public virtual Patient Patient { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }


    }
}
