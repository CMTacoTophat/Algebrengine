namespace AlgebrengineLib;

public class TreeNode
{
	public TreeNode SubNode1 = new INVALID_ELEMENT("TreeNode SubNode 1 not set", true);
	public OperatorBase Operator = new INVALID_OPERATOR("TreeNode Operator not set", true);
	public TreeNode SubNode2 = new INVALID_ELEMENT("TreeNode SubNode 2 not set", true);
	public bool isEndValue; //if it is a leaf
	public TreeNode Parent = new INVALID_ELEMENT("TreeNode Parent not set", true);
	public int sNNumberForParent = -1; //ONE OR TWO
	public bool hasDefiniteValue; //for larger structures with all numbers filled, or for values themselves. NOT for trees with all subnodes filled numbers
	public bool hasScalarRepresentativeValue; //for individual numbers, as display
	protected double scalarRepresentativeValue; //the actual number, only reasonable if the field above is true
	public double Value
	{
		get
		{
			return scalarRepresentativeValue;
		}

		set
		{
			scalarRepresentativeValue = value;
		}
	}

	//TODO: Establish variable naming convention (so variables added to the registry from one process don't conflict with another)
	//NOTE: a node that is a variable will have a name, which can be looked up in the Store class to find the TreeNode
	//which it represents (or eventually may represent)
	public bool isVariable; //for substitution and such
	public string variableName = ""; //used for the Store table, only usable if field above is true
	public string variableDisplayValue = "x"; //how it actually looks. For more graphical programs, the type may have to be more
	//nuanced than  "string"
	public bool isStructuralElement; //if it is part of a larger object (i. e., a row in a matrix)
	public bool isInvalidElement = false; //if the treenode doesn't exist (i. e., 4th component of 3D vector)
	public int id = -1; //for debugging - purpose mostly backend
	public int parentId = -1;
	public static int GlobalElemCount = 0; //TODO: Implement defragmentation process
	public TreeNode[] alternativeArrangements = [];
	
	//No constructors are made without arguments to force child classes to implement them
	public TreeNode(TreeNode sn1, TreeNode sn2, OperatorBase op, bool iSE = false)
	{
		SubNode1 = sn1;
		SubNode2 = sn2;
		sn1.Parent = sn2.Parent = this;
		sn1.parentId = sn2.parentId = id;
		sn1.sNNumberForParent = 1;
		sn2.sNNumberForParent = 2;
		Operator = op;
		op.Parent = this;
		isStructuralElement = iSE;
		id = GlobalElemCount++;
		alternativeArrangements = GetAlternativeArrangements(true);
	}

	public TreeNode(double val)
	{
		hasDefiniteValue = true;
		hasScalarRepresentativeValue = true;
		isEndValue = true;
		scalarRepresentativeValue = val;
		id = GlobalElemCount++;
	}

	//With this class, there is no way to program a TreeNode-inherited class without writing a 
	public TreeNode(string vN, string vD = "x")
	{
		MakeVariable(vN, vD);
		id = GlobalElemCount++;
	}
	
	//In the case a special treenode needs to be made where it can't override any of the REAL constructors
	protected enum EmptyConstructorException {
		Invalid_Element
	}
	
	protected TreeNode(EmptyConstructorException e) {
		switch (e) {
			case EmptyConstructorException.Invalid_Element:
				//TODO: Figure out special behavior here
				isEndValue = true;
				break;
		}
	}
	
	public void MakeVariable(string varName, string varDisplay = "x") {
		isVariable = true;
		isEndValue = true; //because theres not anything (yet) extending from it
		variableName = varName;
		variableDisplayValue = varDisplay;
	}

	public TreeNode GenerateCopy(bool recurse = true)
	{
		if (this.isEndValue) {
			return (TreeNode)MemberwiseClone();
		}
		
		if (recurse) {
			return new TreeNode(SubNode1.GenerateCopy(true), SubNode2.GenerateCopy(true), Operator, isStructuralElement);
		} else {
			return new TreeNode(SubNode1, SubNode2, Operator, isStructuralElement);
		}
		
	}

	//---------------------------------------------
	public void HandleTreeChange(TreeNode oldSelf)
	{
		//TODO: Implement (call every time tree is changed - operator, immediate(?) sub-nodes, etc.)
		_ = ValidateDefiniteValue();
		alternativeArrangements = GetAlternativeArrangements(true);
	}

	public bool ValidateDefiniteValue()
	{
		//Repair logic
		if (hasDefiniteValue)
		{
			return true;
		}
		else if (hasScalarRepresentativeValue)
		{
			hasDefiniteValue = true;
			return true;
		}
		else if (!double.IsNaN(scalarRepresentativeValue))
		{
			hasDefiniteValue = true;
			hasScalarRepresentativeValue = true;
			return true;
		}

		bool def = SubNode1.ValidateDefiniteValue() && SubNode2.ValidateDefiniteValue();
		return def;
	}

