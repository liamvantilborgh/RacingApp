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
    public class RacesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RacesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<RacesDTO> GetAll()
        {
            var races = _unitOfWork.Races.GetAllWithSeasonsCircuits();
            return _mapper.Map<IEnumerable<Races>, IEnumerable<RacesDTO>>(races);
        }

        public RacesDTO GetById(int id)
        {
            var races = _unitOfWork.Races.GetByIdWithSeasonsCircuits(id);
            var Race = new RacesDTO();
            //takes first value from IEnumerable from custom method in repo, get's a race by id with the season and the circuit data filled
            foreach (var c in races)
            {
                Race = _mapper.Map<Races, RacesDTO>(c);
            }
            if (Race == null)
            {
                throw new Exception($"Race with id: {id} could not be found.");
            }
            return Race;
        }

        //get all the races within a specific season
        public IEnumerable<RacesDTO> getAllBySeasonId(int id)
        {
            return _mapper.Map<IEnumerable<Races>, IEnumerable<RacesDTO>>(_unitOfWork.Races.GetAllBySeasonsId(id));
        }

        //get all the races within a specific circuit
        public IEnumerable<RacesDTO> GetAllByCircuitId(int id)
        {
            return _mapper.Map<IEnumerable<Races>, IEnumerable<RacesDTO>>(_unitOfWork.Races.GetAllByCircuitsId(id));
        }

        //get all the races within a specific circuit and season
        public IEnumerable<RacesDTO> GetAllByBothId(int seasonId, int circuitId)
        {
            return _mapper.Map<IEnumerable<Races>, IEnumerable<RacesDTO>>(_unitOfWork.Races.GetAllBySeasonsIdCircuitsId(seasonId, circuitId));
        }

        public void Add(RacesDTO Race)
        {
            //need to get the season to compare the dates so that racedates are in between seasons dates
            var season = _unitOfWork.Seasons.GetById(Race.SeasonId);
            if (Race.SeasonId != 0 && Race.CircuitId != 0 && Race.Name != null && Race.Startdate < Race.Enddate && Race.Startdate >= season.Startdate && Race.Enddate <= season.Enddate)
            {
                Debug.WriteLine("Start " + Race.Startdate + " " + season.Startdate);
                Debug.WriteLine("End " + Race.Enddate + " " + season.Enddate);
                var raceToAdd = _mapper.Map<RacesDTO, Races>(Race);
                _unitOfWork.Races.Add(raceToAdd);
                _unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("Race data is not valid.");
            }
        }

        public void Update(int id, RacesDTO race)
        {
            var raceToUpdate = _unitOfWork.Races.GetById(id);
            //need to get the season to compare the dates so that racedates are in between seasons dates
            var season = _unitOfWork.Seasons.GetById(race.SeasonId);
            if (raceToUpdate == null)
            {
                throw new Exception($"Race with id: {id} could not be found.");
            }
            else
            {
                if (race.SeasonId != 0 && race.CircuitId != 0 && race.Name != null && race.Startdate < race.Enddate && race.Startdate >= season.Startdate && race.Enddate <= season.Enddate)
                {
                    raceToUpdate.SeasonId = race.SeasonId;
                    raceToUpdate.CircuitId = race.CircuitId;
                    raceToUpdate.Name = race.Name;
                    raceToUpdate.Startdate = race.Startdate;
                    raceToUpdate.Enddate = race.Enddate;
                    _unitOfWork.Races.Update(raceToUpdate);
                    _unitOfWork.CommitAsync();
                }
                else
                {
                    throw new Exception("Race data not valid.");
                }
            }
        }

        public void Delete(int id)
        {
            var raceToDelete = _unitOfWork.Races.GetById(id);
            if (raceToDelete != null)
            {
                _unitOfWork.Races.Remove(raceToDelete);
                _unitOfWork.CommitAsync();
            }
            else throw new Exception($"Race with id: {id} could not be found");
        }
    }
}
