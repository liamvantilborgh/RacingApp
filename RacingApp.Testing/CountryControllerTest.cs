using Xunit;
using RacingApp.API.Controllers;
using System.Linq;
using RacingApp.BLL;
using RacingApp.DAL;
using AutoMapper;

namespace RacingApp.Testing
{
    public class CountryControllerTest : IClassFixture<CountriesController>
    {
        private CountriesController _sut;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CountryService _dal;
        public CountryControllerTest()
        {
            _dal = new CountryService(_unitOfWork, _mapper);
            _sut = new CountriesController(_dal);
        }
        [Fact]
        public void Test1()
        {
            var CountryList = _sut.GetAll().ToList();
            Assert.Empty(CountryList);
        }
    }
}
