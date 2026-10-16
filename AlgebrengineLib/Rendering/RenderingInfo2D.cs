namespace AlgebrengineLib.Rendering;

//Assuming each node is scaled properly, so a symbol like "a" is ~1x1 scale (or they're all at least made consistently), go up the tree
public class RenderingInfo2D
{
	//NOTE: origin at top left, Y going DOWN
	public bool isRootRenderable; //is this the root object to be rendered?
	private static double windowDimensionX; //any units, as long as they are drawn to similarly
	private static double windowDimensionY;
	private static double windowMarginX;
	private static double windowMarginY;
	public static void SetWindowInformation(double wDX, double wDY, double wMX, double wMY)
	{
		windowDimensionX = wDX;
		windowDimensionY = wDY;
		windowMarginX = wMX;
		windowMarginY = wMY;
	}
	private static double fittingScaleX; //WARNING: this only defines behavior for ONE root node in ONE window!
	private static double fittingScaleY;
	public int childNumber = -1;
	//Scale and position of children nodes for both going up AND down the tree
	//set by the node's mathematical type, used to calculate ____Relative values
	//(of this node) going UP and ___Absolute values (of child nodes)
	public double child1ScaleX;
	public double child1ScaleY;
	public double child2ScaleX;
	public double child2ScaleY;
	//Position of children relative to this parent's position (in terms of scale)
	public double child1OffsetX;
	public double child1OffsetY;
	public double child2OffsetX;
	public double child2OffsetY;
	//the actual children
	public RenderingInfo2D? child1;
	public RenderingInfo2D? child2;
	public RenderingInfo2D(double c1SX, double c1SY, double c2SX, double c2SY, double c1OX, double c1OY, double c2OX, double c2OY)
	{
		child1ScaleX = c1SX;
		child1ScaleY = c1SY;
		child2ScaleX = c2SX;
		child2ScaleY = c2SY;
		child1OffsetX = c1OX;
		child1OffsetY = c1OY;
		child2OffsetX = c2OX;
		child2OffsetY = c2OY;
	}

	//Scale and position going UP the tree (calculated from the sub-nodes)
	public double ScaleXRelative;
	public double ScaleYRelative;
	public double OffsetXRelative;
	public double OffsetYRelative;
	//Scale and position going DOWN the tree (set from the parent nodes)
	public double ScaleXAbsolute;
	public double ScaleYAbsolute;
	public double OffsetXAbsolute;
	public double OffsetYAbsolute;
	//Going UP the tree
	public void CalculateRelativeValues()
	{
		if (child1 is null || child2 is null)
		{
			return;
		}
		//CALCULATE RELATIVES AND CALL THIS FUNCTION FOR THE PARENT
		//offset from the parent's center, combined with any internal offset
		double xMin = Math.Min(child1OffsetX + child1ScaleX * (child1.OffsetXRelative), child2OffsetX + child2ScaleX * (child2.OffsetXRelative));
		//offset from the parent's center, combined with any internal offset and the child's own scale
		double xMax = Math.Max(child1OffsetX + child1ScaleX * (child1.OffsetXRelative + child1.ScaleXRelative), child2OffsetX + child2ScaleX * (child2.OffsetXRelative + child2.ScaleXRelative));
		//================================
		//Y values
		double yMin = Math.Min(child1OffsetY + child1ScaleY * (child1.OffsetYRelative), child2OffsetY + child2ScaleY * (child2.OffsetYRelative));
		double yMax = Math.Max(child1OffsetY + child1ScaleY * (child1.OffsetYRelative + child1.ScaleYRelative), child2OffsetY + child2ScaleY * (child2.OffsetYRelative + child2.ScaleYRelative));
		ScaleXRelative = xMax - xMin;
		ScaleYRelative = yMax - yMin;
		OffsetXRelative = xMin;
		OffsetYRelative = yMin;
	}

	private bool child1Ready = false;
	private bool child2Ready = false;
	private void ReadyChild(int cNum)
	{
		if (cNum == 0)
		{
			child1Ready = true;
		}
		else
		{
			child2Ready = true;
		}

		if (child1Ready && child2Ready && !isRootRenderable)
		{
			CalculateRelativeValues();
		}
		else if (isRootRenderable)
		{
			this.ScaleXAbsolute = windowDimensionX - 2 * windowMarginX;
			this.ScaleYAbsolute = windowDimensionY - 2 * windowMarginY;
			this.OffsetXAbsolute = windowDimensionX / 2 - ScaleXAbsolute / 2; //center
			this.OffsetYAbsolute = windowDimensionY / 2 - ScaleYAbsolute / 2;
			fittingScaleX = this.ScaleXAbsolute / this.ScaleXRelative; //how big it actually is vs. how big it was designated as by the children
			fittingScaleY = this.ScaleYAbsolute / this.ScaleYRelative;
		}
	}

	public void CalculateAbsoluteValues()
	{
		//CALCULATE ABSOLUTES AND CALL THIS FUNCTION FOR THE CHILDREN
		this.ScaleXAbsolute = this.ScaleXRelative * fittingScaleX;
		this.ScaleYAbsolute = this.ScaleYRelative * fittingScaleY;
	    //TODO: Finish
	}
}