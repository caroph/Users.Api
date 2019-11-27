using System;

namespace Users.Infra.DefaultObjects
{
    public class ResultObject<T>
    {
        public ResultObject(bool success) => Success = success;

        public ResultObject(bool success, T data)
        {
            Success = success;
            Data = data;
        }

        public ResultObject(bool success, T data, DateTime dateTime)
        {
            Success = success;
            Data = data;
            LastModified = dateTime;
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public DateTime LastModified { get; set; }
        public string Message { get; set; }
    }
}
