using DoctorDiet.Models.Interface;
using System.ComponentModel;

namespace DoctorDiet.Models
{
    public class NoEat:IBaseModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
