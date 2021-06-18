namespace OpenRpg.CurveFunctions
{
    /// <summary>
    /// A curve function allows you to express a curve and plot normalized values against the curve
    /// </summary>
    /// <remarks>The values passed in should always be between 0-1 and returns values between 0-1</remarks>
    public interface ICurveFunction
    {
        /// <summary>
        /// This plots the given value on the curve returning the result
        /// </summary>
        /// <param name="value">The value to plot between 0-1</param>
        /// <returns>The value from the graph between 0-1</returns>
        float Plot(float value);
    }
}