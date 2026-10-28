namespace AlgebrengineLib.Trigonometry;

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
	public TrigonometricScalarElementNode(IConstrainedOutput<TrigonometricScalarElementNode> op, bool iSE = false)
    {
        
    }
	public TrigonometricScalarElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedTrigonometricScalarElement : IElementImplementer
{
	
}

public interface IConstrainedTrigonometricScalarElement<in InElement, out OutElement> : IUnconstrainedTrigonometricScalarElement where InElement : IUnconstrainedTrigonometricScalarElement where OutElement : IUnconstrainedTrigonometricScalarElement
{
	public OutElement Add(InElement otherNode);
	public OutElement Add(TrigScalar otherNode);
	public OutElement Multiply(InElement otherNode);
	public OutElement Multiply(TrigScalar otherNode);
}

// Node for the input to trigonometric functions - can be, but not 
public class TrigScalar : TreeNode, IConstrainedTrigonometricScalarElement<TrigScalar, TrigScalar>
{
	public double ScalarVal {get; private set;} = float.NaN;
	public TrigScalar(double val) : base(val) {
		ScalarVal = val;
	}
	
	public TrigScalar(IConstrainedOutput<TrigScalar> O) : base(O, false) {}

	public TrigScalar(string vN, string vD) : base(vN, vD) {}

	public TrigScalar() : base()
	{
		ScalarVal = 0;
	}

    public TrigScalar Add(TrigScalar otherNode)
    {
        return new(this.ScalarVal + otherNode.ScalarVal);
    }

    public TrigScalar Multiply(TrigScalar otherNode)
    {
        return new(this.ScalarVal * otherNode.ScalarVal);
    }
}