using UnityEngine;
using Meta.Numerics;
using Meta.Numerics.Functions;

public class RiemannHypothesis : MonoBehaviour
{
    public GameObject zeta_prefab;

    public void Update()
    {
        float current_time = Time.time;
        for (int interval = 1; interval <= 10; interval += 1)
        {
            //double input = Mathf.Lerp(last_time, current_time, interval/10f);
            Vector2 random = Random.insideUnitCircle;
            Complex output = my_function4(new Complex(random.x, random.y));
            GameObject spawned_object =GameObject.Instantiate(zeta_prefab, new Vector3((float)output.Re, (float)output.Im, 0), Quaternion.identity);
            GameObject.Destroy(spawned_object, 3f);
        }
        last_time = current_time;
    }
    public Complex my_function1(Complex s)
    {
        return AdvancedComplexMath.RiemannZeta(AdvancedComplexMath.RiemannZeta(s)) + 0.5f;
    }

    public Complex my_function2(Complex s)
    {
        for (int i = 0; i < 100; i += 1)
        {
            s = AdvancedComplexMath.RiemannZeta(s);
        }
        return s;
    }

    public Complex my_function3(Complex s)
    {
        for (int i = 0; i < 50; i += 1)
        {
            s = AdvancedComplexMath.RiemannZeta(AdvancedComplexMath.RiemannZeta(s)) + 0.52861f;
        }
        return s;
    }

    public Complex my_function4(Complex s)
    {
        for (int i = 0; i < 50; i += 1)
        {
            s = AdvancedComplexMath.RiemannZeta(s) + 0.5f;
        }
        return s;
    }

    public Complex riemann_zeta_function(Complex s)
    {
        Complex result = 0;
        for (int exponent_base = 1; exponent_base < infinity; exponent_base += 1)
        {
            result += 1/ComplexMath.Pow(exponent_base, s);
        }
        return result;
    }

    public Complex iterative_riemann_zeta_function(Complex s) // Maybe I built this in the opposite direction on top of my other errors? -- getting NaN
    {
        Complex numerator = 1;
        Complex denominator = ComplexMath.Pow(1, s);
        for (int exponent_base = 2; exponent_base < infinity; exponent_base += 1)
        {
            Complex next_denominator = ComplexMath.Pow(denominator, ComplexMath.Log(exponent_base-1));
            Complex minor_denominator = ComplexMath.Pow(infinity-1, s);
            Complex major_denominator = next_denominator/minor_denominator;

            Complex minor_numerator = major_denominator;
            Complex major_numerator = numerator*minor_denominator;
            Complex next_numerator = major_numerator + minor_numerator;
            
            numerator = 1;
            denominator = next_denominator/next_numerator;
        }
        return numerator / denominator;
    }
    
    private float last_time = 0;
    private const int infinity = 100;
}
