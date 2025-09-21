using AutoMapper;
using CrmApp.Domain.Model;
using CrmApp.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmApp.Infrastructure.Mapping
{
    public class CustomerEntityProfile : Profile
    {
        public CustomerEntityProfile() 
        {
            CreateMap<Customer, CustomerEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated));

            CreateMap<CustomerEntity, Customer>()
                .ConstructUsing(e => new Customer(e.FirstName, e.LastName, e.Email))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated));
        }
    }
}
