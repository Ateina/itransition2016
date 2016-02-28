using AutoMapper;
using School.Entities;
using School.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace School.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Material, MaterialViewModel>()
                .ForMember(vm => vm.Category, map => map.MapFrom(m => m.Category.Name))
                .ForMember(vm => vm.CategoryId, map => map.MapFrom(m => m.Category.ID))
                .ForMember(vm => vm.NumberOfTags, map => map.MapFrom(m => m.Tags.Count))
                .ForMember(vm => vm.Image, map => map.MapFrom(m => string.IsNullOrEmpty(m.Image) == true ? "unknown.jpg" : m.Image));

            Mapper.CreateMap<Category, CategoryViewModel>()
                .ForMember(vm => vm.NumberOfMaterials, map => map.MapFrom(g => g.Materials.Count()));

            Mapper.CreateMap<User, UserViewModel>();
        }
    }
}