using System.Numerics;

namespace AlgebrengineLib.Complex;
//Rename system quaternion to not conflate types
using SysQuaternion = System.Numerics.Quaternion;

public class Quaternion : TreeNode
{
    private SysQuaternion systemQuaternion = new(0, 0, 0, 0);
    public SysQuaternion SystemQuaternion //WARNING: only works when normal double values were given
    {
        get {return hasDefiniteValue ? systemQuaternion : new(0, 0, 0, 0);}
        set {systemQuaternion = value;}
    }
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

    public Quaternion(IConstrainedOutputOperator<Quaternion> O) : base(O)
    {
        
    }

    public Quaternion(double r, double i, double j, double k)
    {
        rVal = r;
        iVal = i;
        jVal = j;
        kVal = k;
        hasDefiniteValue = true;
        SystemQuaternion = new((float)i, (float)j, (float)k, (float)r);

        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), new ImaginaryNumber(k, ComplexUnit.Unit.k)));
        ComplexBranch jBranch = new(new(kBranch, new ImaginaryNumber(j, ComplexUnit.Unit.j)));
        ComplexBranch iBranch = new(new(jBranch, new ImaginaryNumber(i, ComplexUnit.Unit.i)));
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, new ImaginaryNumber(r))));
    }

    public Quaternion(ComplexElementNode r, ComplexElementNode i, ComplexElementNode j, ComplexElementNode k)
    {
        ImaginaryNumber kIm = new ImaginaryNumber(k, ComplexUnit.Unit.k);
        ImaginaryNumber jIm = new ImaginaryNumber(j, ComplexUnit.Unit.j);
        ImaginaryNumber iIm = new ImaginaryNumber(i, ComplexUnit.Unit.i);
        ImaginaryNumber rIm = new ImaginaryNumber(r);
        
        ConstructTopologyHelper(rIm, iIm, jIm, kIm);
    }
    public Quaternion(ImaginaryNumber r, ImaginaryNumber i, ImaginaryNumber j, ImaginaryNumber k)
    {
        ConstructTopologyHelper(r, i, j, k);
    }
    private void ConstructTopologyHelper(ImaginaryNumber r, ImaginaryNumber i, ImaginaryNumber j, ImaginaryNumber k)
    {
        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), k));
        ComplexBranch jBranch = new(new(kBranch, j));
        ComplexBranch iBranch = new(new(jBranch, i));
        
        this.ReplaceWithTree(new Quaternion(new O_Structure<Quaternion>(iBranch, r)));

        ValidateDefiniteValue(out hasDefiniteValue);
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