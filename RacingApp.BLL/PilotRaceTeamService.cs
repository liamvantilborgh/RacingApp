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
    public class PilotRaceTeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PilotRaceTeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //standard return value is IEnumerable so I have to take the first value of the IEnumerable
        public PilotRaceTeamDTO GetByIDs(int pilotId, int raceId, int teamId)
        {
            var pilotRaceTeam = _unitOfWork.PilotRaceTeam.GetByIds(pilotId, raceId, teamId);
            var firstPilotRaceTeam = new PilotRaceTeamDTO();
            foreach (var e in pilotRaceTeam)
            {
                firstPilotRaceTeam = _mapper.Map<PilotRaceTeam, PilotRaceTeamDTO>(e);
            }
            return firstPilotRaceTeam;
        }

        public IEnumerable<PilotRaceTeamDTO> GetTeamsByRaceId(int id)
        {
            //dit wordt gereturned als een IEnumerable<PilotRaceTeamDTO> inplaats van een IEnumerable<TeamsDTO> omdat ik in de 
            //frontend ook de naam van de race wil weergeven en niet alleen de teams
            var pilotRaceTeam = _unitOfWork.PilotRaceTeam.GetTeamsByRaceId(id);
            //group teams so that I only get the same team once
            var Teams = pilotRaceTeam.GroupBy(prt => prt.Team).Select(x => x.First());
            return _mapper.Map <IEnumerable<PilotRaceTeam>, IEnumerable<PilotRaceTeamDTO>>(Teams);
        }

        public IEnumerable<PilotRaceTeamDTO> GetPilotsByRaceIdTeamId(int raceId, int teamId)
        {
            //dit wordt gereturned als een IEnumerable<PilotRaceTeamDTO> inplaats van een IEnumerable<PilotsDTO> omdat ik in de 
            //frontend ook de naam van de race  en de naam van het team wil weergeven en niet alleen de piloten
            return _mapper.Map<IEnumerable<PilotRaceTeam>, IEnumerable<PilotRaceTeamDTO>>(_unitOfWork.PilotRaceTeam.GetPilotsByRaceIdTeamId(raceId, teamId));
        }

        public void Add(PilotRaceTeamDTO pilotRaceTeam)
        {
            if (pilotRaceTeam.PilotId != 0 && pilotRaceTeam.RaceId != 0 && pilotRaceTeam.TeamId != 0)
            {
                _unitOfWork.PilotRaceTeam.AddAsync(_mapper.Map<PilotRaceTeamDTO, PilotRaceTeam>(pilotRaceTeam));
                _unitOfWork.CommitAsync();
            }
            else throw new Exception("PilotRaceTeam data is not valid");
        }

        //standard return value from GetByIds (custom method) is IEnumerable so I have to take the first value of the IEnumerable
        public void Delete(int pilotId, int raceId, int teamId)
        {
            var pilotRaceTeamToDelete = _unitOfWork.PilotRaceTeam.GetByIds(pilotId, raceId, teamId);
            if (pilotRaceTeamToDelete == null)
            {
                throw new Exception($"PilotRaceTeam with ids: pilotId: {pilotId}, raceId: {raceId} and teamId: {teamId} could not be found.");
            }
            else
            {
                var FirstPilotRaceTeam = new PilotRaceTeam();
                foreach (var e in pilotRaceTeamToDelete)
                {
                    FirstPilotRaceTeam = e;
                }
                _unitOfWork.PilotRaceTeam.Remove(FirstPilotRaceTeam);
                _unitOfWork.CommitAsync();
            }
        }
    }
}
