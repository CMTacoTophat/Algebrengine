namespace AlgebrengineLib;

public static class Store
{
	private static Dictionary<string, TreeNode> VariableStore = new Dictionary<string, TreeNode>
	{
	};
	//These may need to be expanded in the future, and help to reduce codespace
	public static void AddVariable(string name, TreeNode t, out bool success)
	{
		if (VariableStore.ContainsKey(name))
		{
			success = false;
		}
		else
		{
			VariableStore.Add(name, t);
			success = true;
		}
	}

	public static TreeNode GetVariable(string name, out bool success)
	{
		if (VariableStore.ContainsKey(name))
		{
			success = false;
			return new INVALID_ELEMENT("Nonexistant Variable");
		}
		else
		{
			success = true;
			return VariableStore[name];
		}
	}

	public static void RemoveVariable(string name, out bool success)
	{
		if (VariableStore.ContainsKey(name))
		{
			success = false;
		}
		else
		{
			success = true;
			VariableStore.Remove(name);
		}
	}
}