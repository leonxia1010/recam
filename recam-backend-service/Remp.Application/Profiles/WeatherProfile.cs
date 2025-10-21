using System;
using AutoMapper;
using Remp.Application.DTOs;
using Remp.Model.Entities;

namespace Remp.Application.Profiles;

public class WeatherProfile : Profile
{
    public WeatherProfile()
    {
        CreateMap<WeatherForecast, WeatherDTO>();
    }
}
