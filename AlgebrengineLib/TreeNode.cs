namespace AlgebrengineLib;
using System.Collections.Generic;
using System.Reflection;

public class TreeNode
{
	public TreeNode SubNode1 = INVALID_ELEMENT("TreeNode SubNode 1 not set", true);
	public TreeNode SubNode2 = INVALID_ELEMENT("TreeNode SubNode 2 not set", true);


	//The two following operator definitions should be the same object
	public OperatorBase Operator = new INVALID_OPERATOR("TreeNode Operator not set", true);
	public IConstrainedOutputOperator<TreeNode> ConstrainedOutputOperator = new INVALID_OPERATOR("TreeNode C. O. Operator not set", true);

	public bool isEndValue; //if it is a leaf
	public TreeNode Parent = INVALID_ELEMENT("TreeNode Parent not set", true);
	public int sNNumberForParent = -1; //ONE OR TWO
	public bool hasDefiniteValue; //for larger structures with all numbers filled, or for values themselves. NOT for trees with all subnodes filled numbers

	//NOTE: a node that is a variable will have a name, which can be looked up in the Store class to find the TreeNode
	//which it represents (or eventually may represent)

	//While a variable naming convention isn't necessarily required, here is what this library uses:
	//"<what type of thing its used for>_<what specific thing it comes from>_<further specification...>_<display shorthand>"
	//Example: the "a" used in the distributive property's "a(b+c) --> ab + ac" is named as such:
	//"prop_distributive_forward_a"
	public bool isVariable; //for substitution and such
	public string variableName = ""; //used for the Store table, only usable if field above is true
	public string variableDisplayValue = "x"; //how it actually looks. For more graphical programs, the type may have to be more
	//nuanced than  "string"
	public bool isStructuralElement; //if it is part of a larger object (i. e., a row in a matrix)
	public bool isInvalidElement = false; //if the treenode doesn't exist (i. e., 4th component of 3D vector)
	public string cause = "<NO CAUSE SET>"; //if it is an invalid element
	public int id = -1; //for debugging - purpose mostly backend
	public int parentId = -1;
	private static List<TreeNode> GlobalElemList = [];
	private static int GlobalElemCount = 0;
	private static void AddToGlobalElements(TreeNode t) {
		t.id = GlobalElemCount++;
		GlobalElemList.Add(t);
	}
	private static void RemoveFromGlobalElements(TreeNode t)
	{
		GlobalElemList.RemoveAt(t.id);
		GlobalElemCount--;
		t.id = -1;
	}

	public TreeNode[] alternativeArrangements = [];
	
	//No constructors are made without arguments to force child classes to implement them
	//REMINDER: when overriding this, do NOT constrain the operator 
	public TreeNode(IConstrainedOutputOperator<TreeNode> op, bool iSE = false)
	{
		isStructuralElement = iSE;
		id = GlobalElemCount++;
		alternativeArrangements = GetAlternativeArrangements(true);

		TreeNode copy = op.GetUnsimplified(this);

		this.ReplaceWithTree(copy);
	}

	public TreeNode(double val)
	{
		hasDefiniteValue = true;
		isEndValue = true;
		id = GlobalElemCount++;
	}

	public TreeNode(string vN, string vD = "x", bool assumeAdd = false)
	{
		MakeVariable(vN, vD);

		//REMINDER: treenodes have variable names and descriptions allows them
		//to, by default, TAKE the value from the store, NOT to be added to it.
		//for properties, they are already added via the CheckIfSimilar
		if (assumeAdd && !Store.HasVariable(this))
		{
			Store.AddVariable(vN, this, out _);
		}

		AddToGlobalElements(this);
	}
	
