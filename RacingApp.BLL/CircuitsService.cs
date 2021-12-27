using AutoMapper;
using RacingApp.Core.DTO_S;
using RacingApp.DAL;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.BLL
{
    public class CircuitsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CircuitsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CircuitsDTO> GetAll()
        {
            var people = _unitOfWork.Circuits.GetAllWithCountry();
            return _mapper.Map<IEnumerable<Circuits>, IEnumerable<CircuitsDTO>>(people);
        }

        public CircuitsDTO GetById(int id)
        {
            var circuits = _unitOfWork.Circuits.GetByIdWithCountry(id);
            var circuit = new CircuitsDTO();
            //takes first value from IEnumerable from custom method in repo, get's a circuit by id with the country data filled
            foreach (var c in circuits)
            {
                circuit = _mapper.Map<Circuits, CircuitsDTO>(c);
            }
            if (circuit == null)
            {
                throw new Exception($"circuit with id: {id} could not be found.");
            }
            return circuit;
        }

        //get all the circuits within a specific country
        public IEnumerable<CircuitsDTO> GetAllByCountryId(int id)
        {
            return _mapper.Map<IEnumerable<Circuits>, IEnumerable<CircuitsDTO>>(_unitOfWork.Circuits.GetAllByCountryId(id));
        }

        public void Add(CircuitsDTO circuit)
        {
            if (circuit.Name != null)
            {
                var circuitToAdd = _mapper.Map<CircuitsDTO, Circuits>(circuit);
                _unitOfWork.Circuits.Add(circuitToAdd);
                _unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("Country data is not valid.");
            }


        }
    }
}
