namespace HaloBiz.DTOs.TransferDTOs
{
    public class ServicesTransferDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; } = 0;
        public double VAT { get; set; }
        public double BillableAmount { get; set; }
       
    }
}