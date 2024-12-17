namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }

    public ApiResponseWithData(T data)
    {
        Data = data;
        Success = true;
    }

    public ApiResponseWithData()
    {
        Success = true;
    }
}