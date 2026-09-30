namespace AlgebrengineLib.LinearAlgebra;

//TODO: make Distributive properties for the Inner and Outer Tensor products
public class P_Distributive_ScalarMult : PropertyBase
{
	protected override TreeNode InputBaseTopo
	{
		get {
			//A*(B + C)
			Scalar A = new("prop_distributive_forward_a", "a");
			Scalar B = new("prop_distributive_forward_b", "b");
			Scalar C = new("prop_distributive_forward_c", "c");
			
			Scalar BCSum = new(B, C, new O_Addition());
			Scalar AProd = new(A, BCSum, new O_Multiplication());
			return AProd;
		}
	}
	
	protected override TreeNode OutputBaseTopo
	{
		get {
			//A*B + A*C)
			Scalar A = new("prop_distributive_forward_a", "a");
			Scalar B = new("prop_distributive_forward_b", "b");
			Scalar C = new("prop_distributive_forward_c", "c");
			
			Scalar ABMult = new(A, B, new O_Multiplication());
			Scalar ACMult = new(A, C, new O_Multiplication());
			Scalar ABACProd = new (ABMult, ACMult, new O_Addition());
			return ABACProd;
		}
	}

	public override TreeNode Apply(TreeNode t)
	{
		//check for types with "if (t is Vector v)" and such
		return DefaultApply(t);
	}
}
