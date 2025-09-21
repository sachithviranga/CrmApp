using System;

namespace CrmApp.Domain.Model
{
    /// <summary>
    /// Represents a Customer domain entity in the CRM system.
    /// Encapsulates business rules, validation, and behavior for customer management.
    /// This is the core domain model that enforces business invariants.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets the unique identifier for the customer.
        /// Set by the infrastructure layer during entity mapping.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the customer's first name.
        /// Required field with business rule validation.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the customer's last name.
        /// Required field with business rule validation.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the customer's email address.
        /// Required field with email format validation.
        /// Must be unique across all customers.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the customer's phone number.
        /// Optional field for contact information.
        /// </summary>
        public string? PhoneNumber { get; private set; }

        /// <summary>
        /// Gets the customer's street address.
        /// Optional field for location information.
        /// </summary>
        public string? Address { get; private set; }

        /// <summary>
        /// Gets the customer's city.
        /// Optional field for location information.
        /// </summary>
        public string? City { get; private set; }

        /// <summary>
        /// Gets the customer's state or province.
        /// Optional field for location information.
        /// </summary>
        public string? State { get; private set; }

        /// <summary>
        /// Gets the customer's country.
        /// Optional field for location information.
        /// </summary>
        public string? Country { get; private set; }

        /// <summary>
        /// Gets the date and time when the customer was created.
        /// Automatically set during customer creation.
        /// </summary>
        public DateTime DateCreated { get; private set; }

        /// <summary>
        /// Gets the date and time when the customer was last updated.
        /// Automatically updated when customer details are modified.
        /// </summary>
        public DateTime DateUpdated { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Customer class with required information.
        /// Enforces business rules and validation during customer creation.
        /// </summary>
        /// <param name="firstName">Customer's first name (required)</param>
        /// <param name="lastName">Customer's last name (required)</param>
        /// <param name="email">Customer's email address (required, must be valid format)</param>
        /// <exception cref="Exceptions.DomainException">Thrown when first name or last name is null or empty</exception>
        /// <exception cref="Exceptions.InvalidEmailException">Thrown when email is null, empty, or invalid format</exception>
        public Customer(string firstName, string lastName, string email)
        {
            // Business Rule: First name is required and cannot be empty or whitespace
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exceptions.DomainException("First name is required.");
            
            // Business Rule: Last name is required and cannot be empty or whitespace
            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exceptions.DomainException("Last name is required.");
            
            // Business Rule: Email is required and must contain '@' symbol (basic validation)
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Exceptions.InvalidEmailException(email);

            // Set required properties
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            
            // Set creation timestamp
            DateCreated = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates customer details with comprehensive validation.
        /// Enforces business rules and updates the modification timestamp.
        /// </summary>
        /// <param name="firstName">Updated first name (required)</param>
        /// <param name="lastName">Updated last name (required)</param>
        /// <param name="email">Updated email address (required, must be valid format)</param>
        /// <param name="phoneNumber">Updated phone number (optional)</param>
        /// <param name="address">Updated street address (optional)</param>
        /// <param name="city">Updated city (optional)</param>
        /// <param name="state">Updated state or province (optional)</param>
        /// <param name="country">Updated country (optional)</param>
        /// <exception cref="Exceptions.DomainException">Thrown when first name or last name is null or empty</exception>
        /// <exception cref="Exceptions.InvalidEmailException">Thrown when email is null, empty, or invalid format</exception>
        public void UpdateDetails(
            string firstName, string lastName, string email,
            string? phoneNumber, string? address, string? city, string? state, string? country)
        {
            // Business Rule: First name is required and cannot be empty or whitespace
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exceptions.DomainException("First name is required.");
            
            // Business Rule: Last name is required and cannot be empty or whitespace
            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exceptions.DomainException("Last name is required.");
            
            // Business Rule: Email is required and must contain '@' symbol (basic validation)
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new Exceptions.InvalidEmailException(email);

            // Update all properties with new values
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            State = state;
            Country = country;
            
            // Update modification timestamp
            DateUpdated = DateTime.UtcNow;
        }
    }
}
