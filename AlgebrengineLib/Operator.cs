namespace AlgebrengineLib;

public abstract class OperatorBase
{
	public TreeNode Parent = new INVALID_ELEMENT("Unset operator parent", false);
	public abstract bool Commutative {get;}
	public abstract Type[] Associatives {get;}
	public abstract string TextRep {get;} //NOTE: spaces are NOT added later - they should be included by default if wanted

	public bool invalidOperator = false;

	//NOTE: association can be 1 way between two types: for instance, + is assoc. with -, but not the other way
	//This association is checked from Parent node to Child node
	//that is, A + (B - C) <-> (A + B) - C  | BUT NOT |  A - (B + C) </> (A - B) + C

	public bool AssociativeWith(Type t)
	{
		return Associatives.Contains(t);
	}

	public bool AssociativeWith(OperatorBase o)
	{
		return Associatives.Contains(o.GetType());
	}

	public TreeNode Simplify(bool recursive = false)
	{
		if (Parent.isEndValue)
		{
			return Parent; //whether recursing or not
		}

		//TODO (maybe already done): Implement definite value check (i. e., if simplication can be done) 
		if (recursive)
		{
			return OperatorSpecificSimplify(Parent.SubNode1.Operator.Simplify(true), Parent.SubNode2.Operator.Simplify(true));
		}
		else
		{
			return OperatorSpecificSimplify(Parent.SubNode1, Parent.SubNode2);
		}
	}

	protected abstract TreeNode OperatorSpecificSimplify(TreeNode N1, TreeNode N2);
}

//Special operator for signifying the sub nodes are just part of a larger object, and are not interacting mathematically
public class O_Structure : OperatorBase {
    //can't be commutative or associative: structural elements are meant to be in a specific configuration
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return "<str>";}}
	
	protected override TreeNode OperatorSpecificSimplify(TreeNode N1, TreeNode N2) {
		return Parent; //can't be simplified
	}
}

public class INVALID_OPERATOR : OperatorBase
{
	public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return "<str>";}}
	public string cause = "Unspecified Cause";
	public INVALID_OPERATOR(string causeOfInvalidity, bool suppressCreationWarning = false)
	{
		invalidOperator = true;
		cause = causeOfInvalidity;
		if (!suppressCreationWarning) Console.WriteLine("WARNING: Invalid operator created! | Cause: \"" + cause);
	}

	protected override TreeNode OperatorSpecificSimplify(TreeNode N1, TreeNode N2) {
		return Parent; //can't be simplified
	}
}