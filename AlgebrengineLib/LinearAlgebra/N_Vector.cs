namespace AlgebrengineLib.LinearAlgebra;

public class Vector : Tensor { 
	//Possibly inherit from Matrix?
	public int vecDim = 0;
	public Vector(IConstrainedOutput<Vector> O) : base(O) {}

	public Vector(params TreeNode[] elements) : base()
	{
		vecDim = elements.Length;
		dim = [vecDim];
		ConstructTopologyFromRepresentation();
	}
	
	public Vector(string vN, string vD) : base(vN, vD) {}

	public Vector() : base()
	{
		
	}
	//element gauranteed to be scalar (output) because if it was a tensor of higher rank, that would just
	//make THIS a tensor of higher rank
	public Scalar GetVectorElement(int index)
	{
		if (index + 1 > vecDim)
		{
			return INVALID_ELEMENT<Scalar>("Vector Index Out of Bounds");
		}

		TreeNode target = this;
		for (int i = 0; i < index; i++)
		{
			target = target.SubNode1;
		}

		return (Scalar)target.SubNode2;
	}

	public void SetVectorElement(int index, TreeNode t)
	{
		GetVectorElement(index).ReplaceNode(t);
	}

	
}