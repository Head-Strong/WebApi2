using System.Collections.Generic;

namespace Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Error Category
        /// Business
        /// Authorization
        /// Authentication
        /// Internal Server Error
        /// </summary>
        public string ErrorCategory { get; set; }   

        /// <summary>
        /// Complete Descriptions of Error
        /// </summary>
        public List<string> ErrorDescriptions { get; set; }

        /// <summary>
        /// Serialized the data
        /// </summary>
        /// <returns></returns>
        //public override string ToString() => this.ToString();
    }
}
