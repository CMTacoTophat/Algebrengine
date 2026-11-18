namespace AlgebrengineLib.LinearAlgebra;

//TODO: implement inner and outer products

//This should be covered instead by the multiplication class from Algebrengine.ElementaryAlgebra
/*public class P_Simplify_Multiplication : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            LinearAlgebraElementNode A = new("prop_simplify_multiplication_scalar_a", "a");
			LinearAlgebraElementNode B = new("prop_simplify_multiplication_scalar_b", "b");

            LinearAlgebraElementNode ABSum = new(new O_Multiplication(A, B));
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
}*/