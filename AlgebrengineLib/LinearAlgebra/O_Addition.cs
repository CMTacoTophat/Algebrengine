namespace AlgebrengineLib.LinearAlgebra;

public class O_Addition : OperatorBase<LinearAlgebraElementNode, LinearAlgebraElementNode, LinearAlgebraElementNode>
{
	// THIS SHOULD BE THE MODEL ELEMENT OPERATOR:
	// 1. A constructor for any generic element deriver, making new element nodes from them
	// 2. A constructor for the element nodes directly
	// 3. A constructor for the built-in element deriver
	
	// Theoretically, only #2 is needed, but the others make it much more convenient
	public O_Addition(IUnconstrainedLinearAlgebraElement sn1, IUnconstrainedLinearAlgebraElement sn2) : base(new LinearAlgebraElementNode(sn1), new LinearAlgebraElementNode(sn2))
	{
		
	}
	public O_Addition(LinearAlgebraElementNode sn1, LinearAlgebraElementNode sn2) : base(sn1, sn2)
	{
		
	}
	public O_Addition(Scalar sn1, Scalar sn2) : base(new(sn1), new(sn2))
	{
		
	}
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition)];}}
    public override string TextRep {get {return " + ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Addition();
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