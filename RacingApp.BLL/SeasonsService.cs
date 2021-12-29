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
    public class SeasonsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SeasonsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SeasonsDTO> GetAll()
        {
            var seasons = _unitOfWork.Seasons.GetAllWithSeries();
            return _mapper.Map<IEnumerable<Seasons>, IEnumerable<SeasonsDTO>>(seasons);
        }

        public SeasonsDTO GetById(int id)
        {
            var seasons = _unitOfWork.Seasons.GetByIdWithSeries(id);
            var Season = new SeasonsDTO();
            //takes first value from IEnumerable from custom method in repo, get's a season by id with the series data filled
            foreach (var c in seasons)
            {
                Season = _mapper.Map<Seasons, SeasonsDTO>(c);
            }
            if (Season == null)
            {
                throw new Exception($"season with id: {id} could not be found.");
            }
            return Season;
        }

        //get all the seasons within a specific series
        public IEnumerable<SeasonsDTO> GetAllBySeriesId(int id)
        {
            return _mapper.Map<IEnumerable<Seasons>, IEnumerable<SeasonsDTO>>(_unitOfWork.Seasons.GetAllBySeriesId(id));
        }

        public void Add(SeasonsDTO season)
        {
            if (season.SeriesId != 0 && season.Name != null && season.Startdate < season.Enddate)
            {
                var seasonToAdd = _mapper.Map<SeasonsDTO, Seasons>(season);
                _unitOfWork.Seasons.Add(seasonToAdd);
                _unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("Season data is not valid.");
            }
        }

        public void Update(int id, SeasonsDTO season)
        {
            var seasonToUpdate = _unitOfWork.Seasons.GetById(id);
            if (seasonToUpdate == null)
            {
                throw new Exception($"Season with id: {id} could not be found.");
            }
            else
            {
                if(season.SeriesId != 0 && season.Name != null && season.Startdate < season.Enddate)
                {
                    seasonToUpdate.SeriesId = season.SeriesId;
                    seasonToUpdate.Name = season.Name;
                    seasonToUpdate.Startdate = season.Startdate;
                    seasonToUpdate.Enddate = season.Enddate;
                    _unitOfWork.Seasons.Update(seasonToUpdate);
                    _unitOfWork.CommitAsync();
                }
                else
                {
                    throw new Exception("Season data not valid.");
                } 
            }
        }

        public void Delete(int id)
        {
            var seasonToDelete = _unitOfWork.Seasons.GetById(id);
            if (seasonToDelete != null)
            {
                _unitOfWork.Seasons.Remove(seasonToDelete);
                _unitOfWork.CommitAsync();
            }
            else throw new Exception($"Season with id: {id} could not be found");
        }
    }
}
