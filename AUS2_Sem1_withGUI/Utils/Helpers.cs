namespace AUS2_Sem1_withGUI.Utils
{
    public static class Helpers
    {
        public static Position CharToPosition(this char c)
        {
            c = char.ToUpper(c);

            switch (c)
            {
                case 'N':
                    return Position.North;
                case 'S':
                    return Position.South;
                case 'E':
                    return Position.East;
                case 'W':
                    return Position.West;
                default:
                    return Position.Unknown;
            }
        } 
    }

    #region MathOperations
    public interface IMathOperations<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
    }

    public class MathOperationsInt : IMathOperations<int>
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public int Divide(int a, int b) => a / b;
    }

    public class MathOperationsDouble : IMathOperations<double>
    {
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b) => a / b;
    }
    #endregion
}
