namespace OpenRpg.CurveFunctions.Extensions
{
    public static class ICurveFunctionExtensions
    {
        public static float SanitizeValue(this ICurveFunction curve, float value)
        {
            if(float.IsInfinity(value)) { return 0.0f; }
            if(float.IsNaN(value)) { return 0.0f; }
            if(value < 0 ) { return 0.0f; }
            if(value > 1.0f ) { return 1.0f; }
            return value;
        }
    }
}