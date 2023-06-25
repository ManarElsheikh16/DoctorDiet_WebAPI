using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Services
{
  public class ContactInfoService
  {
    IGenericRepository<ContactInfo,int> _repository;
    IUnitOfWork _unitOfWork;
    IMapper _mapper;

    public ContactInfoService(IGenericRepository<ContactInfo,int> repository
        , IUnitOfWork unitOfWork,IMapper mapper)
    {
      _repository = repository;
      _unitOfWork = unitOfWork;
    }
    public ContactInfo Add(string contactInfo,string doctorid)
    {
      ContactInfo contactinfo = new ContactInfo();
      contactinfo.DoctorId = doctorid;
      contactinfo.contactInfo= contactInfo;


      ContactInfo contact = _repository.Add(contactinfo);

      _unitOfWork.SaveChanges();
      return contact;

    }
  }
}
