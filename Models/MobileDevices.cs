using System.ComponentModel.DataAnnotations;

namespace MobileStats_WebAPIs.Models
{
    public class MobileDevices
    {
        [Key]
        public int Id { get; set; }
        public int Device_id { get; set; }
        public string? Url_hash { get; set; }
        public string? Brand_id { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public string? Released_at { get; set; }
        public string? Body { get; set; }
        public string? Os { get; set; }
        public string? Storage { get; set; }
        public string? Display_size { get; set; }
        public string? Display_resolution { get; set; }
        public string? Camera_pixels { get; set; }
        public string? Video_pixels { get; set; }
        public string? Ram { get; set; }
        public string? Chipset { get; set; }
        public string? Battery_size { get; set; }
        public string? Battery_type { get; set; }
        public string? Specifications { get; set; }
        public string? Deleted_at { get; set; }
        public string? Created_at { get; set; }
        public string? Updated_at { get; set; }
        [Required]
        public int? Price { get; set; }
        public int? Dis_Price { get; set; }
        public int? Max_discount { get; set; }
        
        public int Quantity { get; set; }
    }
}
