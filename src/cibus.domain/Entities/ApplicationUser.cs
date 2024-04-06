using System;

namespace cibus.domain.Entities
{
    public class ApplicationUser 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NIC { get; set; }
        public DateTime? DOB { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public int? ClientId { get; set; }
    }
}
