namespace CarCatalogue.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse(bool isSuccess = true)
        {
            this.IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public T? Content { get; set; }
    }
}
