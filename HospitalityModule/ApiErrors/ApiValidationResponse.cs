namespace HospitalityModules.Errors
{
    public class ApiValidationResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationResponse(int StautsCode) : base(StautsCode)
        {
            Errors = new List<string>();
        }
    }
}
