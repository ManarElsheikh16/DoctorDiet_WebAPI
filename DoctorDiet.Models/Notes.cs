using DoctorDiet.Models.Interface;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorDiet.Models
{
    public class Notes : IBaseModel<int>
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        [ForeignKey("Day")]
        public int DayId { get; set; }
        public string Text { get; set; }
        public virtual Day Day { get; set; }
        public virtual Doctor Doctor { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}