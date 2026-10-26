namespace AlgebrengineLib.LinearAlgebra;

public class O_Multiplication_LAElement : OperatorBase<LinearAlgebraElementNode, LinearAlgebraElementNode, LinearAlgebraElementNode>
{
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Multiplication_LAElement)];}}
    public override string TextRep {get {return " x ";}}
    public O_Multiplication_LAElement(IUnconstrainedLinearAlgebraElement sn1, IUnconstrainedLinearAlgebraElement sn2) : base(new LinearAlgebraElementNode(sn1), new LinearAlgebraElementNode(sn2))
    {
        
    }

    public O_Multiplication_LAElement(LinearAlgebraElementNode sn1, LinearAlgebraElementNode sn2) : base(sn1, sn2)
	{
		
	}

    protected override PropertyBase simplificationProperty {get {return new P_Simplify_Multiplication_Scalar();}}
}

