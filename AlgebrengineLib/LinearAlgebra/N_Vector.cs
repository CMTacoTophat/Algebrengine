namespace AlgebrengineLib.LinearAlgebra;

public class Vector : Tensor { 
	//Possibly inherit from Matrix?
	public Vector(TreeNode SN1, TreeNode SN2, O_Structure O) : base(SN1, SN2, O) {
	
	}
	
	public Vector(string vN, string vD) : base(vN, vD) {}
	
	//TODO: Merge with implementation in Matrix class
	public TreeNode GetVectorElement(int index)
	{
		if (index + 1 > vecDim)
		{
			return new INVALID_ELEMENT("Vector Index Out of Bounds");
		}

		TreeNode target = this;
		for (int i = 0; i < index; i++)
		{
			target = target.SubNode1;
		}

		return target.SubNode2;
	}

	public void SetVectorElement(int index, TreeNode t)
	{
		GetVectorElement(index).ReplaceNode(t);
	}

	public int vecDim = 0;
}