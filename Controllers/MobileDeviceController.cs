using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStats_WebAPIs.DBContext;
using MobileStats_WebAPIs.Helper;
using MobileStats_WebAPIs.Models;
using Newtonsoft.Json;

namespace MobileStats_WebAPIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MobileDeviceController : Controller
    {
        #region Properties
        public ApplicationDBContext _dbContext;
        #endregion

        #region Constructor
        public MobileDeviceController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region API Methods

        //GET: api/<MobileDevicesController>
        [HttpGet]
        [Route("GetAllDevices")]
        [ProducesResponseType(typeof(List<MobileDevices>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllDevices()
        {
            //await AddDummyData();
            List<MobileDevices> MobileDevicesList = await _dbContext.MobileDevices.ToListAsync();
            return MobileDevicesList == null ? NotFound(Common.ResponseMode<List<MobileDevices>>(404, "No Mobile Devices Found", MobileDevicesList == null ? new List<MobileDevices>() : MobileDevicesList)) : Ok(Common.ResponseMode<List<MobileDevices>>(200, $"{MobileDevicesList?.Count} Records found", MobileDevicesList));
        }

        //GET api/<MobileDevicesController>/5
        [HttpGet]
        [Route("GetDeviceById")]
        [ProducesResponseType(typeof(MobileDevices), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            MobileDevices mobileDevice = await _dbContext.MobileDevices.FirstOrDefaultAsync(x => x.Id == id);
            return mobileDevice == null ? NotFound(Common.ResponseMode<MobileDevices>(404, "No Mobile Devices Found", mobileDevice == null ? new MobileDevices() : mobileDevice)) : Ok(Common.ResponseMode<MobileDevices>(200, $"{mobileDevice.Name} Found", mobileDevice));
        }

        // POST api/<MobileDevicesController>
        [HttpPost]
        [Route("AddDevice")]
        [ProducesResponseType(typeof(MobileDevices), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDevice(MobileDevices device)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileDevices>(404, "Devices already Exist", device));
            try
            {
                MobileDevices mobileDevice = await _dbContext.MobileDevices.FirstOrDefaultAsync(x => x.Name == device.Name);
                if (mobileDevice == null)
                {
                    if(!string.IsNullOrEmpty(device.Created_at))
                    {
                        device.Created_at = DateTime.Now.ToString();
                    }
                    if(device.Quantity == 0)
                    {
                        device.Quantity = 1;
                    }
                    await _dbContext.MobileDevices.AddAsync(device);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileDevices>(201, "Device Added Successfully", device));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        // PUT api/<MobileDevicesController>/5
        [HttpPut]
        [Route("UpdateDevice")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDevice(MobileDevices device)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileDevices>(404, $"No Device found to update", device));
            try
            {
                MobileDevices mobileDevice = await _dbContext.MobileDevices.FirstOrDefaultAsync(x => x.Id == device.Id);
                if (mobileDevice != null)
                {
                    if(!string.IsNullOrEmpty(device.Updated_at))
                    {
                        device.Updated_at = DateTime.Now.ToString();
                    }
                    _dbContext.MobileDevices.Update(mobileDevice);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileDevices>(201, $"{mobileDevice.Name} Updated Successfully", mobileDevice));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        // DELETE api/<MobileDevicesController>/5
        [HttpDelete]
        [Route("DeleteDevice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int deviceId)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileDevices>(404, $"No Device found with this Id : {deviceId}", new MobileDevices()));
            try
            {
                MobileDevices mobileDevice = await _dbContext.MobileDevices.FirstOrDefaultAsync(x => x.Id == deviceId);
                if (mobileDevice != null)
                {
                    _dbContext.MobileDevices.Remove(mobileDevice);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileDevices>(200, $"{mobileDevice.Name} Removed Successfully", mobileDevice));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }
        #endregion

        #region Dummy Data Records

        #region Demo Data Add
        public async Task<int> AddDummyData()
        {
            try
            {

                DirectoryInfo fileDir = new DirectoryInfo(@"D:\DummyData");
                FileInfo brandsInfo = new FileInfo(Path.Combine(fileDir.FullName, "brands.json"));
                FileInfo devicesInfo = new FileInfo(Path.Combine(fileDir.FullName, "devices.json"));

                var brandsData = GetJsonFileDataInModel<brandRecords>(brandsInfo.FullName);
                var deviceData = GetJsonFileDataInModel<DevicesRecord>(devicesInfo.FullName);

                List<MobileBrands> brandsList = new List<MobileBrands>();
                foreach (var brandItem in brandsData.RECORDS)
                {
                    MobileBrands brands = new MobileBrands
                    {
                        Brand_id = Convert.ToInt32(brandItem.id),
                        Added_dt = DateTime.Now.ToString(),
                        Name = brandItem.name,
                    };
                    brandsList.Add(brands);
                }

                await _dbContext.MobileBrands.AddRangeAsync(brandsList);
                await _dbContext.SaveChangesAsync();

                Random rnd = new Random();
                List<MobileDevices> devicesList = new List<MobileDevices>();
                foreach (var deviceItem in deviceData.RECORDS)
                {
                    int maxDiscount = rnd.Next(10, 40);
                    int price = rnd.Next(15000, 90000);
                    int quantity = rnd.Next(5, 30);
                    int dis_price = price - (price * maxDiscount / 100);
                    MobileDevices devices = new MobileDevices()
                    {
                        Battery_size = deviceItem.battery_size,
                        Battery_type = deviceItem.battery_type,
                        Body = deviceItem.body,
                        Brand_id = deviceItem.brand_id,
                        Camera_pixels = deviceItem.camera_pixels,
                        Chipset = deviceItem.chipset,
                        Created_at = DateTime.Now.ToString(),
                        Display_resolution = deviceItem.display_resolution,
                        Display_size = deviceItem.display_size,
                        Device_id = Convert.ToInt32(deviceItem.id),
                        Max_discount = maxDiscount,
                        Name = deviceItem.name,
                        Os = deviceItem.os,
                        Picture = deviceItem.picture,
                        Price = price,
                        Quantity = quantity,
                        Ram = deviceItem.ram,
                        Released_at = deviceItem.released_at,
                        Specifications = deviceItem.specifications,
                        Storage = deviceItem.storage,
                        Url_hash = deviceItem.url_hash,
                        Video_pixels = deviceItem.video_pixels,
                        Dis_Price = dis_price,
                    };
                    devicesList.Add(devices);
                }

                await _dbContext.MobileDevices.AddRangeAsync(devicesList);
                await _dbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        public static T GetJsonFileDataInModel<T>(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (StreamReader r = new StreamReader(fileInfo.FullName))
            {
                string readJson = r.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(readJson);
            }
        }

        #endregion

        #region Models
        public class Brand
        {
            public string id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
            public string deleted_at { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class brandRecords
        {
            public List<Brand> RECORDS { get; set; }
        }

        public class Device
        {
            public string id { get; set; }
            public string url_hash { get; set; }
            public string brand_id { get; set; }
            public string name { get; set; }
            public string picture { get; set; }
            public string released_at { get; set; }
            public string body { get; set; }
            public string os { get; set; }
            public string storage { get; set; }
            public string display_size { get; set; }
            public string display_resolution { get; set; }
            public string camera_pixels { get; set; }
            public string video_pixels { get; set; }
            public string ram { get; set; }
            public string chipset { get; set; }
            public string battery_size { get; set; }
            public string battery_type { get; set; }
            public string specifications { get; set; }
            public string deleted_at { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }

        public class DevicesRecord
        {
            public List<Device> RECORDS { get; set; }
        }

        #endregion

        #endregion
    }

}
