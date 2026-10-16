namespace AlgebrengineLib.LinearAlgebra;

//this file is for the vairous simplification properties for linear algebra addition
public class P_Simplify_Addition_Scalar : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            Scalar A = new("prop_simplify_addition_scalar_a", "a");
			Scalar B = new("prop_simplify_addition_scalar_b", "b");

            Scalar ABSum = new Scalar(new O_Addition_Scalar(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A + B = C
			Scalar A = new("prop_simplify_addition_scalar_a", "a");
			Scalar B = new("prop_simplify_addition_scalar_b", "b");
			
			Scalar C = new(A.ScalarVal + B.ScalarVal);
			return C;
        }
    }
}


// VECTOR

public class P_Simplify_Addition_Vector : PropertyBase
{
    
    public override bool CanBeApplied(TreeNode t)
    {
        try
        {
            return ((Vector)t.SubNode1).dim == ((Vector)t.SubNode2).dim;
        } catch
        {
            return false;
        }
    }
    protected override TreeNode InputBaseTopo
    {
        get
        {
            Vector A = new("prop_simplify_addition_vector_a", "vec_a");
            Vector B = new("prop_simplify_addition_vector_b", "vec_b");

            Vector ABSum = new Vector(new O_Addition_Vector(A, B));
            return ABSum;
        }
    }
    protected override TreeNode OutputBaseTopo
    {
        get
        {
            Vector A =  new("prop_simplify_addition_vector_a", "vec_a");
            Vector B =  new("prop_simplify_addition_vector_b", "vec_b");

            List<TreeNode> ABSumElements = new();

            Action<int[]> adder = (int[] ind) =>
            {
                //TODO: Figure out how to:
                // a): Get the right "new TreeNode" type for unknown element situations like this
                // b): Get the right operator for unknown element situations like this
                ABSumElements.Add(new TreeNode(new O_Addition_Scalar(A.GetVectorElement(ind[0]), B.GetVectorElement(ind[0]))));
            };

            A.ExecuteForAllIndexCombinations(adder);

            Vector C = new([.. ABSumElements]);

            return C;
        }
    }
}