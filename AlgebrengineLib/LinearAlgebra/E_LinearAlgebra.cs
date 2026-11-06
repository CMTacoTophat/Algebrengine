using System.Numerics;
using AlgebrengineLib.ElementaryAlgebra;

namespace AlgebrengineLib.LinearAlgebra;




/*
EXAMPLE CREATION OF AN ALGEBRA ELEMENT (not real term):

Here, the linear algebra library defines its own interface to interact with
any newly-created nodes that wish to take part in said algebra. It does this by making an
unconstrained interface, merely to check if an node is valid for linear algebra (as
all nodes MUST inherit from the constrained version, which is more difficult to check), and
an unconstrained interface, which actually specifies the behavior

*/

/*
	TODO:
	
	A. 	Find a better place to put this to-do
	B. 	Make MANY of the classes partial, so modders can extend them to use their own node interfaces, such that
		connction would be seamless


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
	//TODO: Strictly-type this argument (might break the example class with p_distributive though)
	//the main issue is how operators borrowed from ElementaryAlgebra are more generic than LinearAlgebra,
	//and it doesn't seem like there's a way to make them more so in the LinearAlgebra space
	public LinearAlgebraElementNode(IConstrainedOutputOperator<UnconstrainedBaseElement> op, bool iSE = false) : base(op, iSE)
    {
        
    }
	public LinearAlgebraElementNode()
	{
		
	}
}

//DO NOT DERIVE FROM THIS INTERFACE UNLESS ABSOLUTELY NECESSARY
public interface IUnconstrainedLinearAlgebraElement : IElementImplementer, IUnconstrainedAlgebraElement
{
	
}

public interface IConstrainedLinearAlgebraElement<in InElement, out OutElement> : IUnconstrainedLinearAlgebraElement, IConstrainedAlgebraElement<InElement, OutElement> where InElement : IUnconstrainedLinearAlgebraElement, IUnconstrainedAlgebraElement where OutElement : IUnconstrainedLinearAlgebraElement, IUnconstrainedAlgebraElement
{/* 
	public OutElement Add(InElement otherNode);
	public OutElement Add(Scalar otherNode);
	public OutElement Multiply(InElement otherNode);
	public OutElement Multiply(Scalar otherNode);
	public void Negate();
	public void Invert();  */
}


public class Scalar : TreeNode, IConstrainedLinearAlgebraElement<Scalar, Scalar>
{
	public double ScalarVal {get; private set;} = double.NaN;
	public Scalar(double val) : base(val) {
		ScalarVal = val;
	}
	
	public Scalar(IConstrainedOutputOperator<LinearAlgebraElementNode> O) : base(O, false) {}

	public Scalar(string vN, string vD) : base(vN, vD) {}

	public Scalar() : base()
	{
		ScalarVal = 0;
	}
	public Scalar Add(Scalar otherNode)
	{
		return new(this.ScalarVal + otherNode.ScalarVal);
	}
	public Scalar Add(Number otherNode)
	{
		return new(this.ScalarVal + otherNode.NumberVal);
	}
	public Scalar Multiply(Scalar otherNode)
	{
		return new(this.ScalarVal * otherNode.ScalarVal);
	}
	public Scalar Multiply(Number otherNode)
	{
		return new(this.ScalarVal * otherNode.NumberVal);
	}

	public Scalar Power(Scalar otherNode)
	{
		return new(Math.Pow(this.ScalarVal,otherNode.ScalarVal));
	}
	public Scalar Power(Number otherNode)
	{
		return new(Math.Pow(this.ScalarVal, otherNode.NumberVal));
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