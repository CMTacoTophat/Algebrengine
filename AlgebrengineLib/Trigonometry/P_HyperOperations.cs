namespace AlgebrengineLib.ElementaryAlgebra;

//Exmaples of methods that need to override the typical topology:
//turning A + A + A ... into N*A and back
public class P_Add_to_Mult : PropertyBase<AlgebraElementNode>
{
    protected override AlgebraElementNode InputBaseTopo => throw new NotImplementedException();

    protected override AlgebraElementNode OutputBaseTopo => throw new NotImplementedException();

    public override AlgebraElementNode Apply(AlgebraElementNode t, out bool success)
    {
        //TODO: Implement


        return base.Apply(t, out success);
    }
}

public class P_Mult_to_Add : PropertyBase<AlgebraElementNode>
{
    protected override AlgebraElementNode InputBaseTopo => throw new NotImplementedException();

    protected override AlgebraElementNode OutputBaseTopo => throw new NotImplementedException();

    public override AlgebraElementNode Apply(AlgebraElementNode t, out bool success)
    {
        if (!CanBeApplied(t))
        {
            success = false;
            return t;   
        }

        //TODO: Implement

        return base.Apply(t, out success);
    }

    public override bool CanBeApplied(AlgebraElementNode t)
    {
        //TODO: Implement
        return true;
    }
}