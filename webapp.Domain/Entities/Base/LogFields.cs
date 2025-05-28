namespace Domain.Entities
{
    public class LogFields
    {
        public int? CreatedOn { get; set; }
        public int? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public int? Status { get; set; }
    }
    
}
