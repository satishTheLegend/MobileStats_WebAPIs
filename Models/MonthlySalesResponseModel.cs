namespace MobileStats_WebAPIs.Models
{
    public class MonthlySalesResponseModel
    {
        public MonthlySalesDetails? MonthlySalesDetails { get; set; }
        public List<MobileSellingRecords>? MobileSellingRecords { get; set; }
    }

    public class TimeLineResponseModel
    {
        public MonthlySalesDetails? TimeLine1 { get; set; }
        public MonthlySalesDetails? TimeLine2 { get; set; }
    }

    public class MonthlySalesDetails
    {
        public long TotalSellingAmount { get; set; }
        public long TotalSoldDevices { get; set; }
        public List<HighlySoldDevicesInfo>? HighestSoldDevices { get; set; }
        public long TotalProfit { get; set; }
        public long TotalLoss { get; set; }
    }

    public class HighlySoldDevicesInfo
    {
        public string? BrandName { get; set; }
        public List<DeviceInfo>? DeviceInfoList { get; set; }
    }

    public class DeviceInfo
    {
        public string? DeviceName { get; set; }
        public string? TransactionId { get; set; }
        public string? Discount { get; set; }
        public string? OriginalPrice { get; set; }
        public string? SoldPrice { get; set; }
        public DateTime? Sold_dt { get; set; }
    }
}
