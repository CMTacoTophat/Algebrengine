namespace AlgebrengineLib.Complex;

public class Quaternion : TreeNode
{
    public double rVal;
    public double iVal;
    public double jVal;
    public double kVal;
    public Quaternion(string vN, string vD) : base(vN, vD) {}

	public Quaternion () : base () {}

    public Quaternion(IConstrainedOutput<Quaternion> O) : base(O)
    {
        
    }

    /*
    */
    public Quaternion(double r, double i, double j, double k)
    {
        rVal = r;
        iVal = i;
        jVal = j;
        kVal = k;

        ComplexBranch kBranch = new(new(INVALID_ELEMENT("End of Quaternion Topology", true), new ImaginaryNumber(k, ComplexUnit.Unit.k)));
        ComplexBranch jBranch = new(new(kBranch, new ImaginaryNumber(j, ComplexUnit.Unit.j)));
        ComplexBranch iBranch = new(new(jBranch, new ImaginaryNumber(i, ComplexUnit.Unit.i)));

        //TODO: MAKE STRONGLY TYPED STRUCTURAL OPERATOR AND REPLACE THIS (here and in tensor)
        TreeNode CompiledQ = new(new O_Structure(iBranch, new Real(r)));
        this.ReplaceSubNode(CompiledQ.SubNode1, 1);
        this.ReplaceSubNode(CompiledQ.SubNode2, 2);
    }
}

public class ComplexBranch : TreeNode
{
	public ComplexBranch(O_Structure O) : base(O, true)
	{
	}
}