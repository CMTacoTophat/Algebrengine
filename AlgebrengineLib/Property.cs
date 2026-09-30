namespace AlgebrengineLib;

public abstract class PropertyBase
{
	public bool enabled = true;
	protected abstract TreeNode InputBaseTopo { get; }
	protected abstract TreeNode OutputBaseTopo { get; }

	//TODO: check over this process again
	public abstract TreeNode Apply(TreeNode t);
	
	//In case there isn't anything special the property needs to check/do during the application process
	protected TreeNode DefaultApply(TreeNode t) {
		TreeNode matchingApplicant;
		TreeNode result;
		
		for (int i = 0; i < t.alternativeArrangements.Length; i++) {
			if (TreeNode.CheckIfSimilar(InputBaseTopo, t.alternativeArrangements[i], true, true)) {
				matchingApplicant = t.alternativeArrangements[i];
				break;
			}
		}
		result = InputBaseTopo.GenerateCopy(); //generate a copy, so the IBT template's variables aren't replaced during application
		result.ReplaceAsVariable(true);
		return result;
	}
}