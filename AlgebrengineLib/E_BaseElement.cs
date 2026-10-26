namespace AlgebrengineLib;

// every namespace branch has its own "element", an interface with the required ways that two nodes can be simplified.
// for example, linear algebra needs a simplest, minimum element where two of them can be combined into one by adding or multiplying.
// if some future class or add-on wants to create their own node (type of mathematical object) to work with this namespace, 
// such as complex numbers (which can be added and multiplied), they only need to inherit the interface and fill out the methods,
// such that the behavior for "addition" (by each component) and "multiplication" (FOIL) is defined for them.

//this is the interface to be inherited by any elements DEFINED FOR THE NAMESPACE
//See the linear algebra, complex number, and basics libraries for examples
public abstract class UnconstrainedBaseElement : TreeNode
{
    public UnconstrainedBaseElement(string vN, string vD) : base(vN, vD)
    {
        
    }
    public UnconstrainedBaseElement(IConstrainedOutput<UnconstrainedBaseElement> op, bool iSE = false)
    {
        
    }
    public UnconstrainedBaseElement()
    {
        
    }
    public virtual bool IsCompatibleNode(IElementImplementer e)
	{
		return false;
	}
}

public abstract class ConstrainedBaseElement<ImpType> : UnconstrainedBaseElement where ImpType : IElementImplementer
{
    public ImpType? node;
    public override bool IsCompatibleNode(IElementImplementer e)
	{
		return e is ImpType;
	}

    public ConstrainedBaseElement(IElementImplementer i)
	{
		if (i is TreeNode n)
		{
			this.ReplaceWithTree(n);
		} else
		{
			//TODO: Figure out
		}
        
	}
    public ConstrainedBaseElement(string vN, string vD) : base(vN, vD)
    {
        //TODO: figure out how to force it to be replaced as a variable here
        this.ReplaceAsVariable();
    }
    public ConstrainedBaseElement(IConstrainedOutput<UnconstrainedBaseElement> op, bool iSE = false)
    {
        //TODO: Set node here
    }
    public ConstrainedBaseElement()
    {
        
    }
}
//this is the interface to be inherited by the interface for elements MADE AFTER, OUTSIDE OF THE NAMESPACE
public interface IElementImplementer
{
    
}