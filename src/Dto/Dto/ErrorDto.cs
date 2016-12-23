using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dto
{
    public class ErrorDto
    {
        public string ErrorCategory { get; set; }   

        public List<string> ErrorDescriptions { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
