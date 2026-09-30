namespace AlgebrengineLib.LinearAlgebra;
using System.Collections.Generic;
public class Tensor : TreeNode
{
	public int[] dim = [];
	private Dictionary<int[], TreeNode> representation = new();
	//REAL constructors
	public Tensor(TreeNode SN1, TreeNode SN2, OperatorBase O) : base(SN1, SN2, O, false) {}

	public Tensor(Dictionary<int[], TreeNode> rep) : base(EmptyConstructorException.Construction_From_Parameters)
	{
		representation = rep;
		dim = new int[rep.ElementAt(0).Key.Length];
		//initialize dim
		for (int i = 0; i < rep.Count; i++)
		{
			for (int r = 0; r < dim.Length; r++)
			{
				dim[r] = Math.Max(dim[r], rep.ElementAt(i).Key[r]);
			}
		}
		ConstructTopologyFromRepresentation();
	}
	
	public Tensor(int[] shape, params TreeNode[] nodes) : base(EmptyConstructorException.Construction_From_Parameters)
	{
		dim = shape;
		ConfigureTensor(nodes);
	}

	//It is apparently impossible to enforce multiple constructor inheritance. However, largely the only instance
	//where this second constructor would be used would be for properties, which would presumably be created with any new
	//mathematical objects added, thus making the lack of this constructor evident
	public Tensor(string vN, string vD) : base(vN, vD) {}

	protected Tensor(EmptyConstructorException e) : base (e) {}

	public void ConfigureTensor(params TreeNode[] nodes)
	{
		//build a helper array to get from N-D index to 1-D index
		//could potentially make this more permanent, as it might be useful elsewhere
		int[] positionFinder = new int[dim.Length];
		for (int r = dim.Length - 1; r >= 0; r++)
		{
			positionFinder[r] = r == dim.Length - 1 ? 1 : dim[r + 1] * positionFinder[r + 1]; //dimension N + 1 changes by 
			//the size of dimension N times how much IT changes by
		}

		int[] startIdx = new int[dim.Length];
		
		//for every index combination, multiply each index by how many previous ones it represents
		//the size of dimension N times how much IT changes by
		Action<int[]> positionMaker = (int[] idx) => {
			int pos = 0;
			for (int i = 0; i < positionFinder.Length; i++)
			{
				pos += idx[i] * positionFinder[i];
			}

			if (!representation.ContainsKey(idx)) {
				representation.Add(idx, nodes[pos]);
			} else {
				representation[idx] = nodes[pos];
			}
		};
		
		ExecuteForAllIndexCombinations(positionMaker);
		
		ConstructTopologyFromRepresentation();
	}

	public TreeNode GetTensorElement(int[] indices)
	{
		ConstructRepresentationFromTopology(); //keep this up to date
		if (!indices.Select(i => i + 1).ToArray().SequenceEqual(dim))
		{
			return new INVALID_ELEMENT("Tensor Indices Mismatched or Out of Bounds");
		}

		TreeNode target = this;
		for (int r = 0; r < indices.Length; r++)
		{ //for "rank"
			for (int dInd = 0; dInd < indices[r]; dInd++)
			{
				target = target.SubNode1;
			}

			target = target.SubNode2;
		}

		return target;
	}

	public void SetTensorElement(int[] indices, TreeNode t)
	{
		GetTensorElement(indices).ReplaceNode(t);
		ConstructRepresentationFromTopology();
	}
	
	protected int[][] GetAllIndexCombinations() {
		List<int[]> indices = new();
		Action<int[]> addIndex = (int[] idx) => { indices.Add(idx); };
		ExecuteForAllIndexCombinations(addIndex);
		return indices.ToArray();
	}

	public void ExecuteForAllIndexCombinations(Action<int[]> executable) {
		int[] startIdx = new int[dim.Length];
		
		//WARNING: apparently recursion with a lambda throws "use of unassigned local variable" errors. This supposedly works, but in case it doesn't,
		//this may have to be turned into a real method
		Action<int, int[]> loop = (int _, int[] _) => {};
		loop = (int r, int[] idx) => {
			if (r != 0)
			{
				for (int i = 0; i < dim[dim.Length - r]; i++)
				{
					idx[dim.Length - r] = i;
                    loop(r - 1, idx);
				}
			}
			else
			{
				int[] currIdx = (int[])idx.Clone();
				executable(currIdx);
			}
		};
		loop(dim.Length, startIdx);
	}
	
	protected void ConstructTopologyFromRepresentation()
	{	
		TreeNode compiledTensor = CompileBranch(dim.Length, new int[dim.Length]);
		//replace only the sub nodes to keep the special properties of the tensor object
		this.ReplaceSubNode(compiledTensor.SubNode1, 1);
		this.ReplaceSubNode(compiledTensor.SubNode2, 2);
	}
	
	//Helper method for CRFT
	private TreeNode CompileBranch(int r, int[] currIndex) {
		TreeNode lastSN = new INVALID_ELEMENT("Tensor Branch Ending"); //last sub/structural node
		int[] nextIndex = currIndex;
		int l = nextIndex.Length;
		
		for (int i = 0; i < dim[dim.Length - r]; i++) {
			nextIndex[l - r] = i;
			if (r > 1) {
				lastSN = new TensorBranch(lastSN, CompileBranch(r - 1, nextIndex));
			} else {
				lastSN = new TensorBranch(lastSN, representation[nextIndex]);
			}
		}
		
		return lastSN;
	}

	protected void ConstructRepresentationFromTopology()
	{
		Action<int[]> addToRepresentation = (int[] currIndex) => {
			TreeNode element = GetTensorElement(currIndex);
			
			if (!representation.ContainsKey(currIndex)) {
				representation.Add(currIndex, element);
			} else {
				representation[currIndex] = element;
			}
		};
		
		ExecuteForAllIndexCombinations(addToRepresentation);
	}
}

//structural element used for chaining various parts of a tensor block
//next rank or leaf node should ALWAYS be sub node TWO. Imagine, for a vector, the Top to Bottom tree diagram going 
//diagonal to the LEFT, with each scalar being a leaf on the RIGHT side of it
public class TensorBranch : TreeNode
{
	public TensorBranch(TreeNode sn1, TreeNode sn2) : base(sn1, sn2, new O_Structure(), true)
	{
	}
}