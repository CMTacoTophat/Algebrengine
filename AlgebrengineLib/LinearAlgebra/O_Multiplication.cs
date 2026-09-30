namespace AlgebrengineLib.LinearAlgebra;

public class O_Multiplication : OperatorBase
{
	public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Multiplication)];}}
    public override string TextRep {get {return " x ";}}

	protected override TreeNode OperatorSpecificSimplify(TreeNode N1, TreeNode N2)
	{
		//TODO: figure out what to do with other tensors
		if (N1 is Scalar && N2 is Scalar)
		{
			return new Scalar(N1.Value * N2.Value);
		}
		else
		{
			return new Scalar(0);
		}
	}
}