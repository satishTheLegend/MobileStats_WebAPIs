using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStats_WebAPIs.DBContext;
using MobileStats_WebAPIs.Helper;
using MobileStats_WebAPIs.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MobileStats_WebAPIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MobileTransactionalDataController : Controller
    {
        #region Properties
        public ApplicationDBContext _dbContext;
        #endregion

        #region Constructor
        public MobileTransactionalDataController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion


        #region API Methods
        //GET: api/<MobileSellingRecordsController>
        [HttpGet]
        [Route("GetAllTransactions")]
        [ProducesResponseType(typeof(List<MobileSellingRecords>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactions()
        {
            //await CreateDummyRecords();
            List<MobileSellingRecords> mobileTransactionRecordsList = await _dbContext.MobileSellingRecords.ToListAsync();
            return mobileTransactionRecordsList == null ? NotFound(Common.ResponseMode<List<MobileSellingRecords>>(404, "No Records Found", mobileTransactionRecordsList == null ? new List<MobileSellingRecords>() : mobileTransactionRecordsList)) : Ok(Common.ResponseMode<List<MobileSellingRecords>>(200, $"{mobileTransactionRecordsList?.Count} Records found", mobileTransactionRecordsList));
        }



        //GET api/<MobileSellingRecordsController>/5
        [HttpGet]
        [Route("GetTransactionByTransactionId")]
        [ProducesResponseType(typeof(MobileSellingRecords), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionByTransactionId(string transactionId)
        {
            MobileSellingRecords mobileTransaction = await _dbContext.MobileSellingRecords.FirstOrDefaultAsync(x => x.Transaction_Id == transactionId);
            return mobileTransaction == null ? NotFound(Common.ResponseMode<MobileSellingRecords>(404, "No Mobile Devices Found", mobileTransaction == null ? new MobileSellingRecords() : mobileTransaction)) : Ok(Common.ResponseMode<MobileSellingRecords>(200, $"{mobileTransaction.DeviceName} Found", mobileTransaction));
        }

        [HttpGet]
        [Route("GetTransactionByBrand")]
        [ProducesResponseType(typeof(MobileSellingRecords), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionByBrand(string brandId)
        {
            List<MobileSellingRecords> mobileTransactionByBrand = await _dbContext.MobileSellingRecords.Where(x => x.Brand_id == brandId).ToListAsync();
            return mobileTransactionByBrand == null ? NotFound(Common.ResponseMode<List<MobileSellingRecords>>(404, "No Transactions Found", mobileTransactionByBrand == null ? new List<MobileSellingRecords>() : mobileTransactionByBrand)) : Ok(Common.ResponseMode<List<MobileSellingRecords>>(200, $"{mobileTransactionByBrand?.Count} Transactions Found", mobileTransactionByBrand));
        }

        [HttpGet]
        [Route("GetTransactionByDevice")]
        [ProducesResponseType(typeof(MobileSellingRecords), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionByDevice(string deviceId)
        {
            List<MobileSellingRecords> mobileDeviceTransactionByDevice = await _dbContext.MobileSellingRecords.Where(x => x.Device_id == deviceId).ToListAsync();
            return mobileDeviceTransactionByDevice == null ? NotFound(Common.ResponseMode<List<MobileSellingRecords>>(404, "No Mobile Devices Found", mobileDeviceTransactionByDevice == null ? new List<MobileSellingRecords>() : mobileDeviceTransactionByDevice)) : Ok(Common.ResponseMode<List<MobileSellingRecords>>(200, $"{mobileDeviceTransactionByDevice?.Count} Transactions Found", mobileDeviceTransactionByDevice));
        }

        // POST api/<MobileSellingRecordsController>
        [HttpPost]
        [Route("AddTransaction")]
        [ProducesResponseType(typeof(MobileSellingRecords), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransaction(MobileSellingRecords transaction)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileSellingRecords>(404, "Same transaction already Exist", transaction));
            try
            {
                MobileSellingRecords mobileTransactionRecord = await _dbContext.MobileSellingRecords.FirstOrDefaultAsync(x => x.Transaction_Id == transaction.Transaction_Id);
                if (mobileTransactionRecord == null)
                {
                    await _dbContext.MobileSellingRecords.AddAsync(transaction);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileSellingRecords>(201, "Transaction Added Successfully", transaction));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        // POST api/<MobileSellingRecordsController>
        [HttpPost]
        [Route("CheckMonthlySalesReport/{brandName?}")]
        [ProducesResponseType(typeof(MonthlySalesResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckMonthlySalesReport(DateTime startDate, DateTime endDate, String? brandName = null)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MonthlySalesResponseModel>(404, "No transaction found.", null));
            try
            {
                DateTime _startDate = startDate == DateTime.MinValue ? DateTime.Now.AddDays(-30) : startDate;
                DateTime _endDate = endDate == DateTime.MinValue ? DateTime.Now : endDate;
                List<MobileSellingRecords> mobileTransactionFromDate = null;
                if (!string.IsNullOrEmpty(brandName))
                {
                    mobileTransactionFromDate = await _dbContext.MobileSellingRecords.Where(x => x.Selling_dt > _startDate && x.Selling_dt < _endDate && x.Brand == brandName).ToListAsync();
                }
                else
                {
                    mobileTransactionFromDate = await _dbContext.MobileSellingRecords.Where(x => x.Selling_dt > _startDate && x.Selling_dt < _endDate).ToListAsync();
                }
                if (mobileTransactionFromDate?.Count > 0)
                {
                    MonthlySalesResponseModel monthlySalesResponseModel = new MonthlySalesResponseModel();
                    monthlySalesResponseModel.MobileSellingRecords = mobileTransactionFromDate;
                    monthlySalesResponseModel.MonthlySalesDetails = new MonthlySalesDetails();
                    monthlySalesResponseModel.MonthlySalesDetails.TotalSellingAmount = mobileTransactionFromDate.Sum(x => Convert.ToInt64(x.Selling_price));
                    monthlySalesResponseModel.MonthlySalesDetails.TotalSoldDevices = Convert.ToInt64(mobileTransactionFromDate?.Count);
                    foreach (var transItem in mobileTransactionFromDate)
                    {
                        if (Convert.ToInt32(transItem.Selling_price) < Convert.ToInt32(transItem.Original_price))
                        {
                            monthlySalesResponseModel.MonthlySalesDetails.TotalLoss += Convert.ToInt32(transItem.Original_price) - Convert.ToInt32(transItem.Selling_price);
                        }
                        else
                        {
                            monthlySalesResponseModel.MonthlySalesDetails.TotalProfit += Convert.ToInt32(transItem.Selling_price) - Convert.ToInt32(transItem.Original_price);
                        }
                    }
                    List<HighlySoldDevicesInfo> highlySoldDevices = new List<HighlySoldDevicesInfo>();
                    var groupMobileDevicesByBrand = mobileTransactionFromDate.GroupBy(u => u.Brand).Select(grp => grp.ToList()).ToList();
                    foreach (var grpItem in groupMobileDevicesByBrand)
                    {
                        HighlySoldDevicesInfo highlySoldDevicesModel = new HighlySoldDevicesInfo();
                        highlySoldDevicesModel.BrandName = grpItem[0].Brand;
                        List<DeviceInfo> deviceInfoList = new List<DeviceInfo>();
                        foreach (var gItem in grpItem)
                        {
                            DeviceInfo deviceInfo = new DeviceInfo()
                            {
                                DeviceName = gItem.DeviceName,
                                Discount = gItem.Given_discount,
                                OriginalPrice = gItem.Original_price,
                                SoldPrice = gItem.Selling_price,
                                TransactionId = gItem.Transaction_Id,
                                Sold_dt = Convert.ToDateTime(gItem.Selling_dt),
                            };
                            deviceInfoList.Add(deviceInfo);
                        }
                        highlySoldDevicesModel.DeviceInfoList = deviceInfoList;
                        highlySoldDevices.Add(highlySoldDevicesModel);
                    }
                    monthlySalesResponseModel.MonthlySalesDetails.HighestSoldDevices = highlySoldDevices;
                    result = Ok(Common.ResponseMode<MonthlySalesResponseModel>(200, "Monthly transaction found.", monthlySalesResponseModel));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }



        [HttpPost]
        [Route("CheckProfitLossAsPerTimeLine/{brandName?}")]
        [ProducesResponseType(typeof(TimeLineResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckProfitLossAsPerTimeLine(DateTime timeLine1_startDate, DateTime timeLine1_endDate, DateTime timeLine2_startDate, DateTime timeLine2_endDate, String? brandName = null)
        {
            IActionResult result = BadRequest(Common.ResponseMode<TimeLineResponseModel>(404, "Please Enter Correct Timeline.", null));
            try
            {
                if(timeLine1_startDate > DateTime.MinValue && timeLine1_endDate > DateTime.MinValue && timeLine2_startDate > DateTime.MinValue && timeLine2_endDate > DateTime.MinValue)
                {
                    List<MobileSellingRecords> timeLine1 = null;
                    List<MobileSellingRecords> timeLine2 = null;
                    if (!string.IsNullOrEmpty(brandName))
                    {
                        timeLine1 = await _dbContext.MobileSellingRecords.Where(x => x.Selling_dt > timeLine1_startDate && x.Selling_dt < timeLine1_endDate && x.Brand == brandName).ToListAsync();
                    }
                    else
                    {
                        timeLine1 = await _dbContext.MobileSellingRecords.Where(x => x.Selling_dt > timeLine1_startDate && x.Selling_dt < timeLine1_endDate).ToListAsync();
                    }

                    if (!string.IsNullOrEmpty(brandName))
                    {
                        timeLine2 = await _dbContext.MobileSellingRecords.Where(x => x.Selling_dt > timeLine2_startDate && x.Selling_dt < timeLine2_endDate && x.Brand == brandName).ToListAsync();
                    }
                    else
                    {
                        timeLine2 = await _dbContext.MobileSellingRecords.Where(x => x.Selling_dt > timeLine2_startDate && x.Selling_dt < timeLine2_endDate).ToListAsync();
                    }

                    TimeLineResponseModel timeLineResponseModel = new TimeLineResponseModel();
                    timeLineResponseModel.TimeLine1 = GetTimeLineRecords(timeLine1);
                    timeLineResponseModel.TimeLine2 = GetTimeLineRecords(timeLine2);
                    result = Ok(Common.ResponseMode<TimeLineResponseModel>(200, "Monthly transaction found.", timeLineResponseModel));
                }

            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        private MonthlySalesDetails? GetTimeLineRecords(List<MobileSellingRecords> timeLine)
        {
            MonthlySalesDetails monthlySalesDetails = new MonthlySalesDetails();
            if (timeLine?.Count > 0)
            {
                monthlySalesDetails.TotalSellingAmount = timeLine.Sum(x => Convert.ToInt64(x.Selling_price));
                monthlySalesDetails.TotalSoldDevices = Convert.ToInt64(timeLine?.Count);
                foreach (var transItem in timeLine)
                {
                    if (Convert.ToInt32(transItem.Selling_price) < Convert.ToInt32(transItem.Original_price))
                    {
                        monthlySalesDetails.TotalLoss += Convert.ToInt32(transItem.Original_price) - Convert.ToInt32(transItem.Selling_price);
                    }
                    else
                    {
                        monthlySalesDetails.TotalProfit += Convert.ToInt32(transItem.Selling_price) - Convert.ToInt32(transItem.Original_price);
                    }
                }
                List<HighlySoldDevicesInfo> highlySoldDevices = new List<HighlySoldDevicesInfo>();
                var groupMobileDevicesByBrand = timeLine.GroupBy(u => u.Brand).Select(grp => grp.ToList()).ToList();
                foreach (var grpItem in groupMobileDevicesByBrand)
                {
                    HighlySoldDevicesInfo highlySoldDevicesModel = new HighlySoldDevicesInfo();
                    highlySoldDevicesModel.BrandName = grpItem[0].Brand;
                    List<DeviceInfo> deviceInfoList = new List<DeviceInfo>();
                    foreach (var gItem in grpItem)
                    {
                        DeviceInfo deviceInfo = new DeviceInfo()
                        {
                            DeviceName = gItem.DeviceName,
                            Discount = gItem.Given_discount,
                            OriginalPrice = gItem.Original_price,
                            SoldPrice = gItem.Selling_price,
                            TransactionId = gItem.Transaction_Id,
                            Sold_dt = Convert.ToDateTime(gItem.Selling_dt),
                        };
                        deviceInfoList.Add(deviceInfo);
                    }
                    highlySoldDevicesModel.DeviceInfoList = deviceInfoList;
                    highlySoldDevices.Add(highlySoldDevicesModel);
                }
                monthlySalesDetails.HighestSoldDevices = highlySoldDevices;
            }
            return monthlySalesDetails;
        }



        // PUT api/<MobileSellingRecordsController>/5
        [HttpPut]
        [Route("UpdateTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTransaction(MobileSellingRecords transaction)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileSellingRecords>(404, $"No Transaction found to update", transaction));
            try
            {
                MobileSellingRecords mobileTransaction = await _dbContext.MobileSellingRecords.FirstOrDefaultAsync(x => x.Id == transaction.Id);
                if (mobileTransaction != null)
                {
                    if (!string.IsNullOrEmpty(transaction.TransactionUpdated_at))
                    {
                        transaction.TransactionUpdated_at = DateTime.Now.ToString();
                    }
                    _dbContext.MobileSellingRecords.Update(mobileTransaction);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileSellingRecords>(200, $"{mobileTransaction.Transaction_Id} Updated Successfully", mobileTransaction));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        #endregion


        #region Dummy Records
        public static T GetJsonFileDataInModel<T>(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (StreamReader r = new StreamReader(fileInfo.FullName))
            {
                string readJson = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(readJson);
            }
        }

        private async Task CreateDummyRecords()
        {
            try
            {
                var devicesList = await _dbContext.MobileDevices.ToListAsync();
                Random rnd = new Random();
                var FirstNameDataList = GetJsonFileDataInModel<List<string>>(@"D:\DummyData\first-names.json");
                var LastNameDataList = GetJsonFileDataInModel<List<string>>(@"D:\DummyData\last-names.json");
                List<MobileSellingRecords> MobileSellingRecordsList = new List<MobileSellingRecords>();
                List<int> addedDeviceIds = new List<int>();
                bool isDiscount = false;
                for (int i = 0; i < 110000; i++)
                {
                    if (i % 2 == 0)
                    {
                        isDiscount = true;
                    }
                    else
                    {
                        isDiscount = false;
                    }
                    var rnddeviceItemId = rnd.Next(1, Convert.ToInt32(devicesList.Count));
                    var rndFirstName = FirstNameDataList[rnd.Next(1, Convert.ToInt32(FirstNameDataList.Count))];
                    var rndLastName = LastNameDataList[rnd.Next(1, Convert.ToInt32(LastNameDataList.Count))];
                    string byuerName = $"{rndFirstName} {rndLastName}";
                    var deviceItem = devicesList[rnddeviceItemId];
                    var isDiscountProvided = Convert.ToInt32(isDiscount);
                    var discountGiven = rnd.Next(5, Convert.ToInt32(deviceItem.Max_discount));
                    int dis_price = Convert.ToInt32(deviceItem.Price - (deviceItem.Price * discountGiven / 100));
                    var sellingDateaddedDays = DateTime.Now.AddDays(-(rnd.Next(0, 1000)));
                    var sellingDatewithAddedMinutes = sellingDateaddedDays.AddMinutes(rnd.Next(0, 1400));
                    MobileSellingRecords mobileSellingRecords = new MobileSellingRecords()
                    {
                        Buyer_name = byuerName,
                        Brand_id = deviceItem.Brand_id,
                        Brand = _dbContext.MobileBrands.FirstOrDefault(x => x.Brand_id == Convert.ToInt32(deviceItem.Brand_id)).Name,
                        DeviceName = deviceItem.Name,
                        Device_id = deviceItem.Device_id.ToString(),
                        IsDiscountApplied = Convert.ToBoolean(isDiscountProvided),
                        Max_discount = deviceItem.Max_discount.ToString(),
                        Given_discount = isDiscountProvided == 1 ? discountGiven.ToString() : isDiscountProvided.ToString(),
                        MaxDiscount_price = deviceItem.Dis_Price.ToString(),
                        IMEI_number = Guid.NewGuid().ToString(),
                        Original_price = deviceItem.Price.ToString(),
                        Selling_dt = sellingDatewithAddedMinutes,
                        Selling_price = isDiscountProvided == 1 ? dis_price.ToString() : deviceItem.Price.ToString(),
                    };
                    
                    var addedDeviceIdCount = addedDeviceIds.Where(x=> x == deviceItem.Device_id).ToList().Count;
                    if (addedDeviceIdCount < deviceItem.Quantity)
                    {
                        MobileSellingRecordsList.Add(mobileSellingRecords);
                         addedDeviceIds.Add(deviceItem.Device_id);
                    }
                }
                await _dbContext.MobileSellingRecords.AddRangeAsync(MobileSellingRecordsList);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

    }
}
