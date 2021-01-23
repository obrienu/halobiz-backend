namespace HaloBiz.Model.LAMS
{
    public class LeadKeyPerson : Contact
    {
        public long LeadId { get; set; }
        public virtual Lead Lead { get; set; }
    }
}