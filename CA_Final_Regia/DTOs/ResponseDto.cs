namespace CA_Final_Regia.DTOs
{
    public class ResponseDto<T> where T : class
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Special { get; set; }
        public T Object { get; set; }
        public IList<T> List { get; set; }
        public Status StatusCode { get; set; }

        public ResponseDto(bool isSuccess, string message, Status statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }
        public ResponseDto(bool isSuccess, string message, Status statusCode, string special)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
            Special = special;
        }

        public ResponseDto(bool isSuccess, T objectToReturn, Status statusCode)
        {
            IsSuccess = isSuccess;
            Object = objectToReturn;
            StatusCode = statusCode;
        }

        public ResponseDto(bool isSuccess, IList<T> listToReturn, Status statusCode)
        {
            IsSuccess = isSuccess;
            List = listToReturn;
            StatusCode = statusCode;
        }
        public enum Status
        {
            Ok = 200,
            Created = 201,
            No_Content = 204,
            Bad_Request = 400,
            Not_Found = 404,
            Internal_Server_Error = 500
        }
    }
}
