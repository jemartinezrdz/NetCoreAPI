using ApiExamen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace ApiExamen.Recibo_Mapper
{
    public class Recibo_Mappers : Profile
    {
        public Recibo_Mappers()
        {
            CreateMap<Recibo, Recibo_DTO>().ReverseMap();
        }
    }
}
