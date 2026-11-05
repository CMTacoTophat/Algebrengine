namespace AlgebrengineLib;

//basic properties, for structure or general use

//this one is primarily used for special objects where they shouldn't have any simplification properties related to them

public class P_Reflexive : PropertyBase
{
    protected override TreeNode InputBaseTopo { get
        {
            return new TreeNode("prop_reflexive", "A"); //might need a better display text (currently "A = A")
        }
    }

    protected override TreeNode OutputBaseTopo {get
        {
            return new TreeNode("prop_reflexive", "A");
        }
    }
}