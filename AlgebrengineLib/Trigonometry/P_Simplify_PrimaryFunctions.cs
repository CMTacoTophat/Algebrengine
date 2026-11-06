namespace AlgebrengineLib.Trigonometry;


//TODO: Replace "theta" with actual theta character
public class P_Simplify_PrimaryFunction_Sin_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_sin_element_theta", "theta");

            TrigScalar TrigFunc = new(new O_Sin(new(), T));
            return TrigFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_sin_element_theta", "theta");
            TrigonometricScalarElementNode R = new(T.node!.Sin());
			return R;
        }
    }
}

public class P_Simplify_PrimaryFunction_Cos_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_cos_element_theta", "theta");

            TrigScalar TrigFunc = new(new O_Cos(new(), T));
            return TrigFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_cos_element_theta", "theta");
            TrigonometricScalarElementNode R = new(T.node!.Cos());
			return R;
        }
    }
}

public class P_Simplify_PrimaryFunction_Tan_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_tan_element_theta", "theta");

            TrigScalar TrigFunc = new(new O_Tan(new(), T));
            return TrigFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_tan_element_theta", "theta");
            TrigonometricScalarElementNode R = new(T.node!.Tan());
			return R;
        }
    }
}

//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

public class P_Simplify_PrimaryFunction_Csc_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_csc_element_theta", "theta");

            TrigScalar TrigFunc = new(new O_Sin(new(), T));
            return TrigFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_csc_element_theta", "theta");
            TrigonometricScalarElementNode R = new(T.node!.Sin());
            R.node!.Invert(); //as a reciprocal function
			return R;
        }
    }
}

public class P_Simplify_PrimaryFunction_Sec_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_sec_element_theta", "theta");

            TrigScalar TrigFunc = new(new O_Sec(new(), T));
            return TrigFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_sec_element_theta", "theta");
            TrigonometricScalarElementNode R = new(T.node!.Cos());
            R.node!.Invert();
			return R;
        }
    }
}

public class P_Simplify_PrimaryFunction_Cot_Element : PropertyBase
{
    protected override TreeNode InputBaseTopo {get
        {
            TrigonometricAngleElementNode T = new("prop_simplify_pfunction_cot_element_theta", "theta");

            TrigScalar TrigFunc = new(new O_Cot(new(), T));
            return TrigFunc;
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
			TrigonometricAngleElementNode T = new("prop_simplify_pfunction_cot_element_theta", "theta");
            TrigonometricScalarElementNode R = new(T.node!.Tan());
            R.node!.Invert();
			return R;
        }
    }
}