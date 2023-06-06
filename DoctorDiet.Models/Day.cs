using DoctorDiet.Models.Interface;
using System.ComponentModel;

namespace DoctorDiet.Models
{
    public class Day:IBaseModel<int>  {
        public int Id { get; set; }
        public virtual List<Category> Categories { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
