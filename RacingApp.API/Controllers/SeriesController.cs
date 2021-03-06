using System;
using System.Collections.Generic;
using System.Diagnostics;
using Abp.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacingApp.BLL;
using RacingApp.Core.DTO_S;

namespace RacingApp.API.Controllers
{
    [Route("api/series")]
    [ApiController]
    public class SeriesController : Controller
    {
        private SeriesService _seriesService;
        public SeriesController(SeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        [HttpGet]
        public IEnumerable<SeriesDTO> GetAll()
        {
            return _seriesService.GetAll();
        }

        [HttpGet("{id}")]
        // GET: series/5
        public SeriesDTO GetById(int id)
        {
            return _seriesService.GetById(id);
        }

        [HttpPost("add")]
        //POST: series/add
        public IActionResult Add(SeriesDTO series)
        {
            try
            {
                _seriesService.Add(series);
            }
            catch (DbUpdateException E)
            {
                throw new Exception("Series with this name already exists.");
            }
            return Ok(series);
        }

        [HttpPost("update/{id}")]
        //POST: series/update/5
        public IActionResult Update(int id, SeriesDTO series)
        {
            try
            {
                _seriesService.Update(id, series);
            }
            catch(DbUpdateException E)
            {
                throw new Exception("Series with this name already exists.");
            }
            
            return Ok(series);
        }

        [HttpPost("delete/{id}")]
        // POST: series/delete/5
        public IActionResult Delete(int id)
        {
            _seriesService.Delete(id);
            return Ok(id);
        }
    }
}
