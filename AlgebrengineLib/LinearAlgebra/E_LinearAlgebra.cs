using System.Numerics;

namespace AlgebrengineLib.LinearAlgebra;


/*
EXAMPLE CREATION OF AN ALGEBRA ELEMENT (not real term):

Here, the linear algebra library defines its own interface to interact with
any newly-created nodes that wish to take part in said algebra. It does this by making an
unconstrained interface, merely to check if an node is valid for linear algebra (as
all nodes MUST inherit from the constrained version, which is more difficult to check), and
an unconstrained interface, which actually specifies the behavior

*/
using ICLAE_Generic = IConstrainedLinearAlgebraElement<IUnconstrainedLinearAlgebraElement, IUnconstrainedLinearAlgebraElement>;
public class LinearAlgebraElementNode : ConstrainedBaseElement<ICLAE_Generic>//, IConstrainedLinearAlgebraElement<LinearAlgebraElementNode>
{
	public LinearAlgebraElementNode(IUnconstrainedLinearAlgebraElement i) : base(i)
	{
	}
	public LinearAlgebraElementNode(ICLAE_Generic i) : base(i)
	{
		
	}
	public LinearAlgebraElementNode(string vN, string vD) : base(vN, vD)
	{
		
	}
	public LinearAlgebraElementNode(IConstrainedOutput<UnconstrainedBaseElement> op, bool iSE = false)
    {
        
    }
	public LinearAlgebraElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedLinearAlgebraElement : IElementImplementer
{
	
}

public interface IConstrainedLinearAlgebraElement<in InElement, out OutElement> : IUnconstrainedLinearAlgebraElement where InElement : IUnconstrainedLinearAlgebraElement where OutElement : IUnconstrainedLinearAlgebraElement
{
	public OutElement Add(InElement otherNode);
	public OutElement Add(Scalar otherNode);
	public OutElement Multiply(InElement otherNode);
	public OutElement Multiply(Scalar otherNode);

	//TODO: Confirm this is a good architecture for stuff like this
	public void Negate();
	public void Invert(); 
}


public class Scalar : TreeNode, IConstrainedLinearAlgebraElement<Scalar, Scalar>
{
	public double ScalarVal {get; private set;} = double.NaN;
	public Scalar(double val) : base(val) {
		ScalarVal = val;
	}
	
	public Scalar(IConstrainedOutput<LinearAlgebraElementNode> O) : base(O, false) {}

	public Scalar(string vN, string vD) : base(vN, vD) {}

	public Scalar() : base()
	{
		ScalarVal = 0;
	}
	public Scalar Add(Scalar otherNode)
	{
		return new(this.ScalarVal + otherNode.ScalarVal);
	}
	public Scalar Multiply(Scalar otherNode)
	{
		return new(this.ScalarVal * otherNode.ScalarVal);
	}

	public void Negate()
	{
		this.ScalarVal *= -1;
	}
	public void Invert()
	{
		this.ScalarVal = 1/this.ScalarVal;
	}
}