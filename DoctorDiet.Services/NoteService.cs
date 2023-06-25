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
    public class NoteService
    {
        IGenericRepository<Notes,int> _notesRepository;
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public NoteService(IGenericRepository<Notes, int> notesRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _notesRepository= notesRepository;
            _unitOfWork= unitOfWork;
            _mapper= mapper;
        }

        public Notes AddNote(NoteCreateDto noteCreateDto) 
        {
            Notes note=_mapper.Map<Notes>(noteCreateDto);
            _notesRepository.Add(note);
            _unitOfWork.SaveChanges();

            return note;

        }

        public IEnumerable<Notes> GetAllNotesByDocID(string docID)
        {
           IEnumerable<Notes> notes= _notesRepository.Get(note=>note.DoctorId==docID).ToList();
           
            return notes;
        }

        public Notes GetNoteByID(int noteID)
        {
            Notes note=_notesRepository.GetByID(noteID);

            return note;
        }

        public IEnumerable<Notes> GetAllNotesByDayID(int dayID)
        {
            IEnumerable<Notes> notes = _notesRepository.Get(note => note.DayId == dayID).ToList();

            return notes;
        }

        public void updateNote(int noteID,UpdateNoteDto updateNoteDto, params string[] updatedProp)
        {

            Notes note = GetNoteByID(noteID);
            note.Text= updateNoteDto.Text;
            _notesRepository.Update(note, updatedProp);
            _unitOfWork.SaveChanges();
        }

        public void DeleteNote(int noteID)
        {
            _notesRepository.Delete(noteID);
            _unitOfWork.SaveChanges();

        }
    }
}
