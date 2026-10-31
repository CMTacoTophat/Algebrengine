using System.Runtime.CompilerServices;

namespace AlgebrengineLib.LinearAlgebra;

//this file is for the various simplification properties for linear algebra addition

public class P_Simplify_Addition : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            LinearAlgebraElementNode A = new("prop_simplify_addition_la_element_a", "a");
			LinearAlgebraElementNode B = new("prop_simplify_addition_la_element_b", "b");

            LinearAlgebraElementNode ABSum = new(new O_Addition(A, B));
            return ABSum;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A + B = C
			LinearAlgebraElementNode A = new("prop_simplify_addition_la_element_a", "a");
			LinearAlgebraElementNode B = new("prop_simplify_addition_la_element_b", "b");
			
            LinearAlgebraElementNode C;
            C = new(A.node!.Add(B.node!));
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
                ABSumElements.Add(new TreeNode(new O_Addition(A.GetVectorElement(ind[0]), B.GetVectorElement(ind[0]))));
            };

            A.ExecuteForAllIndexCombinations(adder);

            Vector C = new([.. ABSumElements]);

            return C;
        }
    }
}