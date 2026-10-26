namespace AlgebrengineLib.LinearAlgebra;

public class O_Addition_LAElement : OperatorBase<LinearAlgebraElementNode, LinearAlgebraElementNode, LinearAlgebraElementNode>
{
	public O_Addition_LAElement(IUnconstrainedLinearAlgebraElement sn1, IUnconstrainedLinearAlgebraElement sn2) : base(new LinearAlgebraElementNode(sn1), new LinearAlgebraElementNode(sn2))
	{
		
	}
	public O_Addition_LAElement(LinearAlgebraElementNode sn1, LinearAlgebraElementNode sn2) : base(sn1, sn2)
	{
		
	}
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition_LAElement)];}}
    public override string TextRep {get {return " + ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Addition_LAElement();
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