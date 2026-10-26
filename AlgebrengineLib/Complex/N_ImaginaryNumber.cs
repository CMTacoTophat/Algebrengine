namespace AlgebrengineLib.Complex;

public class ImaginaryNumber : TreeNode
{
    public ImaginaryNumber(string vN, string vD) : base(vN, vD) {}

	public ImaginaryNumber() : base () {}
    public ImaginaryNumber(IConstrainedOutput<ImaginaryNumber> o) : base(o)
    {
        
    }
    public ImaginaryNumber(double num, ComplexUnit.Unit u)
    {
        Real r = new Real(num);
        ComplexUnit c = new ComplexUnit(u);
        this.ReplaceWithTree(new(new O_ImaginaryAssignment(r, c)));
    }

    public ComplexUnit GetUnit()
    {
        return (ComplexUnit)SubNode2;
    }
    public Real GetNumber()
    {
        return (Real)SubNode1;
    }
}