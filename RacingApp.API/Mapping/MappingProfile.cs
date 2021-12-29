﻿using AutoMapper;
using RacingApp.Core.DTO_S;
using RacingApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDTO>().ForMember(c => c.Circuits, option => option.Ignore()).ReverseMap();
            //CreateMap<Circuits, CircuitsDTO>().ForMember(c => c.Country, option => option.MapFrom(src => src.Country)).ReverseMap();
            CreateMap<Circuits, CircuitsDTO>().ReverseMap();
            CreateMap<Series, SeriesDTO>().ForMember(s => s.Seasons, option => option.Ignore()).ReverseMap();
            CreateMap<Seasons, SeasonsDTO>().ReverseMap();
            CreateMap<Teams, TeamsDTO>().ReverseMap();
        }
    }
}
