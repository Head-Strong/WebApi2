﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntegrationTest.Response
{
    public class ErrorDto
    {
        [JsonProperty("ErrorDescriptions")]
        public List<string> ErrorDescriptions { get; set; }

        [JsonProperty("ErrorCategory")]
        public string ErrorCategory { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}