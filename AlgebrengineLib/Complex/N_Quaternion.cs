namespace AlgebrengineLib.Complex;

//TODO: Add property of built-in Quaternion type (System.Numerics)
public class Quaternion : TreeNode
{
    public double rVal;
    public double iVal;
    public double jVal;
    public double kVal;
    public bool topologyValid = false;
    public ImaginaryNumber RComp {get { return GetComponent(ComplexUnit.Unit.r); }}
    public ImaginaryNumber IComp {get { return GetComponent(ComplexUnit.Unit.i); }}
    public ImaginaryNumber JComp {get { return GetComponent(ComplexUnit.Unit.j); }}
    public ImaginaryNumber KComp {get { return GetComponent(ComplexUnit.Unit.k); }}
    public Quaternion(string vN, string vD) : base(vN, vD) {}

	public Quaternion () : base () {}

    public Quaternion(IConstrainedOutput<Quaternion> O) : base(O)
    {
        
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
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, new ImaginaryNumber(r))));
    }

    //TODO: Cleanup these two methods
    public Quaternion(ComplexElementNode r, ComplexElementNode i, ComplexElementNode j, ComplexElementNode k)
    {
        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), new ImaginaryNumber(k, ComplexUnit.Unit.k)));
        ComplexBranch jBranch = new(new(kBranch, new ImaginaryNumber(j, ComplexUnit.Unit.j)));
        ComplexBranch iBranch = new(new(jBranch, new ImaginaryNumber(i, ComplexUnit.Unit.i)));
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, new ImaginaryNumber(r))));

        ValidateDefiniteValue();
        if (hasDefiniteValue)
        {
            //TODO: either set values here or completely scrap the definite value architecture
        }
    }
    public Quaternion(ImaginaryNumber r, ImaginaryNumber i, ImaginaryNumber j, ImaginaryNumber k)
    {
        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), k));
        ComplexBranch jBranch = new(new(kBranch, j));
        ComplexBranch iBranch = new(new(jBranch, i));
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, r)));

        ValidateDefiniteValue();
        if (hasDefiniteValue)
        {
            //TODO: either set values here or completely scrap the definite value architecture
        }
    }

    public ImaginaryNumber GetComponent(ComplexUnit.Unit comp)
    {
        if (!topologyValid)
        {
            return INVALID_ELEMENT<ImaginaryNumber>("Quaternion " + comp.ToString().ToUpper() + " component not set");    
        }

        if (comp == ComplexUnit.Unit.i)
        {
            return (ImaginaryNumber)SubNode1.SubNode2;
        }
        else if (comp == ComplexUnit.Unit.j)
        {
            return (ImaginaryNumber)SubNode1.SubNode1.SubNode2;
        }
        else if (comp == ComplexUnit.Unit.k)
        {
            return (ImaginaryNumber)SubNode1.SubNode1.SubNode1.SubNode2;
        } else
        {
            return (ImaginaryNumber)SubNode2; //Fall back to the real component
        }
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