namespace Project_HotelBooking.Base
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? CreatedByUserId { get; set; } 

        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedByUserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
