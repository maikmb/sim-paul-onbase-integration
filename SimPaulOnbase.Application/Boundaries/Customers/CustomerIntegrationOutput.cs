namespace SimPaulOnbase.Application.Boundaries.Customers
{
    public class CustomerIntegrationOutput
    {
        public CustomerIntegrationOutput(int integratedCount)
        {
            IntegratedCount = integratedCount;
        }

        /// <summary>
        /// Total of customers integrated with onbase
        /// </summary>
        public int IntegratedCount { get; set; }
    }
}
