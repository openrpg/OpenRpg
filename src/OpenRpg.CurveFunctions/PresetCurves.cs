using OpenRpg.CurveFunctions.Curves;

namespace OpenRpg.CurveFunctions
{
    public class PresetCurves
    {
        public static ICurveFunction Constant = new PolynomialCurveFunction(0f, 0, 0.5f, 0);
        public static ICurveFunction Linear = new PolynomialCurveFunction(1.0f, 0, 0, 1.0f);
        public static ICurveFunction InverseLinear = new PolynomialCurveFunction(-1.0f, 1.0f, 0, 1.0f);
        public static ICurveFunction StandardCooldown = new PolynomialCurveFunction(1.0f, 0f, 0, 6.0f);
        public static ICurveFunction StandardRuntime = new PolynomialCurveFunction(-1.0f, 0f, 1.0f, 6.0f);
        public static ICurveFunction QuadraticLowerLeft = new PolynomialCurveFunction(1.0f, 1.0f, 0.0f, 4.0f);
        public static ICurveFunction QuadraticLowerRight = new PolynomialCurveFunction(1.0f, 0.0f, 0.0f, 4.0f);
        public static ICurveFunction QuadraticUpperLeft = new PolynomialCurveFunction(-1.0f, 1.0f, 1.0f, 4.0f);
        public static ICurveFunction QuadraticUpperRight = new PolynomialCurveFunction(-1.0f, 0f, 1.0f, 4.0f);
        public static ICurveFunction Logistic = new LogisticCurveFunction(1.0f, 0f, 0.0f, 1.0f);
        public static ICurveFunction InverseLogistic = new LogisticCurveFunction(-1.0f, 0f, 1.0f, 1.0f);
        public static ICurveFunction Logit = new LogitCurveFunction(1.0f, 0, 0);
        public static ICurveFunction InverseLogit = new LogitCurveFunction(-1.0f, 0, 0);
        public static ICurveFunction BellCurve = new NormalCurveFunction(1.0f, 0, 0, 1.0f);
        public static ICurveFunction InverseBellCurve = new NormalCurveFunction(-1.0f, 0, 1.0f, 1.0f);
        public static ICurveFunction SineWave = new SineCurveFunction(1.0f, 0, 0);
        public static ICurveFunction InverseSineWave = new SineCurveFunction(-1.0f, 0, 0);
        public static ICurveFunction GreaterThanHalf = new StepCurveFunction(0.5f);
        public static ICurveFunction LessThanHalf = new StepCurveFunction(0.5f, 1.0f, 0.0f);
        public static ICurveFunction PassThrough = new PassThroughlCurveFunction();
    }
}