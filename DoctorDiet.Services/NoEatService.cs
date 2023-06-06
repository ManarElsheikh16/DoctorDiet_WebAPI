using AutoMapper;
using DoctorDiet.DTO;
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
    public class NoEatService
    {

        IGenericRepository<NoEat, int> _noEatsRepository;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public NoEatService(IGenericRepository<NoEat, int> noeatsRepository, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _noEatsRepository = noeatsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public NoEat AddNoEat(NoEat noEatData)
        {
            NoEat noEat = _mapper.Map<NoEat>(noEatData);
            _noEatsRepository.Add(noEat);
            _unitOfWork.SaveChanges();

            return noEat;

        }







    }
}
