namespace AlgebrengineLib;

//basic properties, for structure or general use

//this one is primarily used for special objects where they shouldn't have any simplification properties related to them

public class P_Reflexive<T> : PropertyBase<T> where T : TreeNode, new()
{
    protected override T InputBaseTopo { get
        {
            T node = new T();
            node.MakeVariable("prop_reflexive", "A");
            return node;
        }
    }

    protected override T OutputBaseTopo {get
        {
            T node = new T();
            node.MakeVariable("prop_reflexive", "A");
            return node;
        }
    }
}