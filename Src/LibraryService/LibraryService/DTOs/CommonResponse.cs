namespace LibraryService.DTOs
{
   
    public class CommonResponse
    {
        public dynamic Data { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public List<ErrorDetail> Error { get; set; }
    }

    public class ErrorDetail
    {
        public string? Message { get; set; }
        public string? Description { get; set; }
        public int ErrorId { get; set; }
    }
}
