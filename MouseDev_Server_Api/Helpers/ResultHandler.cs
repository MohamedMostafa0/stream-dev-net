using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MouseDev_Server_Api.Helpers
{
    public static class ResultHandler
    {
        public async static Task<JsonResult> Success()
        {
            return await Task.FromResult(new JsonResult(new { success = true }));
        }
        public async static Task<JsonResult> Success(object objToJson)
        {
            return await Task.FromResult(new JsonResult(new {
                success = true , 
                result = objToJson
            }));
        }
    }
}
