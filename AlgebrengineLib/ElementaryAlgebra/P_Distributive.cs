namespace AlgebrengineLib.ElementaryAlgebra;

public class P_Distributive_Forward : PropertyBase<AlgebraElementNode>
{
    protected override AlgebraElementNode InputBaseTopo
    {
        get
        {
            //A(B + C) = AB + AC
            AlgebraElementNode A = new("prop_distributive_forward_a", "A");
            AlgebraElementNode B = new("prop_distributive_forward_b", "B");
            AlgebraElementNode C = new("prop_distributive_forward_c", "C");

            AlgebraElementNode BCSum = new(new O_Addition(B.node!, C.node!));
            AlgebraElementNode AProd = new(new O_Multiplication(A.node!, BCSum.node!)); 
            //TODO: Verify using the node property of an ElementNode properly represents the tree

            return AProd;
        }
    }

    protected override AlgebraElementNode OutputBaseTopo
    {
        get
        {
            AlgebraElementNode A = new("prop_distributive_forward_a", "A");
            AlgebraElementNode B = new("prop_distributive_forward_b", "B");
            AlgebraElementNode C = new("prop_distributive_forward_c", "C");

            AlgebraElementNode ABProd = new(new O_Multiplication(A.node!, B.node!));
            AlgebraElementNode ACProd = new(new O_Multiplication(A.node!, C.node!));
            AlgebraElementNode BCSum = new(new O_Addition(ABProd.node!, ACProd.node!));
            return BCSum;
        }
    }
}