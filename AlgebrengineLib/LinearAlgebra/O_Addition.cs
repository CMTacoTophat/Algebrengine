namespace AlgebrengineLib.LinearAlgebra;

public class O_Addition : OperatorBase
{
	public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition)];}}
    public override string TextRep {get {return " + ";}}

	//TODO: Figure out how these would work with variables and other stuff that can't just be added
	protected override TreeNode OperatorSpecificSimplify(TreeNode N1, TreeNode N2)
	{
		//TODO: Add support for higher-rank tensors!
		if (N1 is Scalar && N2 is Scalar)
		{
			return new Scalar(N1.Value + N2.Value);
		} else if (N1 is Vector v1 && N2 is Vector v2)
		{
			//dont assume the unset dimensions are 0 - they could have been specified as such
			if (v1.vecDim != v2.vecDim) return Parent;

			TreeNode[] newElements = new TreeNode[v1.vecDim];

			//Add the elements
			for (int i = 0; i < v1.vecDim; i++)
			{
				newElements[i] = new(v1.GetVectorElement(i), v2.GetVectorElement(i), new O_Addition());
			}

			return new Vector(newElements);
		} else if (N1 is Tensor t1 && N2 is Tensor t2) //basically the same as matrix at this point
		{
			if (!t1.dim.SequenceEqual(t2.dim)) return Parent;

			Dictionary<int[], TreeNode> newDict = new();

			Action<int[]> adder = (int[] index) => {
				TreeNode e1 = t1.GetTensorElement(index);
				TreeNode e2 = t2.GetTensorElement(index);
				newDict.Add(index, new(e1, e2, new O_Addition()));
			};

			t1.ExecuteForAllIndexCombinations(adder);

			return new Tensor(newDict);
		}

		return Parent;
	}
}