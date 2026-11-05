namespace AlgebrengineLib;

//Since trees are not constructed with properties, but they are things applied later, strict type forcing
//doesn't seem necessary for now. In the future, however, one would have to make an unconstrained base, and
//most likely constrained input/output interfaces, with one generic each, representing the root node for
//the input topology and output topology. The constrained class would then have to be modified to use these
//generics for the IBT and OBT, as well as during the Apply/DefaulyApply methods.
public abstract class PropertyBase
{
	public bool enabled = true;
	protected abstract TreeNode InputBaseTopo { get; }
	protected abstract TreeNode OutputBaseTopo { get; }
	public virtual TreeNode Apply(TreeNode t, out bool success) {return DefaultApply(t, out success);}
	public virtual bool CanBeApplied(TreeNode t) { return true; }
	
	//In case there isn't anything special the property needs to check/do during the application process
	protected TreeNode DefaultApply(TreeNode t, out bool success) {
		if (!CanBeApplied(t)) {success = false; return t;}

		success = false;
		TreeNode result;
		
		for (int i = 0; i < t.alternativeArrangements.Length; i++) {
			//don't need to make a copy of IBT - the variables name are used to modify the STORE
			if (TreeNode.CheckIfSimilar(InputBaseTopo, t.alternativeArrangements[i], true, true)) {
				success = true;
				break;
			}
		}

		if (!success)
		{
			return t; //unchanged
		}

		//DO generate a copy of OBT, so its variables aren't replaced during application
		result = OutputBaseTopo.GenerateCopy(); 
		result.ReplaceAsVariable(true);
		return result;
	}
}