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
    public class SeriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SeriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SeriesDTO> GetAll()
        {
            var series = _mapper.Map<IEnumerable<Series>, IEnumerable<SeriesDTO>>(_unitOfWork.Series.GetAll());
            return series;
        }
        public SeriesDTO GetById(int id)
        {
            var series = _unitOfWork.Series.GetById(id);
            if (series == null)
            {
                throw new Exception($"Series with id: {id} could not be found.");
            }
            return _mapper.Map<Series, SeriesDTO>(series);
        }

        public void Add(SeriesDTO series)
        {
            var seriesToChange = _unitOfWork.Series.GetAll().OrderBy(s => s.Sort_Order); 
            if (series.Name != null)
            {
                if (series.Enddate != null)
                {
                    if (series.Startdate < series.Enddate)
                    {
                        //updates sort order
                        var sortOrder = series.Sort_Order;
                        foreach (var item in seriesToChange)
                        {
                            if (sortOrder == item.Sort_Order)
                            {
                                item.Sort_Order++;
                                _unitOfWork.Series.Update(item);
                                _unitOfWork.CommitAsync();
                                sortOrder = item.Sort_Order;
                            }
                        }
                        _unitOfWork.Series.AddAsync(_mapper.Map<SeriesDTO, Series>(series));
                        _unitOfWork.CommitAsync();
                    }
                    else throw new Exception("Date is not valid");
                }
                else
                {
                    if(series.Startdate <= DateTime.Today)
                    {
                        series.Active = true;
                    }
                    else
                    {
                        series.Active = false;
                    }
                    //updates sort order
                    var sortOrder = series.Sort_Order;
                    foreach (var item in seriesToChange)
                    {
                        if (sortOrder == item.Sort_Order)
                        {
                            item.Sort_Order++;
                            _unitOfWork.Series.Update(item);
                            _unitOfWork.CommitAsync();
                            sortOrder = item.Sort_Order;
                        }
                    }
                    _unitOfWork.Series.AddAsync(_mapper.Map<SeriesDTO, Series>(series));
                    _unitOfWork.CommitAsync();
                }                       
            }
            else throw new Exception("Series data is not valid");
        }

        public void Update(int id, SeriesDTO series)
        {
            var seriesToChange = _unitOfWork.Series.GetAll().OrderBy(s => s.Sort_Order); 
            
            if (series.Name != null)
            {
                var seriesToUpdate = _unitOfWork.Series.GetById(id);
                if (seriesToUpdate != null)
                {
                    seriesToUpdate.Name = series.Name;
                    seriesToUpdate.Active = series.Active;
                    seriesToUpdate.Sort_Order = (int)series.Sort_Order;
                    seriesToUpdate.Startdate = series.Startdate;

                    var sortOrder = (int)series.Sort_Order;
                    //update sort order
                    foreach (var item in seriesToChange)
                    {
                        if(item.Id != id)
                        {
                           if (sortOrder == item.Sort_Order)
                           {
                                item.Sort_Order++;
                                _unitOfWork.Series.Update(item);
                                _unitOfWork.CommitAsync();
                                sortOrder = item.Sort_Order;
                           }
                        }
                    }

                    if (series.Enddate != null)
                    {
                        if (series.Startdate < series.Enddate)
                        {
                            seriesToUpdate.Enddate = (DateTime)series.Enddate;
                            _unitOfWork.Series.Update(seriesToUpdate);
                            _unitOfWork.CommitAsync();
                        }
                        else throw new Exception("Date is not valid");
                    }
                    else
                    {
                        _unitOfWork.Series.Update(seriesToUpdate);
                        _unitOfWork.CommitAsync();
                    }
                    
                }
                else throw new Exception($"Series with id: {id} could not be found");

            }
            else throw new Exception("Series data is not valid");
        }

        public void Delete(int id)
        {
            var seriesToDelete = _unitOfWork.Series.GetById(id);
            if (seriesToDelete != null)
            {
                _unitOfWork.Series.Remove(seriesToDelete);
                _unitOfWork.CommitAsync();
            }
            else throw new Exception($"Series with id: {id} could not be found");
        }
    }
}
