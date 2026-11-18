namespace AlgebrengineLib.Trigonometry;
using AlgebrengineLib.ElementaryAlgebra;

using ICTSEN_Generic = IConstrainedTrigonometricScalarElement<IUnconstrainedTrigonometricScalarElement, IUnconstrainedTrigonometricScalarElement>;

public class TrigonometricScalarElementNode : ConstrainedBaseElement<ICTSEN_Generic>
{
	public TrigonometricScalarElementNode(IUnconstrainedTrigonometricScalarElement i) : base(i)
	{
	}
	public TrigonometricScalarElementNode(ICTSEN_Generic i) : base(i)
	{
		
	}
	public TrigonometricScalarElementNode(string vN, string vD) : base(vN, vD)
	{
		
	}
	public TrigonometricScalarElementNode(IConstrainedOutputOperator<TrigonometricScalarElementNode> op, bool iSE = false) : base(op, iSE)
    {
        
    }
	public TrigonometricScalarElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedTrigonometricScalarElement : IElementImplementer, IUnconstrainedAlgebraElement
{
	
}

public interface IConstrainedTrigonometricScalarElement<in InElement, out OutElement> : IUnconstrainedTrigonometricScalarElement, IConstrainedAlgebraElement<InElement, OutElement> where InElement : IUnconstrainedTrigonometricScalarElement, IUnconstrainedAlgebraElement where OutElement : IUnconstrainedTrigonometricScalarElement, IUnconstrainedAlgebraElement
{
	
}

public interface ITrigonometricScalarElementOperators<TSelf>
    : IUnconstrainedTrigonometricScalarElement, IAlgebraElementOperators<TSelf>
    where TSelf : ITrigonometricScalarElementOperators<TSelf>
{
    static abstract bool operator ==(TSelf left, TSelf right);
    static abstract bool operator !=(TSelf left, TSelf right);
    static abstract bool operator ==(TSelf left, Number right);
    static abstract bool operator !=(TSelf left, Number right);
}

public interface ITrigonometricScalarImplementer<TSelf> : IConstrainedTrigonometricScalarElement<TSelf, TSelf>, ITrigonometricScalarElementOperators<TSelf> where TSelf : ITrigonometricScalarImplementer<TSelf>
{
	
}

//TODO: Add equality interfaces to trigonometry

// Node for the input to trigonometric functions - can be, but not 
public class TrigScalar : TreeNode, IConstrainedTrigonometricScalarElement<TrigScalar, TrigScalar>
{
	public double ScalarVal {get; private set;} = float.NaN;
	public TrigScalar(double val) : base(val) {
		ScalarVal = val;
	}
	
	public TrigScalar(IConstrainedOutputOperator<TrigScalar> O) : base(O, false) {}

	public TrigScalar(string vN, string vD) : base(vN, vD) {}

	public TrigScalar() : base()
	{
		ScalarVal = 0;
	}

    public TrigScalar Add(TrigScalar otherNode)
	{
		return new(this.ScalarVal + otherNode.ScalarVal);
	}
	public TrigScalar Add(Number otherNode)
	{
		return new(this.ScalarVal + otherNode.NumberVal);
	}
	public TrigScalar Multiply(TrigScalar otherNode)
	{
		return new(this.ScalarVal * otherNode.ScalarVal);
	}
	public TrigScalar Multiply(Number otherNode)
	{
		return new(this.ScalarVal * otherNode.NumberVal);
	}

	public TrigScalar Power(TrigScalar otherNode)
	{
		return new(Math.Pow(this.ScalarVal,otherNode.ScalarVal));
	}
	public TrigScalar Power(Number otherNode)
	{
		return new(Math.Pow(this.ScalarVal, otherNode.NumberVal));
	}
	public void Invert()
	{
		this.ScalarVal = 1/this.ScalarVal;
	}
	public void Negate()
	{
		this.ScalarVal *= -1;
	}
	public double GetNumericValue()
	{
		return this.ScalarVal;
	}

	public static bool operator ==(TrigScalar left, TrigScalar right)
	{
		return left.ScalarVal == right.ScalarVal;
	}
	public static bool operator ==(TrigScalar left, Number right)
	{
		return left.ScalarVal == right.NumberVal;
	}
	public static bool operator !=(TrigScalar left, TrigScalar right)
	{
		return left.ScalarVal != right.ScalarVal;
	}
	public static bool operator !=(TrigScalar left, Number right)
	{
		return left.ScalarVal != right.NumberVal;
	}

	public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}