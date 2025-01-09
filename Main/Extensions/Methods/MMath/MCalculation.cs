namespace YNL.Utilities.Extensions
{
    public static class MCalculation
    {
        #region Plus Operator +
        public static int Plus(this int input, int value) => input + value;
        public static int RefPlus(this ref int input, int value) => input += value;

        public static float Plus(this float input, float value) => input + value;
        public static float RefPlus(this ref float input, float value) => input += value;
        #endregion

        #region Minus Operator -
        public static float Minus(this float input, float value) => input - value;
        public static float RefMinus(this ref float input, float value) => input -= value;
        #endregion

        #region Multiply Operator *
        public static float Multiply(this float input, float value) => input * value;
        public static float RefMultiply(this ref float input, float value) => input *= value;
        #endregion

        #region Divide Operator /
        public static float Divide(this float input, float value) => input / value;
        public static float RefDivides(this ref float input, float value) => input /= value;
        #endregion

        #region Modulo Operator %
        public static float Modulo(this float input, float value) => input % value;
        public static float RefModulo(this ref float input, float value) => input %= value;
        #endregion
    }
}