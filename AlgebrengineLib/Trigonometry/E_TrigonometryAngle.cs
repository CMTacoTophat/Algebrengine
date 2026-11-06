namespace AlgebrengineLib.Trigonometry;

using ICTAEN_Generic = IConstrainedTrigonometricAngleElement<IUnconstrainedTrigonometricAngleElement, IUnconstrainedTrigonometricAngleElement, IUnconstrainedTrigonometricScalarElement>;

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
	public TrigonometricAngleElementNode(IConstrainedOutputOperator<UnconstrainedBaseElement> op, bool iSE = false)
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

//There are certian things that can be used in trigonometric functions that aren't scalars, and don't give out scalars
//(mostly via taylor expansion, as it seems) - why a specific scalar output is needed

public interface IConstrainedTrigonometricAngleElement<in InElement, out OutAngleElement, out OutScalarElement> : IUnconstrainedTrigonometricAngleElement where InElement : IUnconstrainedTrigonometricAngleElement where OutAngleElement : IUnconstrainedTrigonometricAngleElement where OutScalarElement : IUnconstrainedTrigonometricScalarElement
{
	//Angle needs to be able to add to and multiply with a different trigscalar node
	public OutAngleElement Add(InElement otherNode);
	public OutAngleElement Add(TrigAngle otherNode);
	public OutAngleElement Add(TrigScalar otherNode);
	public OutAngleElement Multiply(InElement otherNode);
	//dont require angle multiplication
	public OutAngleElement Multiply(TrigScalar otherNode);
	public void Negate();

	//will always return a trigscalar
	public OutScalarElement Sin();
	public OutScalarElement Cos();
	public OutScalarElement Tan();
	public OutScalarElement Sinh();
	public OutScalarElement Cosh();
	public OutScalarElement Tanh();
}

// Node for the input to trigonometric functions - can be, but not 
public class TrigAngle : TreeNode, IConstrainedTrigonometricAngleElement<TrigAngle, TrigAngle, TrigScalar>
{
	public double Angle {get; private set;} = float.NaN;
	public TrigAngle(double val) : base(val) {
		Angle = val;
	}
	
	public TrigAngle(IConstrainedOutputOperator<TrigAngle> O) : base(O, false) {}

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
	public void Negate()
	{
		this.Angle *= -1;
	}

	public TrigScalar Sin()
	{
		return new(Math.Sin(Angle));
	}
	public TrigScalar Cos()
	{
		return new(Math.Cos(Angle));
	}
	public TrigScalar Tan()
	{
		return new(Math.Tan(Angle));
	}
	public TrigScalar Sinh()
	{
		return new(Math.Sinh(Angle));
	}
	public TrigScalar Cosh()
	{
		return new(Math.Cosh(Angle));
	}
	public TrigScalar Tanh()
	{
		return new(Math.Tanh(Angle));
	}
}