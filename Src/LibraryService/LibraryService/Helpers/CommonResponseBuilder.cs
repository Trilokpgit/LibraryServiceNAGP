using LibraryService.DTOs;

namespace LibraryService.Helpers
{
    public static class CommonResponseBuilder
    {
        public static CommonResponse BuildResponse(bool isSuccess, int statusCode, dynamic data, ErrorDetail? errorDetail = null)
        {
            CommonResponse response = new();
            response.StatusCode = statusCode;
            response.Data = data;
            response.Success = isSuccess;
            if (!isSuccess)
            {
                response.Error = new List<ErrorDetail>() { errorDetail ?? new() };
            }
            return response;
        }
    }
}
