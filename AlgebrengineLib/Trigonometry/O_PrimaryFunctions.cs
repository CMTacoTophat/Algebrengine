namespace AlgebrengineLib.Trigonometry;

// TODO: or NOTE: element-based properties currently return the built-in interface derivers, 
// and depend on other implementers to use their specially-acclimated methods to work with them. 
// This might have to be fixed to be more convenient
public class O_Sin_Element : OperatorBase<TrigScalar, TreeNode, TrigonometricAngleElementNode>
{
	public O_Sin_Element(TreeNode sn1, TrigonometricAngleElementNode sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " sin";}}
}
public class O_Sin : OperatorBase<TrigScalar, TreeNode, TrigAngle>
{
	public O_Sin(TreeNode sn1, TrigAngle sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return " sin";}}
}