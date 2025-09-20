using CrmApp.Application.Interfaces;
using CrmApp.Shared.DTO;
using FluentValidation;

namespace CrmApp.Application.Validators
{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerRequestValidator(ICustomerRepository repository)
        {
            _repository = repository;

            // ID validation - must be positive and within reasonable range
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0")
                .LessThanOrEqualTo(int.MaxValue)
                .WithMessage("Customer ID must be a valid integer")
                .Must(id => id > 0)
                .WithMessage("Customer ID is required and must be a positive number");

            // First Name validation - required, not empty, reasonable length
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                .WithMessage("First name can only contain letters, spaces, hyphens, and apostrophes");

            // Last Name validation - required, not empty, reasonable length
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(50)
                .WithMessage("Last name cannot exceed 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                .WithMessage("Last name can only contain letters, spaces, hyphens, and apostrophes");

            // Email validation - required, valid format, reasonable length
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email must be in a valid format")
                .MaximumLength(100)
                .WithMessage("Email cannot exceed 100 characters")
                .MustAsync(BeUniqueEmail)
                .WithMessage("Email already exists");

            // Phone Number validation - optional, but if provided must be valid format
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^[\+]?[1-9][\d]{0,15}$")
                .WithMessage("Phone number must be in a valid international format")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            // Address validation - optional, but if provided must be reasonable length
            RuleFor(x => x.Address)
                .MaximumLength(200)
                .WithMessage("Address cannot exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.Address));

            // City validation - optional, but if provided must be reasonable length and format
            RuleFor(x => x.City)
                .MaximumLength(50)
                .WithMessage("City cannot exceed 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                .WithMessage("City can only contain letters, spaces, hyphens, and apostrophes")
                .When(x => !string.IsNullOrEmpty(x.City));

            // State validation - optional, but if provided must be reasonable length and format
            RuleFor(x => x.State)
                .MaximumLength(50)
                .WithMessage("State cannot exceed 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                .WithMessage("State can only contain letters, spaces, hyphens, and apostrophes")
                .When(x => !string.IsNullOrEmpty(x.State));

            // Country validation - optional, but if provided must be reasonable length and format
            RuleFor(x => x.Country)
                .MaximumLength(50)
                .WithMessage("Country cannot exceed 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                .WithMessage("Country can only contain letters, spaces, hyphens, and apostrophes")
                .When(x => !string.IsNullOrEmpty(x.Country));
        }

        private async Task<bool> BeUniqueEmail(UpdateCustomerRequest request, string email, CancellationToken token)
        {
            // exclude current customer when updating
            return !await _repository.ExistsByEmailAsync(email, request.Id);
        }
    }
}
