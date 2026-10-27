namespace AlgebrengineLib.Complex;

public class O_ImaginaryAssignment : OperatorBase<ImaginaryNumber, ComplexElementNode, ComplexUnit>
{
    public O_ImaginaryAssignment(ComplexElementNode r, ComplexUnit c) : base(r, c) {}
    public O_ImaginaryAssignment(Real r, ComplexUnit c) : base(new ComplexElementNode(r), c) {}
    public override bool Commutative {get {return false;}}
    public override Type[] Associatives {get {return [];}}
    public override string TextRep {get {return "";}}

	protected override PropertyBase simplificationProperty { get{ return new P_Reflexive();}}
}