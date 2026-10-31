namespace AlgebrengineLib.Complex;

using ICCEN_Generic = IConstrainedComplexElement<IUnconstrainedComplexElement, IUnconstrainedComplexElement>;

public class ComplexElementNode : ConstrainedBaseElement<ICCEN_Generic>
{
	public ComplexElementNode(IUnconstrainedComplexElement i) : base(i)
	{
	}
	public ComplexElementNode(ICCEN_Generic i) : base(i)
	{
		
	}
	public ComplexElementNode(string vN, string vD) : base(vN, vD)
	{
		
	}
	public ComplexElementNode(IConstrainedOutput<UnconstrainedBaseElement> op, bool iSE = false)
    {
        
    }
	public ComplexElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedComplexElement : IElementImplementer
{
	
}

public interface IConstrainedComplexElement<in InElement, out OutElement> : IUnconstrainedComplexElement where InElement : IUnconstrainedComplexElement where OutElement : IUnconstrainedComplexElement
{
	public OutElement Add(InElement otherNode);
	public OutElement Add(Real otherNode);
	public OutElement Multiply(InElement otherNode);
	public OutElement Multiply(Real otherNode);

	//TODO: Confirm this is a good architecture for stuff like this
	public void Negate();
	public void Invert(); 
}


public class Real : TreeNode, IConstrainedComplexElement<Real, Real>
{
	public double RealVal {get; private set;} = float.NaN;
	public Real(double val) : base(val) {
		RealVal = val;
	}
	
	public Real(IConstrainedOutput<Real> O) : base(O, false) {}

	public Real(string vN, string vD) : base(vN, vD) {}

	public Real() : base()
	{
		RealVal = 0;
	}
	public Real Add(Real otherNode)
	{
		return new(this.RealVal + otherNode.RealVal);
	}
	public Real Multiply(Real otherNode)
	{
		return new(this.RealVal * otherNode.RealVal);
	}
	public void Negate()
	{
		this.RealVal *= -1;
	}
	public void Invert()
	{
		this.RealVal = 1/this.RealVal;
	}
}