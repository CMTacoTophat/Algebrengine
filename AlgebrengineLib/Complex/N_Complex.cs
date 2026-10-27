namespace AlgebrengineLib.Complex;

public class ComplexNumber : Quaternion
{
    public ComplexNumber(double r, double i) : base(r, i, 0, 0)
    {
        
    }
    public ComplexNumber(string vN, string vD) : base(vN, vD) {}

	public ComplexNumber () : base () {}

    public ComplexNumber(IConstrainedOutput<ComplexNumber> O) : base(O)
    {
        
    }
    public double Angle {
        get { return Math.Atan2(iVal, rVal); }
    }
}