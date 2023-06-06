using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : Controller
    {
        private readonly PlanService _planService;
        private readonly IUnitOfWork _unitOfWork;

        public PlanController(PlanService planService, IUnitOfWork unitOfWork)
        {
            _planService = planService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllPlans()
        {
            var plans = _planService.GetAllPlans();
            _unitOfWork.CommitChanges();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanById(int id)
        {
            var plan = _planService.GetPlanById(id);
            if (plan == null)
                return NotFound();

            _unitOfWork.CommitChanges();
            return Ok(plan);
        }

        [HttpPost]
        public IActionResult AddPlan(Plan plan)
        {
            var addedPlan = _planService.AddPlan(plan);
            _unitOfWork.CommitChanges();
            return Ok(addedPlan);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlan(int id, Plan plan)
        {
            if (id != plan.Id)
                return BadRequest();

            _planService.UpdatePlan(plan);
            _unitOfWork.CommitChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlan(int id)
        {
            _planService.DeletePlan(id);
            _unitOfWork.CommitChanges();
            return NoContent();
        }
    }
}
