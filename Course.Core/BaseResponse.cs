using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Course.Core
{
    public class BaseResponse<T>(T? data, string message, List<string> errors = null, bool success = true)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; } = errors;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; set; } = data;
    }

}
