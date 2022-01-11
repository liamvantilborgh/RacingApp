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
    public class TeamsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeamsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<TeamsDTO> GetAll()
        {
            var teams = _mapper.Map<IEnumerable<Teams>, IEnumerable<TeamsDTO>>(_unitOfWork.Teams.GetAll());
            return teams;
        }

        public TeamsDTO GetById(int id)
        {
            var team = _unitOfWork.Teams.GetById(id);
            if (team == null)
            {
                throw new Exception($"Team with id: {id} could not be found.");
            }
            return _mapper.Map<Teams, TeamsDTO>(team);
        }

        public void Add(TeamsDTO team)
        {
            if (team.Name != null)
            {
                _unitOfWork.Teams.AddAsync(_mapper.Map<TeamsDTO, Teams>(team));
                _unitOfWork.CommitAsync();
            }
            else throw new Exception("Team data is not valid");
        }

        public void Update(int id, TeamsDTO team)
        {
            if (team.Name != null)
            {
                var teamToUpdate = _unitOfWork.Teams.GetById(id);
                if (teamToUpdate != null)
                {
                    teamToUpdate.Name = team.Name;
                    _unitOfWork.Teams.Update(teamToUpdate);
                    _unitOfWork.CommitAsync();
                }
                else throw new Exception($"Team with id: {id} could not be found");

            }
            else throw new Exception("Team data is not valid");
        }

        public void Delete(int id)
        {
            var teamToDelete = _unitOfWork.Teams.GetById(id);
            if (teamToDelete != null)
            {
                //_unitOfWork.Teams.Remove(teamToDelete);
                //hier delete een team met de inline query die we moesten maken
                _unitOfWork.Teams.DeleteWithInlineQuery(id);
                _unitOfWork.CommitAsync();
            }
            else throw new Exception($"Team with id: {id} could not be found");
        }
    }
}
