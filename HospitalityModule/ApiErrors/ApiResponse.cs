namespace HospitalityModules.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public ApiResponse(int _StatusCode, string? _ErrorMessage = null)
        {
            StatusCode = _StatusCode;
            ErrorMessage = _ErrorMessage ?? GetErrorMessage(_StatusCode);

        }

        public string? GetErrorMessage(int StatusCode)
        {
            return StatusCode switch
            {
                404 => "Resource was not found",
                400 => "A bad Request , you have made",
                401 => "Authroized, you are not",
                500 => "Server Error",

            };

        }
    }
}
