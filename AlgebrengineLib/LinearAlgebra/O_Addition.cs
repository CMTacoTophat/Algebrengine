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
		}

		//
		return Parent;
	}
}