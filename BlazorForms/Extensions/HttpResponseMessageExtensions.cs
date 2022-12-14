using BlazorForms.Models;
using BlazorForms.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorForms.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<(ResponseStatus status, string data, List<string> errors)> ToClientResponse(this HttpResponseMessage response)
        {
            ResponseStatus status = ResponseStatus.Success;
            string data = null;
            List<string> errors = null;

            var statusCode = (int)response.StatusCode;
            string result = null;

            if (statusCode != 401 && statusCode != 403)
            {
                result = await response.Content.ReadAsStringAsync();
            }

            switch (statusCode)
            {
                case 200:
                case 201:
                    status = ResponseStatus.Success;
                    data = result;
                    break;
                case 400:
                    status = ResponseStatus.Error;
                    errors = ProcessResponse400(result);
                    break;
                case 401:
                    status = ResponseStatus.Unauthorized;
                    errors = ProcessResponse401();
                    break;
                case 403:
                    status = ResponseStatus.Forbidden;
                    errors = ProcessResponse403();
                    break;
                case 500:
                    status = ResponseStatus.Error;
                    errors = ProcessResponse500(result);
                    break;
                default:
                    break;
            }

            return (status, data, errors);
        }

        public static (ResponseStatus status, string data, List<string> errors) ToClientResponseV2(this HttpResponseMessage response)
        {
            ResponseStatus status = ResponseStatus.Success;
            string data = null;
            List<string> errors = null;

            var statusCode = (int)response.StatusCode;
            string result = null;
            if (statusCode != 401 && statusCode != 403)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            
            switch (statusCode)
            {
                case 200:
                case 201:
                    status = ResponseStatus.Success;
                    data = result;
                    break;
                case 400:
                    status = ResponseStatus.Error;
                    errors = ProcessResponse400(result);
                    break;
                case 401:
                    status = ResponseStatus.Unauthorized;
                    errors = ProcessResponse401();
                    break;
                case 403:
                    status = ResponseStatus.Forbidden;
                    errors = ProcessResponse403();
                    break;
                case 500:
                    status = ResponseStatus.Error;
                    errors = ProcessResponse500(result);
                    break;
                default:
                    break;
            }

            return (status, data, errors);
        }

        private static List<string> ProcessResponse400(string result)
        {
            List<string> errors = new List<string>();

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
            var errorsCount = (errorResponse.Errors as JObject).Count;

            if (errorResponse.Title.Contains("One or more validation errors occurred.") && errorsCount > 0)
            {
                foreach (var error in errorResponse.Errors)
                {
                    var asd = (error as JProperty).Value;

                    foreach (var item in asd)
                    {
                        errors.Add(item.Value<string>());
                    }
                }
            }

            return errors;
        }

        private static List<string> ProcessResponse401()
        {
            return new List<string> { "User is not Authorized" };
        }

        private static List<string> ProcessResponse403()
        {
            return new List<string> { "User does not permision to perform this action" };
        }

        private static List<string> ProcessResponse500(string result)
        {
            List<string> errors = new List<string>();
            errors.Add(result);

            return errors;
        }
    }
}
