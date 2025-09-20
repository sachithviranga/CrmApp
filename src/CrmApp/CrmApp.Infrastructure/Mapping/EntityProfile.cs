using AutoMapper;
using CrmApp.Domain.Model;
using CrmApp.Infrastructure.Entities;

namespace CrmApp.Infrastructure.Mapping
{
    /// <summary>
    /// AutoMapper profile for Infrastructure layer mapping operations.
    /// Handles bidirectional mapping between Domain models and Entity models.
    /// Ensures proper data transformation between business logic and data persistence layers.
    /// </summary>
    public class EntityProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the EntityProfile class.
        /// Configures all mapping rules between Domain and Entity models.
        /// </summary>
        public EntityProfile()
        {
            // ===== DOMAIN TO ENTITY MAPPING =====
            // Maps from Domain model (Customer) to Entity model (CustomerEntity)
            // Used when persisting domain changes to the database
            CreateMap<Customer, CustomerEntity>()
                // Map domain ID to entity AccountId (different property names)
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                // Map audit timestamps from domain to entity
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated));
                // Note: Other properties (FirstName, LastName, Email, etc.) are mapped automatically
                // by AutoMapper due to matching property names

            // ===== ENTITY TO DOMAIN MAPPING =====
            // Maps from Entity model (CustomerEntity) to Domain model (Customer)
            // Used when retrieving data from database and converting to domain model
            CreateMap<CustomerEntity, Customer>()
                // Use specific constructor to ensure business rules are enforced
                // This ensures domain model validation is applied during creation
                .ConstructUsing(e => new Customer(e.FirstName, e.LastName, e.Email))
                // Map entity AccountId to domain ID (different property names)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AccountId))
                // Map optional contact information
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                // Map audit timestamps from entity to domain
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated));
        }
    }
}