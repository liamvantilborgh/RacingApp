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
    public class CountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<CountryDTO> GetAll()
        {
            var countries = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(_unitOfWork.Countries.GetAll());
            return countries;
        }

        public CountryDTO GetById(int id)
        {
            var country = _unitOfWork.Countries.GetById(id);
            if (country == null)
            {
                throw new Exception($"Country with id: {id} could not be found.");
            }
            return _mapper.Map<Country, CountryDTO>(country);
        }

        public void Add(CountryDTO country)
        {
            if (country.Name != null)
            {
                _unitOfWork.Countries.AddAsync(_mapper.Map<CountryDTO, Country>(country));
                _unitOfWork.CommitAsync();
            }
            else throw new Exception("Country data is not valid");
        }

        public void Update(int id, CountryDTO country)
        {
            if (country.Name != null)
            {
                var countryToUpdate = _unitOfWork.Countries.GetById(id);
                if (countryToUpdate != null)
                {
                    countryToUpdate.Name = country.Name;
                    _unitOfWork.Countries.Update(countryToUpdate);
                    _unitOfWork.CommitAsync();
                }
                else throw new Exception($"Country with id: {id} could not be found");

            }
            else throw new Exception("Country data is not valid");
        }

        public void Delete(int id)
        {
            var countryToDelete = _unitOfWork.Countries.GetById(id);
            if (countryToDelete != null)
            {
                _unitOfWork.Countries.Remove(countryToDelete);
                _unitOfWork.CommitAsync();
            }
            else throw new Exception($"Country with id: {id} could not be found");
        }
    }
}
