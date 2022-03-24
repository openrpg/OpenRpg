namespace OpenRpg.CurveFunctions.Curves
{
    public class PassThroughCurveFunction : ICurveFunction
    {
        public float Plot(float value)
        { return value; }
    }
}