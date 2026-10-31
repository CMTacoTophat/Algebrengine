namespace AlgebrengineLib.Complex;

public class P_Simplify_Addition_Quaternion : PropertyBase
{
    protected override TreeNode InputBaseTopo
    {
        get
        {
            //A + B = C
            Quaternion A = new("prop_simplify_addition_quaternion_a", "a");
			Quaternion B = new("prop_simplify_addition_quaternion_b", "b");

            Quaternion ABSum = new(new O_Addition_Quaternion(A, B));
            return ABSum;
        }
    }
    protected override TreeNode OutputBaseTopo
    {
        get
        {
            //A + B = C
            Quaternion A = new("prop_simplify_addition_quaternion_a", "a");
			Quaternion B = new("prop_simplify_addition_quaternion_b", "b");

            ImaginaryNumber RComp = A.RComp + B.RComp;
            ImaginaryNumber IComp = A.IComp + B.IComp;
            ImaginaryNumber JComp = A.JComp + B.JComp;
            ImaginaryNumber KComp = A.KComp + B.KComp;

            Quaternion C = new(RComp, IComp, JComp, KComp);
            return C;
        }
    }
}