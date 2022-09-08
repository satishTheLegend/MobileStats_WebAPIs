using System.ComponentModel.DataAnnotations;

namespace MobileStats_WebAPIs.Models
{
    public class MobileBrands
    {
        [Key]
        public int Id { get; set; }
        public int Brand_id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Remove_dt { get; set; }
        public string? Added_dt { get; set; }
        public string? Updated_dt { get; set; }
    }
}
