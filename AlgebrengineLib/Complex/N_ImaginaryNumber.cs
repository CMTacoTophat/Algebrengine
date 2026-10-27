namespace AlgebrengineLib.Complex;

public class ImaginaryNumber : TreeNode
{
    public ImaginaryNumber(string vN, string vD) : base(vN, vD) {}

	public ImaginaryNumber() : base () {}
    public ImaginaryNumber(IConstrainedOutput<ImaginaryNumber> o) : base(o)
    {
        
    }
    //=========================================================================
    //TODO: Clean up constructors
    public ImaginaryNumber(double num, ComplexUnit.Unit u)
    {
        Real r = new(num);
        ComplexUnit c = new ComplexUnit(u);
        this.ReplaceWithTree(new(new O_ImaginaryAssignment(r, c)));
    }
    public ImaginaryNumber(double num, ComplexUnit i)
    {
        Real r = new(num);
        this.ReplaceWithTree(new(new O_ImaginaryAssignment(r, i)));
    }
    public ImaginaryNumber(ComplexElementNode r, ComplexUnit.Unit u)
    {
        ComplexUnit c = new ComplexUnit(u);
        this.ReplaceWithTree(new(new O_ImaginaryAssignment(r, c)));
    }
    public ImaginaryNumber(ComplexElementNode r, ComplexUnit i)
    {
        this.ReplaceWithTree(new(new O_ImaginaryAssignment(r, i)));
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