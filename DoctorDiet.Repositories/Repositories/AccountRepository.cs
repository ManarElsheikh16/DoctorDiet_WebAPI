using DoctorDiet.Data;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Reposetories
{
    public class AccountRepository:IAccountRepository
    {
        Context _context;

        public AccountRepository(Context context)
        {
            _context = context;

        }
        public void AddAdmin(Admin Admin)
        {
            _context.Admin.Add(Admin);

        }

        public void AddPatient(Patient Patient)
        {
            _context.Patient.Add(Patient);

        }

        public void AddDoctor(Doctor Doctors)
        {
            _context.Doctors.Add(Doctors);

        }

    }
}
