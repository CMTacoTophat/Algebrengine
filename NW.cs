using Godot;
using System;
using System.Collections.Generic;

public partial class NW : ENBase //Short for "number wrapper" (reference - type numbers or variables)
{
    public bool isVariable;
    public double value;
    public string variableName;
    private Dictionary<string, double> VR = Main.VariableRegistry;
    public NW(double val) {
        this.value = val;
        isVariable = false;
        isExpression = false;
    }
    public NW(string vN) {
        this.variableName = vN;
        value = VR[vN];
        isVariable = true;
        isExpression = false;
    }

    public override string Stringify()
    {
        return value.ToString();
    }
}
