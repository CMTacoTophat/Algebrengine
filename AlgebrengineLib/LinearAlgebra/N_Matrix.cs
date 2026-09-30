namespace AlgebrengineLib.LinearAlgebra;

public class Matrix : Tensor
{
	public Matrix(TreeNode SN1, TreeNode SN2, OperatorBase O) : base(SN1, SN2, O) {
	
	}
	
	public Matrix(string vN, string vD) : base(vN, vD) {}
	public TreeNode GetMatrixElement(int RowIndex, int ColIndex)
	{
		if (RowIndex + 1 > dim[0] || ColIndex + 1 > dim[1])
		{
			return new INVALID_ELEMENT("Matrix Index Out of Bounds");
		}

		TreeNode target = this;
		//count rows
		for (int r = 0; r < RowIndex; r++)
		{
			target = target.SubNode1;
		}

		target = target.SubNode2; //transition to counting columns
		for (int c = 0; c < ColIndex; c++)
		{
			target = target.SubNode1;
		}

		return target.SubNode2;
	}

	public void SetMatrixElement(int RowIndex, int ColIndex, TreeNode t)
	{
		GetMatrixElement(RowIndex, ColIndex).ReplaceNode(t);
	}
}