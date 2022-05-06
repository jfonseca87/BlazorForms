using BlazorForms.Utils;
using System.Collections.Generic;

namespace BlazorForms.Models
{
    public class ApiResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
