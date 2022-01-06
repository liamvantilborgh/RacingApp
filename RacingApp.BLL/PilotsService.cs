using AutoMapper;
using RacingApp.Core.DTO_S;
using RacingApp.DAL;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.BLL
{
    public class PilotsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PilotsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<PilotsDTO> GetAll()
        {
            var pilots = _mapper.Map<IEnumerable<Pilots>, IEnumerable<PilotsDTO>>(_unitOfWork.Pilots.GetAll());
            return pilots;
        }

        public PilotsDTO GetById(int id)
        {
            var pilot = _unitOfWork.Pilots.GetById(id);
            if (pilot == null)
            {
                throw new Exception($"Pilot with id: {id} could not be found.");
            }
            return _mapper.Map<Pilots, PilotsDTO>(pilot);
        }

        public void Add(PilotsDTO pilot)
        {
            if (pilot.Name != null && pilot.FirstName != null && pilot.NickName != null && pilot.LicenseNumber != null && pilot.PhotoRelativePath != null && pilot.Sex != 0 && pilot.Length > 0 && pilot.Weight > 0)
            {
                //check if License number is allready in use by other pilot
                var pilots = _unitOfWork.Pilots.GetAll();
                foreach (var p in pilots)
                {
                    if(p.LicenseNumber == pilot.LicenseNumber)
                    {
                        throw new Exception($"License number '{pilot.LicenseNumber}' is allready in use by {p.Name}.");
                    }
                }
                _unitOfWork.Pilots.AddAsync(_mapper.Map<PilotsDTO, Pilots>(pilot));
                _unitOfWork.CommitAsync();
            }
            else throw new Exception("Pilot data is not valid");
        }

        public void Update(int id, PilotsDTO pilot)
        {
            if (pilot.Name != null && pilot.FirstName != null && pilot.NickName != null && pilot.LicenseNumber != null && pilot.PhotoRelativePath != null && pilot.Sex != 0 && pilot.Length > 0 && pilot.Weight > 0)
            {
                //check if License number is allready in use by other pilot
                /*var pilots = _unitOfWork.Pilots.GetAll();
                foreach (var p in pilots)
                {
                    if (p.LicenseNumber == pilot.LicenseNumber)
                    {
                        throw new Exception($"License number '{pilot.LicenseNumber}' is allready in use by {p.Name}.");
                    }
                }*/
                var pilotToUpdate = _unitOfWork.Pilots.GetById(id);
                if (pilotToUpdate != null)
                {
                    pilotToUpdate.Name = pilot.Name;
                    pilotToUpdate.FirstName = pilot.FirstName;
                    pilotToUpdate.NickName = pilot.NickName;
                    pilotToUpdate.LicenseNumber = pilot.LicenseNumber;
                    pilotToUpdate.PhotoRelativePath = pilot.PhotoRelativePath;
                    pilotToUpdate.Sex = pilot.Sex;
                    pilotToUpdate.Length = (int)pilot.Length;
                    pilotToUpdate.Weight = (decimal)pilot.Weight;
                    _unitOfWork.Pilots.Update(pilotToUpdate);
                    _unitOfWork.CommitAsync();
                }
                else throw new Exception($"Pilot with id: {id} could not be found");

            }
            else throw new Exception("Pilot data is not valid");
        }

        public void Delete(int id)
        {
            var pilotToDelete = _unitOfWork.Pilots.GetById(id);
            if (pilotToDelete != null)
            {
                _unitOfWork.Pilots.Remove(pilotToDelete);
                _unitOfWork.CommitAsync();
            }
            else throw new Exception($"Pilot with id: {id} could not be found");
        }
    }
}
