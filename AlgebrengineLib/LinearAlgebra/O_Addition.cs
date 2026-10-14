namespace AlgebrengineLib.LinearAlgebra;

public class O_Addition_Scalar : OperatorBase<Scalar, Scalar, Scalar>
{
	public O_Addition_Scalar(Scalar sn1, Scalar sn2) : base(sn1, sn2)
	{
		
	}
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
	public O_Addition_Vector(Vector sn1, Vector sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition_Vector)];}}
    public override string TextRep {get {return " + ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Addition_Vector();
		}
	} 
}