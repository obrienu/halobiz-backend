namespace HaloBiz.Model.LAMS
{
    public class QuoteServiceDocument : Documents
    {
        public long QuoteServiceId { get; set; }
        public QuoteService QuoteService { get; set; }
    }
}