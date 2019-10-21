namespace SimPaulOnbase.Core.Customers
{
    /// <summary>
    /// Customer class
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer document
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Customer e-mail
        /// </summary>
        public string Email { get; set; }
       
        /// <summary>
        /// Customer mother name
        /// </summary>
        public string Mother { get; set; }

        /// <summary>
        /// Customer photo represented by base64 data
        /// </summary>
        public string Photo { get; set; }
    }
}
