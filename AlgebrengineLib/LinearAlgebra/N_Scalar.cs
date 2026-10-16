namespace AlgebrengineLib.LinearAlgebra;

//NOTE: while this should technically also inherit from tensor or vector, it is used
//so frequently on it's own, uses the TreeNode constructor, and doesn't have structural element children,
//so it really shouldn't
//(also, because everything is a generalization, one could just make a tensor of rank 0 anyway
public class Scalar : TreeNode 
{
	public double ScalarVal {get; private set;} = float.NaN;
	public Scalar(double val) : base(val) {
		ScalarVal = val;
	}
	
	public Scalar(IConstrainedOutput<Scalar> O) : base(O, false) {}

	public Scalar(string vN, string vD) : base(vN, vD) {}

	public Scalar() : base()
	{
		ScalarVal = 0;
	}
}