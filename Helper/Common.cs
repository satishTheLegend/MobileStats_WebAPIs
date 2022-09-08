using MobileStats_WebAPIs.Models;

namespace MobileStats_WebAPIs.Helper
{
    public class Common
    {
        #region Properties

        #endregion

        #region Public Methods
        public static ResponseModel<T> ResponseMode<T>(int statusCode, string Message, T model)
        {
            return new ResponseModel<T>(statusCode, Message, model);
        }

        #endregion
    }
}
