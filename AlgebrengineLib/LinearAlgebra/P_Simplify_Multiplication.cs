namespace AlgebrengineLib.LinearAlgebra;

//this file is for the vairous simplification properties for linear algebra Multiplication,
//including inner and outer products

//TODO: implement inner and outer products
public class P_Simplify_Multiplication_Scalar : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            Scalar A = new("prop_simplify_multiplication_scalar_a", "a");
			Scalar B = new("prop_simplify_multiplication_scalar_b", "b");

            Scalar ABSum = new Scalar(new O_Addition_Scalar(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A + B = C
			Scalar A = new("prop_simplify_multiplication_scalar_a", "a");
			Scalar B = new("prop_simplify_multiplication_scalar_b", "b");
			
			Scalar C = new(A.ScalarVal * B.ScalarVal);
			return C;
        }
    }
}