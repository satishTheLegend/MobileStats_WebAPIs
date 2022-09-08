using System.ComponentModel.DataAnnotations;

namespace MobileStats_WebAPIs.Models
{
    public class MobileSellingRecords
    {
        [Key]
        public int Id { get; set; }
        public string Transaction_Id { get; set; } = Guid.NewGuid().ToString();
        public string? Brand { get; set; }
        public string? Brand_id { get; set; }
        public string? DeviceName { get; set; }
        public string? Device_id { get; set; }
        public string? IMEI_number { get; set; }
        public string? Original_price { get; set; }
        public string? MaxDiscount_price { get; set; }
        public string? Selling_price { get; set; }
        public string? Max_discount { get; set; }
        public string? Given_discount { get; set; }
        public bool? IsDiscountApplied { get; set; }
        public DateTime? Selling_dt { get; set; } = DateTime.Now;
        public string? TransactionUpdated_at { get; set; }
        public string? Buyer_name { get; set; }
    }
}
