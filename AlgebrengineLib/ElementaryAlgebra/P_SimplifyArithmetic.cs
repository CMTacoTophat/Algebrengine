namespace AlgebrengineLib.ElementaryAlgebra;

public class P_Simplify_Addition : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            AlgebraElementNode A = new("prop_simplify_addition_number_a", "a");
			AlgebraElementNode B = new("prop_simplify_addition_number_b", "b");

            AlgebraElementNode ABSum = new(new O_Addition(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A + B = C
			AlgebraElementNode A = new("prop_simplify_addition_number_a", "a");
			AlgebraElementNode B = new("prop_simplify_addition_number_b", "b");
			
            AlgebraElementNode C;
            C = new(A.node!.Add(B.node!));
			return C;
        }
    }
}

public class P_Simplify_Multiplication : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A * B = C
            AlgebraElementNode A = new("prop_simplify_multiplication_number_a", "a");
			AlgebraElementNode B = new("prop_simplify_multiplication_number_b", "b");

            AlgebraElementNode ABSum = new(new O_Multiplication(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A * B = C
			AlgebraElementNode A = new("prop_simplify_multiplication_number_a", "a");
			AlgebraElementNode B = new("prop_simplify_multiplication_number_b", "b");
			
			AlgebraElementNode C = new(A.node!.Multiply(B.node!));
			return C;
        }
    }
}

public class P_Simplify_Exponentiation : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A ^ B = C
            AlgebraElementNode A = new("prop_simplify_exponentiation_number_a", "a");
			AlgebraElementNode B = new("prop_simplify_exponentiation_number_b", "b");

            AlgebraElementNode ABSum = new(new O_Power(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A ^ B = C
			AlgebraElementNode A = new("prop_simplify_exponentiation_number_a", "a");
			AlgebraElementNode B = new("prop_simplify_exponentiation_number_b", "b");
			
			AlgebraElementNode C = new(A.node!.Power(B.node!));
			return C;
        }
    }
}