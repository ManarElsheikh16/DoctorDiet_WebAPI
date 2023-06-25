using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
  public class PatientDTO
  {
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public DateTime BirthDate { get; set; }
    public string Diseases { get; set; }
    public int? MaxCalories { get; set; }
    public int? MinCalories { get; set; }
    public byte[] ProfileImage { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public List<CustomPlanDTO> CustomPlans { get; set; }
    public List<NoEatDTO>? NoEat { get; set; }
    public string Goal { get; set; }
    public List<DoctorPatientBridgeDTO> DoctorPatientBridge { get; set; }
    public string? ActivityRates { get; set; }

  }
}