	/*check if two trees are similar, with support for different definitions
	
		"extended tree num base" refers to the option to check that two are similar, in the sense that one is an extension
			of another - i. e., it contains all the nodes of the base tree, but added on. No nodes have been removed or swapped
			with one another. if it is true, this is considered instead of being an exact match, with the n1 being the base
			and n2 being the extension.
	*/
	public static bool CheckIfSimilar(TreeNode n1, TreeNode n2, bool checkIfExtension = false, bool connectVariables = false)
	{ //NOTE: Convention will be than n1 has the variables to be connected!
		//Console.WriteLine("Checking " + msg);
		//not necessarily scalar representative, as it would be inconvenient to define, say, the distributive property
		//for all combinations of tensor ranks. Thus, just check if they are quantities at all, and trust that
		//the expressions were set up and the properties are coded so invalid combinations (like scalar + vector) dont
		//show up
		//If the quantities are being connected, have the n1 variables correspond to the n2 nodes
		
		if (n1.isEndValue || n2.isEndValue) {
			
			//if they are both the end value
			if (n1.isEndValue && n2.isEndValue) {
				if (connectVariables) { Store.AddVariable(n1.variableName, n2, out _); /*TODO: use error checking*/}
				return true;
			//if there is a mismatch, but because n2 could be an extension
			} else if (checkIfExtension && n1.isEndValue) { 
				if (connectVariables) { Store.AddVariable(n1.variableName, n2, out _); }
				return true;
			//No way to save it
			} else {
				return false;
			}
		}

		//if not gauranteed that either is a quantity, keep going
		return CheckIfSimilar(n1.SubNode1, n2.SubNode1) && CheckIfSimilar(n1.SubNode2, n2.SubNode2);
	}

	public void ReplaceAsVariable(bool recurse = false) {
		//TODO: Implement recursion
		bool exists;
		TreeNode newTree = Store.GetVariable(variableName, out exists);
		if (!isVariable || !exists) {
			return; //won't work
		}
		
		ReplaceWithTree(newTree);
	}
	
	public void ReplaceWithTree(TreeNode tree)
	{
		TreeNode old = (TreeNode)this.MemberwiseClone();
		old.SubNode1 = this.SubNode1;
		old.Operator = this.Operator;
		old.SubNode2 = this.SubNode2;
		SubNode1 = tree.SubNode1;
		Operator = tree.Operator;
		SubNode2 = tree.SubNode2;
		HandleTreeChange(old);
	}

	public void ReplaceSubNode(TreeNode newSN, int sNNum)
	{ //ONE OR TWO
		if (sNNum == 1)
		{
			ReplaceWithTree(new(newSN, SubNode2, Operator, isStructuralElement));
		}
		else if (sNNum == 1)
		{
			ReplaceWithTree(new(SubNode1, newSN, Operator, isStructuralElement));
		}
	}

	public void ReplaceNode(TreeNode newNode)
	{
		Parent.ReplaceSubNode(this, sNNumberForParent);
	}

	public TreeNode[] GetAlternativeArrangements(bool includeOriginal)
	{
		HashSet<TreeNode> alts = GAARecurse(this);
		if (includeOriginal)
		{
			alts.Add(this);
		}

		//TODO: Call GAARecurse
		TreeNode[] ret = {};
		alts.CopyTo(ret);
		return ret;
	}

	private HashSet<TreeNode> GAARecurse(TreeNode curr, int variant = 0)
	{
		HashSet<TreeNode> variations = new();
		//skip logic if curr is a leaf
		if (curr.isEndValue)
		{
			variations.Add(curr);
			return variations;
		}

		//check all possible permutations
		//explore the graph space, making a new agent at each junction (commutativity and/or associativity)
		bool result;
		TreeNode branch;
		//swap around nodes if possible (less so identities - can be checked here)
		result = CanBeAssociated(this, out branch);
		if (result && variant != 1)
		{
			variations.UnionWith(GAARecurse(branch, 1)); //Be sure not to create an infinite cascade of comm-swapped branches
		}

		result = CanBeCommutated(this, out branch);
		if (result)
		{
			variations.UnionWith(GAARecurse(branch));
		}

		//Go down each node
		HashSet<TreeNode> SN1Var = GAARecurse(curr.SubNode1);
		HashSet<TreeNode> SN2Var = GAARecurse(curr.SubNode2);
		TreeNode temp;
		//build the combined variations set of each sub node
		foreach (TreeNode sn1b in SN1Var)
		{
			foreach (TreeNode sn2b in SN2Var)
			{
				temp = curr.GenerateCopy();
				temp.ReplaceSubNode(sn1b, 1);
				temp.ReplaceSubNode(sn2b, 2);
				variations.Add(temp);
			}
		}

		return variations;
	}

	private bool CanBeAssociated(TreeNode t, out TreeNode alt)
	{
		if (t.Operator.Associatives.Length == 0 || !t.Operator.AssociativeWith(t.SubNode1.Operator) || !t.Operator.AssociativeWith(t.SubNode2.Operator))
		{
			alt = t;
			return false;
		}

		//NOTE: Commutivity changes order of TOP elements, so both paths need to be checked unless its going up and down the chain
		if (!t.SubNode1.isEndValue)
		{
			alt = new(t.SubNode1.SubNode1, new(t.SubNode1.SubNode2, t.SubNode2, t.Operator), t.SubNode1.Operator);
		}
		else
		{
			alt = new(new(t.SubNode1, t.SubNode2.SubNode1, t.Operator), t.SubNode2.SubNode2, t.SubNode2.Operator);
		}

		return true;
	}

	private bool CanBeCommutated(TreeNode t, out TreeNode alt)
	{
		if (t.Operator.Commutative)
		{
			alt = t;
			return false;
		}

		alt = new(t.SubNode2, t.SubNode1, t.Operator);
		return true;
	}
}

//===========================================================================================
//===========================================================================================
//===========================================================================================

public class INVALID_ELEMENT : TreeNode
{
    public string cause = "Unspecified Cause";
	public INVALID_ELEMENT(string causeOfInvalidity, bool suppressCreationWarning = false) : base(EmptyConstructorException.Invalid_Element)
	{
		isInvalidElement = true;
		cause = causeOfInvalidity;
		if (!suppressCreationWarning) Console.WriteLine("WARNING: Invalid element created! | Cause: \"" + cause + "\" | ID: " + id);
	}
}