namespace AlgebrengineLib.LinearAlgebra;

//this file is for the vairous simplification properties for linear algebra Multiplication,
//including inner and outer products

//TODO: implement inner and outer products
public class P_Simplify_Multiplication_LAElement : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            LinearAlgebraElementNode A = new("prop_simplify_multiplication_scalar_a", "a");
			LinearAlgebraElementNode B = new("prop_simplify_multiplication_scalar_b", "b");

            LinearAlgebraElementNode ABSum = new(new O_Multiplication_LAElement(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A + B = C
			LinearAlgebraElementNode A = new("prop_simplify_multiplication_scalar_a", "a");
			LinearAlgebraElementNode B = new("prop_simplify_multiplication_scalar_b", "b");
			
			LinearAlgebraElementNode C = new(A.node!.Multiply(B.node!));
			return C;
        }
    }
}