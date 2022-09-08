using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStats_WebAPIs.DBContext;
using MobileStats_WebAPIs.Helper;
using MobileStats_WebAPIs.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobileStats_WebAPIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MobileBrandsController : ControllerBase
    {
        #region Properties
        public ApplicationDBContext _dbContext;
        #endregion

        #region Constructor
        public MobileBrandsController(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        #endregion

        // GET: api/<MobileBrandsController>
        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(List<MobileBrands>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllBrands()
        {
            List<MobileBrands> mobileBrandsList = await _dbContext.MobileBrands.ToListAsync();
            return mobileBrandsList == null ? NotFound(Common.ResponseMode<List<MobileBrands>>(404, "No Mobile Brands Found", mobileBrandsList == null ? new List<MobileBrands>() : mobileBrandsList)) : Ok(Common.ResponseMode<List<MobileBrands>>(200, $"{mobileBrandsList?.Count} Records found", mobileBrandsList));
        }

        //GET api/<MobileBrandsController>/5
        [HttpGet]
        [Route("GetBrandById")]
        [ProducesResponseType(typeof(MobileBrands), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBrandById(int id)
        {
            MobileBrands mobileBrand = await _dbContext.MobileBrands.FirstOrDefaultAsync(x => x.Id == id);
            return mobileBrand == null ? NotFound(Common.ResponseMode<MobileBrands>(404, "No Mobile Brands Found", mobileBrand == null ? new MobileBrands() : mobileBrand)) : Ok(Common.ResponseMode<MobileBrands>(200, $"{mobileBrand.Name} Found", mobileBrand));
        }

        // POST api/<MobileBrandsController>
        [HttpPost]
        [Route("AddBrand")]
        [ProducesResponseType(typeof(MobileBrands), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBrand(string brandName)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileBrands>(404, "Brand already Exist", new MobileBrands()));
            try
            {
                MobileBrands mobileBrand = await _dbContext.MobileBrands.FirstOrDefaultAsync(x => x.Name == brandName);
                if (mobileBrand == null)
                {
                    MobileBrands brand = new MobileBrands()
                    {
                        Name = brandName,
                        Added_dt = DateTime.Now.ToString(),
                    };
                    await _dbContext.MobileBrands.AddAsync(brand);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileBrands>(201, "Brand Added Successfully", brand));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        // PUT api/<MobileBrandsController>/5
        [HttpPut]
        [Route("UpdateBrand")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBrand(int brandId, string brandName)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileBrands>(404, $"No Brand found with this id : {brandId}", new MobileBrands()));
            try
            {
                MobileBrands mobileBrand = await _dbContext.MobileBrands.FirstOrDefaultAsync(x => x.Id == brandId);
                if (mobileBrand != null)
                {
                    mobileBrand.Name = brandName;
                    mobileBrand.Updated_dt = DateTime.Now.ToString();
                    _dbContext.MobileBrands.Update(mobileBrand);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileBrands>(201, $"{brandName} Updated Successfully", mobileBrand));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }

        // DELETE api/<MobileBrandsController>/5
        [HttpDelete]
        [Route("DeleteBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int brandId)
        {
            IActionResult result = BadRequest(Common.ResponseMode<MobileBrands>(404, $"No Brand found with this Id : {brandId}", new MobileBrands()));
            try
            {
                MobileBrands mobileBrand = await _dbContext.MobileBrands.FirstOrDefaultAsync(x => x.Id == brandId);
                if (mobileBrand != null)
                {
                    _dbContext.MobileBrands.Remove(mobileBrand);
                    await _dbContext.SaveChangesAsync();
                    result = Ok(Common.ResponseMode<MobileBrands>(200, $"{mobileBrand.Name} Removed Successfully", mobileBrand));
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(Common.ResponseMode<Exception>(404, "Error occured", ex));
            }
            return result;
        }
    }
}
