using OpenRpg.CurveFunctions.Curves;

namespace OpenRpg.CurveFunctions
{
    public class PresetCurves
    {
        public static PolynomialCurveFunction Constant = new PolynomialCurveFunction(0f, 0, 0.5f, 0);
        public static PolynomialCurveFunction Linear = new PolynomialCurveFunction(1.0f, 0, 0, 1.0f);
        public static PolynomialCurveFunction InverseLinear = new PolynomialCurveFunction(-1.0f, 1.0f, 0, 1.0f);
        public static PolynomialCurveFunction StandardCooldown = new PolynomialCurveFunction(1.0f, 0f, 0, 6.0f);
        public static PolynomialCurveFunction StandardRuntime = new PolynomialCurveFunction(-1.0f, 0f, 1.0f, 6.0f);
        public static PolynomialCurveFunction QuadraticLowerLeft = new PolynomialCurveFunction(1.0f, 1.0f, 0.0f, 4.0f);
        public static PolynomialCurveFunction QuadraticLowerRight = new PolynomialCurveFunction(1.0f, 0.0f, 0.0f, 4.0f);
        public static PolynomialCurveFunction QuadraticUpperLeft = new PolynomialCurveFunction(-1.0f, 1.0f, 1.0f, 4.0f);
        public static PolynomialCurveFunction QuadraticUpperRight = new PolynomialCurveFunction(-1.0f, 0f, 1.0f, 4.0f);
        public static LogisticCurveFunction Logistic = new LogisticCurveFunction(1.0f, 0f, 0.0f, 1.0f);
        public static LogisticCurveFunction InverseLogistic = new LogisticCurveFunction(-1.0f, 0f, 1.0f, 1.0f);
        public static LogitCurveFunction Logit = new LogitCurveFunction(1.0f, 0, 0);
        public static LogitCurveFunction InverseLogit = new LogitCurveFunction(-1.0f, 0, 0);
        public static NormalCurveFunction BellCurve = new NormalCurveFunction(1.0f, 0, 0, 1.0f);
        public static NormalCurveFunction InverseBellCurve = new NormalCurveFunction(-1.0f, 0, 1.0f, 1.0f);
        public static SineCurveFunction SineWave = new SineCurveFunction(1.0f, 0, 0);
        public static SineCurveFunction InverseSineWave = new SineCurveFunction(-1.0f, 0, 0);
        public static StepCurveFunction GreaterThanHalf = new StepCurveFunction(0.5f);
        public static StepCurveFunction LessThanHalf = new StepCurveFunction(0.5f, 1.0f, 0.0f);
        public static PassThroughlCurveFunction PassThrough = new PassThroughlCurveFunction();
    }
}