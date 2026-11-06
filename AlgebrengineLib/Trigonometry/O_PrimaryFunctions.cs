namespace AlgebrengineLib.Trigonometry;

// NOTE: element-based properties currently return the built-in interface derivers, 
// and depend on other implementers to use their specially-acclimated methods to work with them. 
// This might have to be fixed to be more convenient

//NOTE: Only the non-inverted trig functions have required methods in the element implementers, as you can invert
//what comes after, as its likely to be a scalar of some sort

//TODO: Add hyperbolic functions (at least when the properties for these are finished)
public class O_Sin : OperatorBase<TrigonometricScalarElementNode, TreeNode, TrigonometricAngleElementNode>
{
	public O_Sin(TreeNode sn1, TrigAngle sn2) : base(sn1, new(sn2)) {}
    public O_Sin(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " sin";}}
    protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_PrimaryFunction_Sin_Element();
		}
	} 
}

public class O_Cos : OperatorBase<TrigonometricScalarElementNode, TreeNode, TrigonometricAngleElementNode>
{
	public O_Cos(TreeNode sn1, TrigAngle sn2) : base(sn1, new(sn2)) {}
    public O_Cos(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " cos";}}
    protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_PrimaryFunction_Cos_Element();
		}
	} 
}

public class O_Tan : OperatorBase<TrigonometricScalarElementNode, TreeNode, TrigonometricAngleElementNode>
{
	public O_Tan(TreeNode sn1, TrigAngle sn2) : base(sn1, new(sn2)) {}
    public O_Tan(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " cos";}}
    protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_PrimaryFunction_Tan_Element();
		}
	} 
}


//###################################################################################################
//###################################################################################################
//###################################################################################################
//###################################################################################################


public class O_Csc : OperatorBase<TrigonometricScalarElementNode, TreeNode, TrigonometricAngleElementNode>
{
	public O_Csc(TreeNode sn1, TrigAngle sn2) : base(sn1, new(sn2)) {}
    public O_Csc(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " sin";}}
    protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_PrimaryFunction_Csc_Element();
		}
	} 
}

public class O_Sec : OperatorBase<TrigonometricScalarElementNode, TreeNode, TrigonometricAngleElementNode>
{
	public O_Sec(TreeNode sn1, TrigAngle sn2) : base(sn1, new(sn2)) {}
    public O_Sec(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " cos";}}
    protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_PrimaryFunction_Sec_Element();
		}
	} 
}

public class O_Cot : OperatorBase<TrigonometricScalarElementNode, TreeNode, TrigonometricAngleElementNode>
{
	public O_Cot(TreeNode sn1, TrigAngle sn2) : base(sn1, new(sn2)) {}
    public O_Cot(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " cos";}}
    protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_PrimaryFunction_Cot_Element();
		}
	} 
}