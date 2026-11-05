namespace AlgebrengineLib.Complex;

public class P_Simplify_Multiplication_Quaternion : PropertyBase
{
    protected override TreeNode InputBaseTopo
    {
        get
        {
            //A * B = C
            Quaternion A = new("prop_simplify_multiplication_quaternion_a", "a");
			Quaternion B = new("prop_simplify_multiplication_quaternion_b", "b");

            Quaternion ABProd = new(new O_Multiplication_Quaternion(A, B));
            return ABProd;
        }
    }
    protected override TreeNode OutputBaseTopo
    {
        get
        {
            //A * B = C
            Quaternion A = new("prop_simplify_multiplication_quaternion_a", "a");
			Quaternion B = new("prop_simplify_multiplication_quaternion_b", "b");

            // NOTE: multiplication should already take care of the signs, so just use positive addition
            ImaginaryNumber RComp = A.RComp*B.RComp + A.IComp*B.IComp + A.JComp*B.JComp + A.KComp*B.KComp;
            ImaginaryNumber IComp = A.RComp*B.IComp + A.IComp*B.RComp + A.JComp*B.KComp + A.KComp*B.JComp;
            ImaginaryNumber JComp = A.RComp*B.JComp + A.IComp*B.KComp + A.JComp*B.RComp + A.KComp*B.IComp;
            ImaginaryNumber KComp = A.RComp*B.KComp + A.IComp*B.JComp + A.JComp*B.IComp + A.KComp*B.RComp;

            Quaternion C = new(RComp, IComp, JComp, KComp);
            return C;
        }
    }
}