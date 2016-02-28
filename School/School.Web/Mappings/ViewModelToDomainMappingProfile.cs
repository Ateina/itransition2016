using AutoMapper;
using School.Entities;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<MaterialViewModel, Material>()
                .ForMember(m => m.Image, map => map.Ignore())
                .ForMember(m => m.Category, map => map.Ignore());
        }
    }
}