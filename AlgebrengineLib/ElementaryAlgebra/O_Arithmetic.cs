namespace AlgebrengineLib.ElementaryAlgebra;
public class O_Addition : OperatorBase<AlgebraElementNode, AlgebraElementNode, AlgebraElementNode>
{
	public O_Addition(IUnconstrainedAlgebraElement sn1, IUnconstrainedAlgebraElement sn2) : base(new AlgebraElementNode(sn1), new AlgebraElementNode(sn2))
	{
		
	}
	public O_Addition(AlgebraElementNode sn1, AlgebraElementNode sn2) : base(sn1, sn2)
	{
		
	}
	public O_Addition(Number sn1, Number sn2) : base(new(sn1), new(sn2))
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

public class O_Multiplication : OperatorBase<AlgebraElementNode, AlgebraElementNode, AlgebraElementNode>
{
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Multiplication)];}}
    public override string TextRep {get {return " x ";}}
    public O_Multiplication(IUnconstrainedAlgebraElement sn1, IUnconstrainedAlgebraElement sn2) : base(new AlgebraElementNode(sn1), new AlgebraElementNode(sn2))
    {
        
    }

    public O_Multiplication(AlgebraElementNode sn1, AlgebraElementNode sn2) : base(sn1, sn2)
	{
		
	}

    protected override PropertyBase simplificationProperty {get {return new P_Simplify_Multiplication();}}
}

public class O_Power : OperatorBase<AlgebraElementNode, AlgebraElementNode, AlgebraElementNode>
{
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " ^ ";}}
    public O_Power(IUnconstrainedAlgebraElement sn1, IUnconstrainedAlgebraElement sn2) : base(new AlgebraElementNode(sn1), new AlgebraElementNode(sn2))
    {
        
    }

    public O_Power(AlgebraElementNode sn1, AlgebraElementNode sn2) : base(sn1, sn2)
	{
		
	}

    protected override PropertyBase simplificationProperty {get {return new P_Simplify_Exponentiation();}}
}