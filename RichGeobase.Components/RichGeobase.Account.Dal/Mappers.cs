using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GeoLib;
using GeoLib.Specification;
using RichGeobase.Account.Dal.Dal;
using RichGeobase.Account.Dal.model;
using RichGeobase.Query.Interface;

namespace RichGeobase.Account.Dal
{
    internal class Mappers
    {
        public void Initialize()
        {
            Mapper.CreateMap<User, UserDto>();
        }

        public static IDto GetDto<T>(T entity)
        {
            return Mapper.Map<T, IDto>(entity);
        }

        public static IEntity<int> GetEntity<TDto>(TDto dto) where TDto : IDto
        {
            return Mapper.Map<TDto, IEntity<int>>(dto);
        }
    }
}