namespace AlgebrengineLib.Complex;

//TODO: Add property of built-in Quaternion type (System.Numerics)
public class Quaternion : TreeNode
{
    public double rVal;
    public double iVal;
    public double jVal;
    public double kVal;
    public Quaternion(string vN, string vD) : base(vN, vD) {}

	public Quaternion () : base () {}

    public Quaternion(IConstrainedOutput<Quaternion> O) : base(O)
    {
        
    }

    public Quaternion(ComplexElementNode r, ComplexElementNode i, ComplexElementNode j, ComplexElementNode k)
    {
        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), new ImaginaryNumber(k, ComplexUnit.Unit.k)));
        ComplexBranch jBranch = new(new(kBranch, new ImaginaryNumber(j, ComplexUnit.Unit.j)));
        ComplexBranch iBranch = new(new(jBranch, new ImaginaryNumber(i, ComplexUnit.Unit.i)));
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, r)));


        ValidateDefiniteValue();
    }

    public Quaternion(double r, double i, double j, double k)
    {
        rVal = r;
        iVal = i;
        jVal = j;
        kVal = k;
        hasDefiniteValue = true;

        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), new ImaginaryNumber(k, ComplexUnit.Unit.k)));
        ComplexBranch jBranch = new(new(kBranch, new ImaginaryNumber(j, ComplexUnit.Unit.j)));
        ComplexBranch iBranch = new(new(jBranch, new ImaginaryNumber(i, ComplexUnit.Unit.i)));
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, new Real(r))));
    }

    public double Roll {
        get { return Math.Atan2(2 * (rVal * iVal + jVal * kVal), 1 - 2 * (iVal*iVal + jVal*jVal)); }
    }
    public double Pitch {
        get { return Math.Asin(2 * (rVal * jVal - kVal * iVal)); }
    }
    public double Yaw {
        get { return Math.Atan2(2 * (rVal * kVal + iVal * jVal), 1 - 2 * (iVal*iVal + jVal*jVal)); }
    }

    public double MagnitudeSquared {
        get { return rVal*rVal + iVal*iVal + jVal*jVal + kVal*kVal; }
    }
    public double Magnitude
    {
        get { return Math.Sqrt(MagnitudeSquared); }
    }
}

public class ComplexBranch : TreeNode
{
	public ComplexBranch(O_Structure<Quaternion> O) : base(O, true)
	{
	}
}