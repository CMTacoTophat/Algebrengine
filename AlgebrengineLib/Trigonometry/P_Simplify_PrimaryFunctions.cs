namespace AlgebrengineLib.Trigonometry;

public class P_Simplify_PrimaryFunction_Sin_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            //A + B = C
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_sin_element_theta", "theta");

            //TODO: verify or create special node for same-checking for one-child nodes (like a sin func)
            TrigScalar SinFunc = new(new O_Sin(new(), T));
            return SinFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            //A + B = C
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_sin_element_theta", "theta");
            TrigScalar R = T.node!.Sin();
			return R;
        }
    }
}