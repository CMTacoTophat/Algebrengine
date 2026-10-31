namespace AlgebrengineLib.LinearAlgebra;

//This file is for the various distributive properties of each kind of linear algebra type/product
//TODO: Implement inner and outer tensor propducts
public class P_Distributive_LAElement : PropertyBase
{
	protected override TreeNode InputBaseTopo
	{
		get {
			//A*(B + C)
			LinearAlgebraElementNode A = new("prop_distributive_forward_a", "a");
			LinearAlgebraElementNode B = new("prop_distributive_forward_b", "b");
			LinearAlgebraElementNode C = new("prop_distributive_forward_c", "c");
			
			LinearAlgebraElementNode BCSum = new(new O_Addition(B, C));
			LinearAlgebraElementNode AProd = new(new O_Multiplication_LAElement(A, BCSum));
			return AProd;
		}
	}
	
	protected override TreeNode OutputBaseTopo
	{
		get {
			//A*B + A*C)
			LinearAlgebraElementNode A = new("prop_distributive_forward_a", "a");
			LinearAlgebraElementNode B = new("prop_distributive_forward_b", "b");
			LinearAlgebraElementNode C = new("prop_distributive_forward_c", "c");
			
			LinearAlgebraElementNode ABMult = new(new O_Multiplication_LAElement(A, B));
			LinearAlgebraElementNode ACMult = new(new O_Multiplication_LAElement(A, C));
			LinearAlgebraElementNode ABACProd = new (new O_Addition(ABMult, ACMult));
			return ABACProd;
		}
	}
}