	public TreeNode()
	{
		//required for the operator class to work
		//do NOT use this normally
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
			Operator.ReplaceGenericSubNodes(SubNode1.GenerateCopy(true), SubNode2.GenerateCopy(true));
			//Operator should be the same as constrained output operator
			return new TreeNode(ConstrainedOutputOperator, isStructuralElement);
		} else {
			return new TreeNode(ConstrainedOutputOperator, isStructuralElement);
		}
		
	}

	//---------------------------------------------
	public void HandleTreeChange(TreeNode oldSelf)
	{
		ValidateDefiniteValue(out _);
		alternativeArrangements = GetAlternativeArrangements(true);
	}

	public void ValidateDefiniteValue(out bool isDef)
	{
		//Repair logic
		if (hasDefiniteValue)
		{
			isDef = true;
		}

        SubNode1.ValidateDefiniteValue(out bool def1);
        SubNode2.ValidateDefiniteValue(out bool def2);
		isDef = def1 && def2;
	}

	/*check if two trees are similar, with support for different definitions
	
		"extended tree num base" refers to the option to check that two are similar, in the sense that one is an extension
			of another - i. e., it contains all the nodes of the base tree, but added on. No nodes have been removed or swapped
			with one another. if it is true, this is considered instead of being an exact match, with the n1 being the base
			and n2 being the extension.
	*/
	//TODO: Confirm this works
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
				ConnectVariablesHelper(connectVariables, n1.variableName, n2);
				return true;
			//if there is a mismatch, but because n2 could be an extension
			} else if (checkIfExtension && n1.isEndValue) { 
				ConnectVariablesHelper(connectVariables, n1.variableName, n2);
				return true;
			//No way to save it
			} else {
				return false;
			}
		}

		//keep checking down the tree
		if (n1.Operator.GetType() != n2.Operator.GetType())
		{
			return false; //if different operators, the tree is not similar
		}
		if (!n2.GetType().IsAssignableFrom(n1.GetType())) //if not assignable, return false
		{
			//IMPORTANT EXCEPTION: dealing with a type unknown for the current algebra because it was implemented outside it
			//(is an element)
			if (n1 is UnconstrainedBaseElement B && n2 is IElementImplementer EI && B.IsCompatibleNode(EI))
			{
				ConnectVariablesHelper(connectVariables, n1.variableName, n2);
			}
			return false; //if it cannot reasonably be replaced
		} else
		{
			ConnectVariablesHelper(connectVariables, n1.variableName, n2);
		}

		//if not gauranteed that either is a quantity, keep going
		bool similar = CheckIfSimilar(n1.SubNode1, n2.SubNode1) && CheckIfSimilar(n1.SubNode2, n2.SubNode2);

		if (similar) { //connect current if the sub-tree ends up being valid
			ConnectVariablesHelper(connectVariables, n1.variableName, n2);
		}

		return similar;
	}
	private static void ConnectVariablesHelper(bool canConnect, string varName, TreeNode varNode)
	{
		if (canConnect) { 
			Store.AddVariable(varName, varNode, out bool s); 
			if (!s) Console.WriteLine("Error: could not connect variable!");
		}
	}

	public void ReplaceAsVariable(bool recurse = false) {
        TreeNode newTree = Store.GetVariable(variableName, out bool exists);
        if (isVariable && exists) {
			ReplaceWithTree(newTree);
		}
		
		if (recurse)
		{
			SubNode1.ReplaceAsVariable(true);
			SubNode2.ReplaceAsVariable(true);
		}
	}
	
	public void ReplaceWithTree(TreeNode tree)
	{
		TreeNode old = (TreeNode)this.MemberwiseClone();
		old.SubNode1 = this.SubNode1;
		old.Operator = this.Operator;
		old.SubNode2 = this.SubNode2;
		SubNode1 = tree.SubNode1;
		ConstrainedOutputOperator = tree.ConstrainedOutputOperator;
		SubNode2 = tree.SubNode2;
		SubNode1.Parent = this;
		SubNode2.Parent = this;
		SubNode1.sNNumberForParent = 1;
		SubNode2.sNNumberForParent = 2;

		if (ConstrainedOutputOperator is OperatorBase realOp)
		{
			Operator = realOp;
			Operator.Parent = this;
		} else
		{
			throw new InvalidOperationException("ERROR: all operators should inherit from OperatorBase or OperatoreBase<TreeNode, TreeNode, TreeNode>!");
		}

		AddToGlobalElements(this);
		RemoveFromGlobalElements(old);

		HandleTreeChange(old);
	}

	public void ReplaceSubNode(TreeNode newSN, int sNNum)
	{ //ONE OR TWO
		Operator.ReplaceGenericSubNodes(newSN, sNNum);
		if (sNNum == 1)
		{
			ReplaceWithTree(new(ConstrainedOutputOperator, isStructuralElement));
		}
		else if (sNNum == 1)
		{
			ReplaceWithTree(new(ConstrainedOutputOperator, isStructuralElement));
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

	private static bool CanBeAssociated(TreeNode t, out TreeNode alt)
	{
		if (t.Operator.Associatives.Length == 0 || !t.Operator.AssociativeWith(t.SubNode1.Operator) || !t.Operator.AssociativeWith(t.SubNode2.Operator))
		{
			alt = t;
			return false;
		}

		//NOTE: Commutivity changes order of TOP elements, so both paths need to be checked unless its going up and down the chain
		if (!t.SubNode1.isEndValue)
		{
			t.Operator.ReplaceGenericSubNodes(t.SubNode1.SubNode2, t.SubNode2);
			t.SubNode1.Operator.ReplaceGenericSubNodes(t.SubNode1.SubNode1, new TreeNode(t.ConstrainedOutputOperator));
		
			alt = new(t.SubNode1.ConstrainedOutputOperator);
		}
		else
		{
			t.Operator.ReplaceGenericSubNodes(t.SubNode1, t.SubNode2.SubNode1);
			t.SubNode1.Operator.ReplaceGenericSubNodes(t.SubNode2.SubNode2, new TreeNode(t.ConstrainedOutputOperator));

			alt = new(t.SubNode2.ConstrainedOutputOperator);
		}

		return true;
	}

	private static bool CanBeCommutated(TreeNode t, out TreeNode alt)
	{
		if (t.Operator.Commutative)
		{
			alt = t;
			return false;
		}

		t.Operator.ReplaceGenericSubNodes(t.SubNode2, t.SubNode1);
		
		alt = new(t.ConstrainedOutputOperator);
		return true;
	}

	public static TreeNode INVALID_ELEMENT(string causeOfInvalidity, bool suppressCreationWarning = false)
	{
		return INVALID_ELEMENT<TreeNode>(causeOfInvalidity, suppressCreationWarning);
	}

	public static INode INVALID_ELEMENT<INode>(string causeOfInvalidity, bool suppressCreationWarning = false) where INode : TreeNode, new()
	{
		INode iv = new()
        {
            cause = causeOfInvalidity,
            isInvalidElement = true
        };

		if (!suppressCreationWarning) Console.WriteLine("WARNING: Invalid element created! | Cause: \"" + iv.cause + "\" | ID: " + iv.id);
        
		return iv;
	}
}

//===========================================================================================
//===========================================================================================
//===========================================================================================
