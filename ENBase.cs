using Godot;
using System;

public partial class ENBase : Node //short for Expression and Number Base
{
    public bool isExpression; //false --> it is a number
    public virtual string Stringify() {return "";}
}
