using CrmApp.Shared.DTO;

namespace CrmApp.Server.Tests
{
    public static class TestData
    {
        public static readonly List<CustomerDto> Customers = [
            new CustomerDto { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", PhoneNumber = "555-0101", Address = "123 Main St", City = "Springfield", State = "IL", Country = "USA", FullName = "Alice Smith", CreatedDate = "2024-01-01", UpdatedDate = "2024-01-02" },
                new CustomerDto { Id = 2, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", PhoneNumber = "555-0102", Address = "456 Oak Ave", City = "Springfield", State = "IL", Country = "USA", FullName = "Bob Johnson", CreatedDate = "2024-01-03", UpdatedDate = "2024-01-04" },
                new CustomerDto { Id = 3, FirstName = "Carol", LastName = "Williams", Email = "carol.williams@example.com", PhoneNumber = "555-0103", Address = "789 Pine Rd", City = "Springfield", State = "IL", Country = "USA", FullName = "Carol Williams", CreatedDate = "2024-01-05", UpdatedDate = "2024-01-06" },
                new CustomerDto { Id = 4, FirstName = "David", LastName = "Brown", Email = "david.brown@example.com", PhoneNumber = "555-0104", Address = "321 Maple St", City = "Springfield", State = "IL", Country = "USA", FullName = "David Brown", CreatedDate = "2024-01-07", UpdatedDate = "2024-01-08" },
                new CustomerDto { Id = 5, FirstName = "Eve", LastName = "Jones", Email = "eve.jones@example.com", PhoneNumber = "555-0105", Address = "654 Cedar Ave", City = "Springfield", State = "IL", Country = "USA", FullName = "Eve Jones", CreatedDate = "2024-01-09", UpdatedDate = "2024-01-10" },
                new CustomerDto { Id = 6, FirstName = "Frank", LastName = "Garcia", Email = "frank.garcia@example.com", PhoneNumber = "555-0106", Address = "987 Birch Rd", City = "Springfield", State = "IL", Country = "USA", FullName = "Frank Garcia", CreatedDate = "2024-01-11", UpdatedDate = "2024-01-12" },
                new CustomerDto { Id = 7, FirstName = "Grace", LastName = "Martinez", Email = "grace.martinez@example.com", PhoneNumber = "555-0107", Address = "159 Spruce St", City = "Springfield", State = "IL", Country = "USA", FullName = "Grace Martinez", CreatedDate = "2024-01-13", UpdatedDate = "2024-01-14" },
                new CustomerDto { Id = 8, FirstName = "Henry", LastName = "Davis", Email = "henry.davis@example.com", PhoneNumber = "555-0108", Address = "753 Elm Ave", City = "Springfield", State = "IL", Country = "USA", FullName = "Henry Davis", CreatedDate = "2024-01-15", UpdatedDate = "2024-01-16" },
                new CustomerDto { Id = 9, FirstName = "Ivy", LastName = "Rodriguez", Email = "ivy.rodriguez@example.com", PhoneNumber = "555-0109", Address = "852 Willow Rd", City = "Springfield", State = "IL", Country = "USA", FullName = "Ivy Rodriguez", CreatedDate = "2024-01-17", UpdatedDate = "2024-01-18" },
                new CustomerDto { Id = 10, FirstName = "Jack", LastName = "Miller", Email = "jack.miller@example.com", PhoneNumber = "555-0110", Address = "951 Aspen St", City = "Springfield", State = "IL", Country = "USA", FullName = "Jack Miller", CreatedDate = "2024-01-19", UpdatedDate = "2024-01-20" },
                new CustomerDto { Id = 11, FirstName = "Kathy", LastName = "Wilson", Email = "kathy.wilson@example.com", PhoneNumber = "555-0111", Address = "357 Poplar Ave", City = "Springfield", State = "IL", Country = "USA", FullName = "Kathy Wilson", CreatedDate = "2024-01-21", UpdatedDate = "2024-01-22" },
                new CustomerDto { Id = 12, FirstName = "Leo", LastName = "Moore", Email = "leo.moore@example.com", PhoneNumber = "555-0112", Address = "258 Chestnut Rd", City = "Springfield", State = "IL", Country = "USA", FullName = "Leo Moore", CreatedDate = "2024-01-23", UpdatedDate = "2024-01-24" },
                new CustomerDto { Id = 13, FirstName = "Mia", LastName = "Taylor", Email = "mia.taylor@example.com", PhoneNumber = "555-0113", Address = "654 Walnut St", City = "Springfield", State = "IL", Country = "USA", FullName = "Mia Taylor", CreatedDate = "2024-01-25", UpdatedDate = "2024-01-26" },
                new CustomerDto { Id = 14, FirstName = "Nick", LastName = "Anderson", Email = "nick.anderson@example.com", PhoneNumber = "555-0114", Address = "753 Hickory Ave", City = "Springfield", State = "IL", Country = "USA", FullName = "Nick Anderson", CreatedDate = "2024-01-27", UpdatedDate = "2024-01-28" },
                new CustomerDto { Id = 15, FirstName = "Olivia", LastName = "Thomas", Email = "olivia.thomas@example.com", PhoneNumber = "555-0115", Address = "852 Sycamore Rd", City = "Springfield", State = "IL", Country = "USA", FullName = "Olivia Thomas", CreatedDate = "2024-01-29", UpdatedDate = "2024-01-30" }
        ];
    }
}
