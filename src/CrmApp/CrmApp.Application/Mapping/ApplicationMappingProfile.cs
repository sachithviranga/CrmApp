using AutoMapper;
using CrmApp.Domain.Model;
using CrmApp.Shared.DTO;

namespace CrmApp.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for Application layer mapping operations.
    /// Handles mapping between Domain models and DTOs for API communication.
    /// Provides computed fields and formatted data for client consumption.
    /// </summary>
    public class ApplicationMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationMappingProfile class.
        /// Configures all mapping rules between Domain models and DTOs.
        /// </summary>
        public ApplicationMappingProfile()
        {
            // ===== DOMAIN TO DTO MAPPING =====
            // Maps from Domain model (Customer) to Response DTO (CustomerDto)
            // Used when returning data to API clients
            CreateMap<Customer, CustomerDto>()
                // Compute full name by concatenating first and last names
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                // Format creation date for consistent API response
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateCreated.ToString("yyyy-MM-dd")))
                // Format update date for consistent API response
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.DateUpdated.ToString("yyyy-MM-dd")));
                // Note: Other properties (Id, FirstName, LastName, Email, etc.) are mapped automatically

            // ===== DTO TO DOMAIN MAPPING =====
            // Maps from Response DTO (CustomerDto) to Domain model (Customer)
            // Used when processing data from API clients
            CreateMap<CustomerDto, Customer>()
                // Use specific constructor to ensure business rules are enforced
                .ConstructUsing(dto => new Customer(dto.FirstName, dto.LastName, dto.Email))
                // Map optional contact information
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
                // Note: Id, DateCreated, DateUpdated are handled by domain model logic

            // ===== REQUEST DTO TO DOMAIN MAPPING =====
            // Maps from Create Request DTO to Domain model
            // Used when creating new customers from API requests
            CreateMap<CreateCustomerRequest, Customer>()
                // Use specific constructor to ensure business rules are enforced
                .ConstructUsing(req => new Customer(req.FirstName, req.LastName, req.Email));
                // Note: Optional fields (PhoneNumber, Address, etc.) are set via UpdateDetails method

            // ===== UPDATE REQUEST MAPPING =====
            // Maps from Update Request DTO to Domain model
            // Used when updating existing customers from API requests
            CreateMap<UpdateCustomerRequest, Customer>()
                // Ignore ID mapping - ID should come from route parameter, not request body
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                // Ignore creation date - should not be modified during updates
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                // Set update timestamp to current time
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.UtcNow));
                // Note: This mapping is used for partial updates, full mapping done via UpdateDetails method
        }
    }
}
