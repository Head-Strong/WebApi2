namespace Dto
{
    /// <summary>
    /// Address Details
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Address Id 
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Customer Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Pin. Required Field
        /// </summary>
        public string Pin { get; set; }        
    }
}