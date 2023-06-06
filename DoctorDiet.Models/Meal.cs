using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Models
{
    public class Meal:IBaseModel<int>
    {

        public int Id { get; set; }
        [ForeignKey("CustomPlan")]
        public int MealId { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public CustomPlan CustomPlan { get; set; }
        public virtual List<PlanMealBridge> PlanMealBridges { get; set; }

        public byte[] Image { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
