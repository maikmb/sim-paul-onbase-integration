namespace SimPaulOnbase.Infraestructure.Services
{
    /// <summary>
    /// OnbaseSettings class
    /// </summary>
    public class OnbaseSettings
    {
        /// <summary>
        /// Form Template Id
        /// </summary>
        public int FormIntegrationID { get; set; }

        /// <summary>
        /// App server url
        /// </summary>
        public string AppServerURL { get; set; }

        /// <summary>
        /// App server user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// App server user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// App server data source
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// Customer onbase document type description
        /// </summary>
        public string CustomerDocumentType { get; set; }

        /// <summary>
        /// Customer onbase document file type description
        /// </summary>
        public string CustomerDocumentFileType { get; set; }
    }
}
