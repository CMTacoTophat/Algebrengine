namespace AlgebrengineLib.Trigonometry;

using ICTAEN_Generic = IConstrainedTrigonometricAngleElement<IUnconstrainedTrigonometricAngleElement, IUnconstrainedTrigonometricAngleElement>;

public class TrigonometricAngleElementNode : ConstrainedBaseElement<ICTAEN_Generic>
{
	public TrigonometricAngleElementNode(IUnconstrainedTrigonometricAngleElement i) : base(i)
	{
	}
	public TrigonometricAngleElementNode(ICTAEN_Generic i) : base(i)
	{
		
	}
	public TrigonometricAngleElementNode(string vN, string vD) : base(vN, vD)
	{
		
	}
	public TrigonometricAngleElementNode(IConstrainedOutput<UnconstrainedBaseElement> op, bool iSE = false)
    {
        
    }
	public TrigonometricAngleElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedTrigonometricAngleElement : IElementImplementer
{
	
}

public interface IConstrainedTrigonometricAngleElement<in InElement, out OutElement> : IUnconstrainedTrigonometricAngleElement where InElement : IUnconstrainedTrigonometricAngleElement where OutElement : IUnconstrainedTrigonometricAngleElement
{
	//Angle needs to be able to add to and multiply with a different trigscalar node
	public OutElement Add(InElement otherNode);
	public OutElement Add(TrigAngle otherNode);
	public OutElement Add(TrigScalar otherNode);
	public OutElement Multiply(InElement otherNode);
	//dont require angle multiplication
	public OutElement Multiply(TrigScalar otherNode);
	//will always return a trigscalar
	public TrigScalar Sin();
}

// Node for the input to trigonometric functions - can be, but not 
public class TrigAngle : TreeNode, IConstrainedTrigonometricAngleElement<TrigAngle, TrigAngle>
{
	public double Angle {get; private set;} = float.NaN;
	public TrigAngle(double val) : base(val) {
		Angle = val;
	}
	
	public TrigAngle(IConstrainedOutput<TrigAngle> O) : base(O, false) {}

	public TrigAngle(string vN, string vD) : base(vN, vD) {}

	public TrigAngle() : base()
	{
		Angle = 0;
	}
	public TrigAngle Add(TrigAngle otherNode)
	{
		return new(this.Angle + otherNode.Angle);
	}
	public TrigAngle Multiply(TrigAngle otherNode)
	{
		return new(this.Angle * otherNode.Angle);
	}

    public TrigAngle Add(TrigScalar otherNode)
    {
        return new(this.Angle + otherNode.ScalarVal);
    }

    public TrigAngle Multiply(TrigScalar otherNode)
    {
        return new(this.Angle * otherNode.ScalarVal);
    }

	public TrigScalar Sin()
	{
		return new(Math.Sin(Angle));
	}
}