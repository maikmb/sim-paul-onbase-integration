namespace SimPaulOnbase.Application.Boundaries.Customers
{
    /// <summary>
    /// OutputPort interface
    /// </summary>
    public interface IOutputPort
    {
        /// <summary>
        /// Handle method
        /// </summary>
        /// <param name="output">CustomerIntegrationOutput</param>
        void Handle(CustomerIntegrationOutput output);
    }
}
