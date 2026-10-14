namespace AlgebrengineLib.LinearAlgebra;

//This file is for the various distributive properties of each kind of linear algebra type/product
//TODO: Implement inner and outer tensor propducts
public class P_Distributive_Scalar : PropertyBase
{
	protected override TreeNode InputBaseTopo
	{
		get {
			//A*(B + C)
			Scalar A = new("prop_distributive_forward_a", "a");
			Scalar B = new("prop_distributive_forward_b", "b");
			Scalar C = new("prop_distributive_forward_c", "c");
			
			Scalar BCSum = new(new O_Addition_Scalar(B, C));
			Scalar AProd = new(new O_Multiplication_Scalar(A, BCSum));
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
			
			Scalar ABMult = new(new O_Multiplication_Scalar(A, B));
			Scalar ACMult = new(new O_Multiplication_Scalar(A, C));
			Scalar ABACProd = new (new O_Addition_Scalar(ABMult, ACMult));
			return ABACProd;
		}
	}
}
