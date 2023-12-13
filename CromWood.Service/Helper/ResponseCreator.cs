namespace CromWood.Business.Helper
{
    public static class ResponseCreater<T>
    {
        public static Response<T> CreateSuccessResponse(T data, string message = "Successful")
        {
            return new Response<T>
            {
                StatusCode = 200,
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static Response<T> CreateErrorResponse(T data, string message = "Error")
        {
            return new Response<T>
            {
                StatusCode = 500,
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static Response<T> CreateNotFoundResponse(T data, string message = "Not Found")
        {
            return new Response<T>
            {
                StatusCode = 404,
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static Response<T> CreateDuplicateExistResponse(T duplicateKeys, string message = "Already exists")
        {
            return new Response<T>
            {
                StatusCode = 409,
                Success = false,
                Message = message,
                Data = duplicateKeys
            };
        }
    }
}
