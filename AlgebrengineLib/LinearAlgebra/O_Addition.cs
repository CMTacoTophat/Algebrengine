namespace AlgebrengineLib.LinearAlgebra;

public class O_Addition_Scalar : OperatorBase<Scalar, Scalar, Scalar>
{

    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition_Scalar)];}}
    public override string TextRep {get {return " + ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Addition_Scalar();
		}
	} 
}

public class O_Addition_Vector : OperatorBase<Vector, Vector, Vector>
{

    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition_Vector)];}}
    public override string TextRep {get {return " + ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Addition_Vector();
		}
	} 
}