namespace CromWood.Business.Helper
{
    public static class ResponseCreater<T>
    {
        public static AppResponse<T> CreateSuccessResponse(T data, string message = "Successful")
        {
            return new AppResponse<T>
            {
                StatusCode = 200,
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static AppResponse<T> CreateErrorResponse(T data, string message = "Error")
        {
            return new AppResponse<T>
            {
                StatusCode = 500,
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static AppResponse<T> CreateNotFoundResponse(T data, string message = "Not Found")
        {
            return new AppResponse<T>
            {
                StatusCode = 404,
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static AppResponse<T> CreateDuplicateExistResponse(T duplicateKeys, string message = "Already exists")
        {
            return new AppResponse<T>
            {
                StatusCode = 409,
                Success = false,
                Message = message,
                Data = duplicateKeys
            };
        }
    }
}
