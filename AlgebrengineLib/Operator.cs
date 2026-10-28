namespace AlgebrengineLib;

public abstract class OperatorBase
{
	//WARNING: NOT CONSTRAINED LIKE THE SUBNODES (potential TODO?)
	public TreeNode Parent = TreeNode.INVALID_ELEMENT("Unset operator parent", false);
	public abstract bool Commutative {get;} //TODO: potentially set this automatically for OperatorBase<t1, t2, t3> if t2 !-= t3
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

	//helper methods to allow for basic topology rearrangement. Because these will presumably be done from
	//self-consisten logic, the typing does not need to be compile-time checked (although it will still
	//result in invalid nodes)
	public virtual void ReplaceGenericSubNodes(TreeNode sn1, TreeNode sn2, bool generateInvalidsIfTypeMismatch = false) {}
	public virtual void ReplaceGenericSubNodes(TreeNode n, int num, bool generateInvalidsIfTypeMismatch = false) {}
}

/*
	EXPLANATION FOR OPERATOR ARCHITECTURE:
	
	in order to only let the user create mathematically-valid expressions, the operators must be constrained such that
	TreeNodes only accept ones that output their specific type, and the operators themselves only accept
	subnodes of a specific type. By leveraging covariance and contravariance, this behavior can be pushed to the
	compiler level, such that it is impossible to run a program that could have the potential for mathematically
	nonsensical statements (i. e., matrix = vector + scalar, or something), while still allowing for different levels
	of objects in certain situations. For instance, an operator adding together two matrices would still work for
	two vectors, even if they are the other type (instead of just implicitly having rank 1). The only issue is that 
	definitions can be somewhat verbose, but that can hopefully be dealt with more easily.

	ALL operators MUST inherit from OperatorBase<Parent, SubNode1, SubNode2>, and set the 3 types to be as described:
	Parent - the type of what results from the operator
	SubNode1 - the type of the first sub node being plugged in to the operator
	SubNode2 - the same as above, but with the second one

	NOTE: this means you MUST define a separate operator for every different type coming in and out!

	If you wish to use an operator somewhere (presumably as no pre-existing methods suit your needs), you can store it
	either by its output (IConstrainedOutput), and/or its input (IConstrainedInput). The former determines its Simplify()
	method, and the latter its Setup() method.

	TreeNode contructors only accept an operator based on its output, and operators can be constructed with only certain
	input arguments. Therefore, an example construction could be something like:
	new Vector(  new CrossProduct(new Vector(new Vector_Scalar_Mult(new Scalar(...), new Vector(...))), new Vector(...))  )
	for the expression "V3 = S1*V1 x V2". Helper methods can, of course, simplify this further

*/





				/* "simplifiable" */
public interface IConstrainedOutput<out PT> where PT : TreeNode
{
	public PT GetSimplified(bool recursive);
	public PT GetUnsimplified(TreeNode original);
}

public interface IConstrainedInput<in ST1, in ST2> where ST1 : TreeNode where ST2: TreeNode
{
	public void Setup(ST1 sn1, ST2 sn2);
}

public abstract class OperatorBase<PT, ST1, ST2> : OperatorBase, IConstrainedOutput<PT>, IConstrainedInput<ST1, ST2> where PT : TreeNode, new() where ST1 : TreeNode where ST2: TreeNode
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

	public PT Apply(ST1 sn1, ST2 sn2)
	{
		string invText = "";
		try
		{
			OperatorBase<PT, ST1, ST2>? newO = (OperatorBase<PT, ST1, ST2>)Activator.CreateInstance(this.GetType(), sn1, sn2)!;
			if (newO is not null)
			{
				PT? newN = (PT)Activator.CreateInstance(typeof(PT), newO)!;
				if (newN is not null)
				{
					return newN;
				} else
				{
					invText = "Couldn't apply operator";

				}
			}
		} catch (Exception e)
		{
			invText = "Error when applying operator: " + e.Message;
		}
		
		return (PT)TreeNode.INVALID_ELEMENT(invText);
	}



	//Use properties to simplify, as they can handle the necessary logic
	protected virtual PropertyBase simplificationProperty {get {return new P_Reflexive();}}
	public PT GetSimplified(bool recursive = false)
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

			tsn1.ReplaceWithTree(tsn1.ConstrainedOutputOperator.GetSimplified(true));
			tsn2.ReplaceWithTree(tsn2.ConstrainedOutputOperator.GetSimplified(true));
		}
		return temp;
	}

	public PT GetUnsimplified(TreeNode original)
	{
		PT temp = (PT)original.GenerateCopy();
		temp.SubNode1 = SubNode1;
		temp.SubNode2 = SubNode2;
		temp.Operator = this;
		temp.ConstrainedOutputOperator = this;

		return temp;
	}

	public override void ReplaceGenericSubNodes(TreeNode sn1, TreeNode sn2, bool generateInvalidsIfTypeMismatch = false)
	{
		//WARNING: unsafe
		if (sn1 is ST1 sn1Safe)
		{
			SubNode1 = sn1Safe;
		} else if (generateInvalidsIfTypeMismatch)
		{
			SubNode1 = (ST1)TreeNode.INVALID_ELEMENT("Type mismatch when replacing operator subnode (SN2)");
		} 

		if (sn2 is ST2 sn2Safe)
		{
			SubNode2 = sn2Safe;
		} else if (generateInvalidsIfTypeMismatch)
		{
			SubNode2 = (ST2)TreeNode.INVALID_ELEMENT("Type mismatch when replacing operator subnode (SN2)");
		} 
	}
    public override void ReplaceGenericSubNodes(TreeNode n, int num, bool generateInvalidsIfTypeMismatch = false)
    {
        if (num == 1)
		{
			ReplaceGenericSubNodes(n, SubNode2, generateInvalidsIfTypeMismatch);
		} else if (num == 2)
		{
			ReplaceGenericSubNodes(SubNode1, n, generateInvalidsIfTypeMismatch);
		}
    }
}


//Special operator for signifying the sub nodes are just part of a larger object, and are not interacting mathematically
public class O_Structure<CompiledElement> : OperatorBase<CompiledElement, TreeNode, TreeNode> where CompiledElement : TreeNode, new()
{
    //can't be commutative or associative: structural elements are meant to be in a specific configuration
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return "<str>";}}

	public O_Structure(TreeNode sn1, TreeNode sn2) : base(sn1, sn2)
	{
		
	}
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
}