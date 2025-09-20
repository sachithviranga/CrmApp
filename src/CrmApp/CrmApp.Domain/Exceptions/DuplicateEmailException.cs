namespace CrmApp.Domain.Exceptions
{
    public class DuplicateEmailException : DomainException
    {
        public DuplicateEmailException(string email)
            : base($"The email '{email}' already exists in the system.") { }
    }
}
