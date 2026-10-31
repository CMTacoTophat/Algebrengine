namespace AlgebrengineLib.Complex;

public class O_Multiplication_Quaternion : OperatorBase<Quaternion, Quaternion, Quaternion>
{
	public O_Multiplication_Quaternion(Quaternion sn1, Quaternion sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return false;}} //<--- IMPORTANT
    public override Type[] Associatives {get {return [typeof(O_Multiplication_Quaternion)];}}
    public override string TextRep {get {return " * ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Multiplication_Quaternion();
		}
	} 
}