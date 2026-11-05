namespace AlgebrengineLib.Complex;

public class ComplexUnit : TreeNode
{
    public enum Unit
    {
        r,
        i,
        j,
        k
    }
    public Unit unit = Unit.r;

    public ComplexUnit(Unit u)
    {
        unit = u;
    }
    public ComplexUnit()
    {
        
    }
}