using System;

namespace StudentAdminPortal.API.DTO
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        // Navigation Property
        public Guid StudentId { get; set; }
    }
}
