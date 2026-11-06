namespace AlgebrengineLib.Complex;
using AlgebrengineLib.ElementaryAlgebra;

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
	public ComplexElementNode(IConstrainedOutputOperator<UnconstrainedBaseElement> op, bool iSE = false)
    {
        
    }
	public ComplexElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedComplexElement : IElementImplementer, IUnconstrainedAlgebraElement
{
	
}

public interface IConstrainedComplexElement<in InElement, out OutElement> : IUnconstrainedComplexElement, IConstrainedAlgebraElement<InElement, OutElement> where InElement : IUnconstrainedComplexElement, IUnconstrainedAlgebraElement where OutElement : IUnconstrainedComplexElement, IUnconstrainedAlgebraElement
{
	
}


public class Real : TreeNode, IConstrainedComplexElement<Real, Real>
{
	public double RealVal {get; private set;} = float.NaN;
	public Real(double val) : base(val) {
		RealVal = val;
	}
	
	public Real(IConstrainedOutputOperator<Real> O) : base(O, false) {}

	public Real(string vN, string vD) : base(vN, vD) {}

	public Real() : base()
	{
		RealVal = 0;
	}
	public Real Add(Real otherNode)
	{
		return new(this.RealVal + otherNode.RealVal);
	}
	public Real Add(Number otherNode)
	{
		return new(this.RealVal + otherNode.NumberVal);
	}
	public Real Multiply(Real otherNode)
	{
		return new(this.RealVal * otherNode.RealVal);
	}
	public Real Multiply(Number otherNode)
	{
		return new(this.RealVal * otherNode.NumberVal);
	}

	public Real Power(Real otherNode)
	{
		return new(Math.Pow(this.RealVal, otherNode.RealVal));
	}
	public Real Power(Number otherNode)
	{
		return new(Math.Pow(this.RealVal, otherNode.NumberVal));
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