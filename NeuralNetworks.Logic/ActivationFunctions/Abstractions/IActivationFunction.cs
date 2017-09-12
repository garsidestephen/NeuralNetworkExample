namespace NeuralNetworks.Logic.ActivationFunctions.Abstractions
{
    /// <summary>
    /// Activation Function Interface
    /// </summary>
    public interface IActivationFunction
    {
        /// <summary>
        /// Activation Output
        /// </summary>
        /// <param name="x">The Value</param>
        /// <returns>Activated Value</returns>
        double output(double x);

        /// <summary>
        /// The Activation Derivative
        /// </summary>
        /// <param name="x">The Value</param>
        /// <returns>Activation Derivative</returns>
        double derivative(double x);
    }
}
