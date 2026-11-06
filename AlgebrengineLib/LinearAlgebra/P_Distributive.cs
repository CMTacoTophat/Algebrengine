namespace AlgebrengineLib.LinearAlgebra;
using AlgebrengineLib.ElementaryAlgebra;

//This file is for the various distributive properties of each kind of linear algebra type/product
//TODO: Implement inner and outer tensor propducts

//this is an EXAMPLE class to show how the different interfaces and elements can work together
public class P_Distributive_LA_EXAMPLE : PropertyBase
{
	protected override TreeNode InputBaseTopo
	{
		get {
			//A*(B + C)
			LinearAlgebraElementNode A = new("prop_distributive_forward_a", "a");
			LinearAlgebraElementNode B = new("prop_distributive_forward_b", "b");
			LinearAlgebraElementNode C = new("prop_distributive_forward_c", "c");
			
			LinearAlgebraElementNode BCSum = new(new ElementaryAlgebra.O_Addition(B.node!, C.node!));
			LinearAlgebraElementNode AProd = new(new ElementaryAlgebra.O_Multiplication(A.node!, BCSum.node!));
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
			
			LinearAlgebraElementNode ABMult = new(new ElementaryAlgebra.O_Multiplication(A.node!, B.node!));
			LinearAlgebraElementNode ACMult = new(new ElementaryAlgebra.O_Multiplication(A.node!, C.node!));
			LinearAlgebraElementNode ABACProd = new (new ElementaryAlgebra.O_Addition(ABMult.node!, ACMult.node!));
			return ABACProd;
		}
	}
}
