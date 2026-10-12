namespace AlgebrengineLib;

public abstract class OperatorBase
{
	public TreeNode Parent = TreeNode.INVALID_ELEMENT("Unset operator parent", false);
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
}


				/* "simplifiable" */
public interface IConstrainedOutput<out PT> where PT : TreeNode
{
	public PT Simplify(bool recursive);
}

public interface IConstrainedInput<in ST1, in ST2> where ST1 : TreeNode where ST2: TreeNode
{
	public void Setup(ST1 sn1, ST2 sn2);
}

public abstract class OperatorBase<PT, ST1, ST2> : OperatorBase, IConstrainedOutput<PT>, IConstrainedInput<ST1, ST2> where PT : TreeNode where ST1 : TreeNode where ST2: TreeNode
{
	ST1 SubNode1 = (ST1)TreeNode.INVALID_ELEMENT("Operator SubNode1 Not Set", true);
	ST2 SubNode2 = (ST2)TreeNode.INVALID_ELEMENT("Operator SubNode2 Not Set", true);
	public OperatorBase(ST1 sn1, ST2 sn2)
	{
		Setup(sn1, sn2);
	}
	public OperatorBase()
	{
		
	}
	public void Setup(ST1 sn1, ST2 sn2)
	{
		SubNode1 = sn1;
		SubNode2 = sn2;
	}
	public PT Simplify(bool recursive = false)
	{
		if (Parent.isEndValue)
		{
			return (PT)Parent; //whether recursing or not
		}

		//apply the simplification property
		PT temp = (PT)simplificationProperty.Apply(Parent, out _);

		if (recursive)
		{
			//do it to the new subnodes, if recursing
			//Parent.SubNode1.Operator.BaseSimplify(true);
			//Parent.SubNode2.Operator.BaseSimplify(true);
			
			TreeNode tsn1 = temp.SubNode1;
			TreeNode tsn2 = temp.SubNode2;

			tsn1.ReplaceWithTree(tsn1.ConstrainedOutputOperator.Simplify(true));
			tsn2.ReplaceWithTree(tsn2.ConstrainedOutputOperator.Simplify(true));
		}
		return temp;
	}

	//Use properties to simplify, as they can handle the necessary logic
	protected abstract PropertyBase simplificationProperty {get;}
}


//Special operator for signifying the sub nodes are just part of a larger object, and are not interacting mathematically
public class O_Structure : OperatorBase<TreeNode, TreeNode, TreeNode> {
    //can't be commutative or associative: structural elements are meant to be in a specific configuration
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return "<str>";}}


    protected override PropertyBase simplificationProperty { get{ return new P_Reflexive();}}
}

public class INVALID_OPERATOR : OperatorBase<TreeNode, TreeNode, TreeNode>
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

    protected override PropertyBase simplificationProperty {get{return new P_Reflexive();}}
}