namespace AlgebrengineLib.LinearAlgebra;

//Care should be taken with this class!
public class ImaginaryUnit : TreeNode
{
    public enum Component //TODO: add support for quaternions (only I for normal complex numbers)
    {
        real, //for reference in the dictionary, NOT for external use
        I,
        J,
        K,
    }
    public Component comp;
    public ImaginaryUnit(Component c = Component.I) : base(1) //basically a unit vector
    {
        comp = c;
    }

    private static readonly Dictionary<Tuple<Component, Component>, Component> multiplicationRule = new()
    {
        {new Tuple<Component, Component>(Component.I, Component.I), Component.real},
    };

    public static Component GetMultiplicationComponent(Component c1, Component c2)
    {
        return multiplicationRule[new Tuple<Component, Component>(c1, c2)];
    }
}