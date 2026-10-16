namespace AlgebrengineLib.LinearAlgebra;

public class O_Multiplication_Scalar : OperatorBase<Scalar, Scalar, Scalar>
{
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Multiplication_Scalar)];}}
    public override string TextRep {get {return " x ";}}
    public O_Multiplication_Scalar(Scalar sn1, Scalar sn2)
    {
        
    }

    protected override PropertyBase simplificationProperty {get {return new P_Simplify_Multiplication_Scalar();}}
}

