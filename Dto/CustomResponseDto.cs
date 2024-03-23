using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        //[JsonIgnore]
        public int StatusCode { get; set; }
        public static CustomResponseDto<T> Success(int StatusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = StatusCode, Data = data };
        }
        public static CustomResponseDto<T> Success(int StatusCode)
        {
            return new CustomResponseDto<T> { StatusCode = StatusCode };
        }
        public static CustomResponseDto<T> Fail(List<string> Errors, int StatusCode)
        {
            return new CustomResponseDto<T> { StatusCode = StatusCode, Errors = Errors };
        }
        public static CustomResponseDto<T> Fail(int StatusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = StatusCode, Errors = new List<string> { error } };
        }
        public static CustomResponseDto<T> Fail(int StatusCode, string error, T data)
        {
            return new CustomResponseDto<T> { StatusCode = StatusCode, Errors = new List<string> { error }, Data = data };
        }
    }
    public class CustomResponseNullDto
    {
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public static CustomResponseNullDto Success(int StatusCode)
        {
            return new CustomResponseNullDto { StatusCode = StatusCode };
        }
        public static CustomResponseNullDto Fail(List<string> Errors, int StatusCode)
        {
            return new CustomResponseNullDto { StatusCode = StatusCode, Errors = Errors };
        }
        public static CustomResponseNullDto Fail(int StatusCode, string error)
        {
            return new CustomResponseNullDto { StatusCode = StatusCode, Errors = new List<string> { error } };
        }
    }
}
