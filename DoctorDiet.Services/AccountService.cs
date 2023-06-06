using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Repository.UnitOfWork;

namespace DoctorDiet.Services
{
    public class AccountService
    {
        IUnitOfWork _UnitOfWork;
        IAccountRepository _accountRepository;

        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            _UnitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public void AddPatient(Patient Patient)
        {
            _accountRepository.AddPatient(Patient);
            _UnitOfWork.SaveChanges();
        }
        
        public void AddAdmin(Admin Admin)
        {
            _accountRepository.AddAdmin(Admin);
            _UnitOfWork.SaveChanges();
        }
        public void AddDoctor(Doctor Doctor)
        {
          _accountRepository.AddDoctor(Doctor);
            _UnitOfWork.SaveChanges();
        }
        
    }
}
