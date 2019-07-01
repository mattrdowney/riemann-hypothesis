using System.Numerics;
using UnityEngine;

public class RiemannHypothesis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Exact form: " + test_output);
        Debug.Log("Basic implementation: " + riemann_zeta_function(test_input));
        Debug.Log("Novel implementation: " + iterative_riemann_zeta_function(test_input));
    }

    public Complex riemann_zeta_function(Complex s)
    {
        Complex result = 0;
        for (int exponent_base = 1; exponent_base < infinity; exponent_base += 1)
        {
            result += 1/Complex.Pow(exponent_base, s);
        }
        return result;
    }

    public Complex iterative_riemann_zeta_function(Complex s) // Maybe I built this in the opposite direction on top of my other errors? -- getting NaN
    {
        Complex numerator = 1;
        Complex denominator = Complex.Pow(1, s);
        for (int exponent_base = 2; exponent_base < infinity; exponent_base += 1)
        {
            Complex next_denominator = Complex.Pow(denominator, Complex.Log(exponent_base-1));
            Complex minor_denominator = Complex.Pow(infinity-1, s);
            Complex major_denominator = next_denominator/minor_denominator;

            Complex minor_numerator = major_denominator;
            Complex major_numerator = numerator*minor_denominator;
            Complex next_numerator = major_numerator + minor_numerator;
            
            numerator = 1;
            denominator = next_denominator/next_numerator;
        }
        return numerator / denominator;
    }

    private readonly Complex test_input = new Complex(2, Mathf.PI);
    private readonly Complex test_output = new Complex(0.7956402067356775174, -0.0942944615085101302365576); // generated from Wolfram Alpha

    private const int infinity = 10;
}
