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
			
			Scalar BCSum = new(B, C, new O_Addition_Scalar());
			Scalar AProd = new(A, BCSum, new O_Multiplication_Scalar());
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
			
			Scalar ABMult = new(A, B, new O_Multiplication_Scalar());
			Scalar ACMult = new(A, C, new O_Multiplication_Scalar());
			Scalar ABACProd = new (ABMult, ACMult, new O_Addition_Scalar());
			return ABACProd;
		}
	}
}
