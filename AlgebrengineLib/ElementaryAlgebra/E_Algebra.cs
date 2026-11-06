namespace AlgebrengineLib.ElementaryAlgebra;

using ICAE_Generic = IConstrainedAlgebraElement<IUnconstrainedAlgebraElement, IUnconstrainedAlgebraElement>;

public class AlgebraElementNode : ConstrainedBaseElement<ICAE_Generic>
{
	public AlgebraElementNode(IUnconstrainedAlgebraElement i) : base(i)
	{
	}
	public AlgebraElementNode(ICAE_Generic i) : base(i)
	{
		
	}
	public AlgebraElementNode(string vN, string vD) : base(vN, vD)
	{
		
	}
	public AlgebraElementNode(IConstrainedOutputOperator<UnconstrainedBaseElement> op, bool iSE = false)
    {
        
    }
	public AlgebraElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedAlgebraElement : IElementImplementer
{
	
}

public interface IConstrainedAlgebraElement<in InElement, out OutElement> : IUnconstrainedAlgebraElement where InElement : IUnconstrainedAlgebraElement where OutElement : IUnconstrainedAlgebraElement
{
	public OutElement Add(InElement otherNode);
	public OutElement Add(Number otherNode);
	public OutElement Multiply(InElement otherNode);
	public OutElement Multiply(Number otherNode);
    public OutElement Power(InElement otherNode);
    public OutElement Power(Number otherNode);
	public void Negate();
	public void Invert();
}


public class Number : TreeNode, IConstrainedAlgebraElement<Number, Number>
{
	public double NumberVal {get; private set;} = float.NaN;
	public Number(double val) : base(val) {
		NumberVal = val;
	}
	
	public Number(IConstrainedOutputOperator<Number> O) : base(O, false) {}

	public Number(string vN, string vD) : base(vN, vD) {}

	public Number() : base()
	{
		NumberVal = 0;
	}
	public Number Add(Number otherNode)
	{
		return new(this.NumberVal + otherNode.NumberVal);
	}
	public Number Multiply(Number otherNode)
	{
		return new(this.NumberVal * otherNode.NumberVal);
	}
    public Number Power(Number otherNode)
    {
        return new(Math.Pow(this.NumberVal, otherNode.NumberVal));
    }
	public void Negate()
	{
		this.NumberVal *= -1;
	}
	public void Invert()
	{
		this.NumberVal = 1/this.NumberVal;
	}
}