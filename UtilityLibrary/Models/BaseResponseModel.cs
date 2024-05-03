#region Imports
using System.Net;
using System.Text.Json.Serialization;
#endregion

namespace UtilityLibrary.Models
{
    public class BaseResponseModel
    {
        #region Errors Container
        public bool IsSuccess
        {
            get
            {
                if (Errors.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public List<string> Errors { get; set; } = new List<string>();
        #endregion

        #region Response Error Code
        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
        #endregion

        #region Public Methods
        public void AddErrorWithStatusCode(string errorText, HttpStatusCode httpStatusCode)
        {
            Errors.Add(errorText);
            HttpStatusCode = httpStatusCode;
        }
        #endregion
    }
}
