namespace HaloBiz.Model.LAMS
{
    public class LeadDivisionKeyPerson : Contact
    {
        public long LeadDivisionId { get; set; }
        public virtual LeadDivision LeadDivision { get; set; }  
    }
}