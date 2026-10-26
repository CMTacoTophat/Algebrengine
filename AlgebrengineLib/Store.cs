namespace AlgebrengineLib;

public interface IVariable
{
	public void ReplaceAsVariable(bool recurse = false);
}


public static class Store
{
	private static Dictionary<string, TreeNode> VariableStore = new Dictionary<string, TreeNode>
	{
	};
	//These may need to be expanded in the future, and help to reduce codespace
	public static void AddVariable(string name, TreeNode t, out bool success, bool setIfAlreadyAdded = false)
	{
		if (!VariableStore.TryAdd(name, t))
		{
			if (setIfAlreadyAdded)
			{
				SetVariable(name, t, out success, false);
			} else
			{
				success = false;
			}
		}
		else
		{
			success = true;
		}
	}

	public static void SetVariable(string name, TreeNode t, out bool success, bool addIfNotPresent = false)
	{
		if (!VariableStore.ContainsKey(name))
		{
			if (addIfNotPresent)
			{
				AddVariable(name, t, out success, false);
			} else
			{
				success = false;
			}
			
		}
		else
		{
			VariableStore[name] = t;
			success = true;
		}
	}

	public static TreeNode GetVariable(string name, out bool success)
	{
		if (VariableStore.ContainsKey(name))
		{
			success = false;
			return TreeNode.INVALID_ELEMENT("Nonexistant Variable");
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