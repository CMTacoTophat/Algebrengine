using AlgebrengineLib.ElementaryAlgebra;

namespace AlgebrengineLib.Trigonometry;

/*
    TO BE MADE IN THIS FILE (TODO:)

    Pythagorean Identities
    1.      sin^2 + cos^2 = 1
    2.      tan^2 + 1 = sec^2
    3.      1 + cot^2 = csc^2

    Co-Function Identities
    1.      cos(pi/2 - x) = sin(x)
    2.      sin(pi/2 - x) = cos(x)
    3.      cot(pi/2 - x) = tan(x)
    4.      tan(pi/2 - x) = cot(x)
    5.      sec(pi/2 - x) = csc(x)
    6.      csc(pi/2 - x) = sec(x)

    Even-Odd Identities
    1.      sin(-x) = -sin(x)
    2.      cos(-x) = cos(x)
    3.      tan(-x) = -tan(x)
    4.      cot(-x) = -cot(x)
    5.      sec(-x) = sec(x)
    6.      csc(-x) = -csc(x)

    Half Angle Formulae (NOTE: requires sqrt operator!)
    1.      sin(x/2) = +-sqrt((1 - cos(x)) / 2)
    2.      cos(x/2) = +-sqrt((1 + cos(x)) / 2)
    3.      tan(x/2) = +-sqrt((1 - cos(x)) / (1 + cos(x)))
    4.      tan(x/2) = sin(x)/(1 + cos(x))
    5.      tan(x/2) = (1 - cos(x))/sin(x)

    Double Angle Formulae
    1.      sin(2x) = 2sin(x)cos(x)
    2.      sin(2x) = 2tan(x)/(1 + tan^2(x))
    3.      tan(2x) = 2tan(x)/(1 - tan^2(x))
    4.      cos(2x) = cos^2(x) - sin^2(x)
    5.      cos(2x) = 2cos^2(x) - 1
    6.      cos(2x) = (1 - tan^2(x))/(1 + tan^2(x))

    Product to Sum Formulae
    1.      cos(x)cos(y) = 1/2(cos(x - y) + cos(x + y))
    2.      sin(x)cos(y) = 1/2(sin(x + y) + sin(x - y))
    3.      sin(x)sin(y) = 1/2(cos(x - y) - cos(x + y))
    4.      cos(x)sin(y) = 1/2(sin(x + y) - sin(x - y))

    Sum to Product Formulae
    1.      sin(x) + sin(y) = 2sin((x + y)/2)cos((x - y)/2)
    2.      sin(x) - sin(y) = 2sin((x - y)/2)cos((x + y)/2)
    3.      cos(x) - cos(y) = -2sin((x + y)/2)cos((x - y)/2)
    4.      cos(x) + cos(y) = 2cos((x + y)/2)cos((x - y)/2)

    (Optional) Triple Angle Formulae
    1.      sin(3x) = 3sin(x) - 4sin^3(x)
    2.      cos(3x) = 4cos^3(x) - 3cos(x)
    3.      tan(3x) = (3tan(x) - tan^3(x))/(1 - 3tan^2(x))
    4.      csc(3x) = 1/(3sin(x) - 4sin^3(x))
    5.      sec(3x) = 1/(4cos^3(x) - 3cos(x))
    6.      cot(3x) = (1 - 3tan^2(x))/(3tan(x) - tan^3(x))

    NOTE: There do exist generic sin and cosine multiple angle
    formulae, using the "Chebyshev polynomial of the first kind",
    but its very cantankerous and requires combinatorics

    CURRENT COMPLETION: 0/34 (0%)
*/

public class P_Pythagorean : PropertyBase
{
	protected override TreeNode InputBaseTopo
	{
		get {
			TrigonometricAngleElementNode T = new("prop_pythagorean_1_theta", "theta");
            TrigonometricScalarElementNode SinNode = new(new O_Sin(new(), T));
            TrigonometricScalarElementNode CosNode = new(new O_Cos(new(), T));

            AlgebraElementNode Sin2Node = new(new O_Power(SinNode.node!, new Number(2)));
            AlgebraElementNode Cos2Node = new(new O_Power(CosNode.node!, new Number(2)));

            AlgebraElementNode SinCosSum = new(new O_Addition(Sin2Node, Cos2Node));
            return SinCosSum;
		}
	}
	
	protected override TreeNode OutputBaseTopo
	{
		get {
			return new AlgebraElementNode(new Number(1));
		}
	}
}

public class P_Pythagorean_TS : PropertyBase
{
	protected override TreeNode InputBaseTopo
	{
		get {
			TrigonometricAngleElementNode T = new("prop_pythagorean_2_theta", "theta");
            //tan^2 + 1 = sec^2
            TrigonometricScalarElementNode TanNode = new(new O_Tan(new(), T));
            AlgebraElementNode Tan2Node = new(new O_Power(TanNode.node!, new Number(2)));
            //TODO: Make system for certain numbers to be verified during CheckIfSimilar (like how this has to be one,
            //currently it would only check if it is a number)
            //(possibly with property checking override?)
            AlgebraElementNode UnityNode = new(new Number(1));

            AlgebraElementNode TanUnitySum = new(new O_Addition(Tan2Node, UnityNode));
            return TanUnitySum;
		}
	}
	
	protected override TreeNode OutputBaseTopo
	{
		get {
			TrigonometricAngleElementNode T = new("prop_pythagorean_2_theta", "theta");
            TrigonometricScalarElementNode SecNode = new(new O_Sec(new(), T));
            AlgebraElementNode Sec2Node = new(new O_Power(SecNode.node!, new Number(2)));

            return Sec2Node;
		}
	}
}