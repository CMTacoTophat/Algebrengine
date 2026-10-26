namespace AlgebrengineLib.Complex;

public class O_Addition_Quaternion : OperatorBase<Quaternion, Quaternion, Quaternion>
{
	public O_Addition_Quaternion(Quaternion sn1, Quaternion sn2) : base(sn1, sn2) {}
    public override bool Commutative {get {return true;}}
    public override Type[] Associatives {get {return [typeof(O_Addition_Quaternion)];}}
    public override string TextRep {get {return " + ";}}

	protected override PropertyBase simplificationProperty {get
		{
			return new P_Simplify_Addition_Quaternion();
		}
	} 
}