using System.CodeDom.Compiler;
using AlgebrengineLib.Basics;

namespace AlgebrengineLib.Complex;

public class ImaginaryNumber : TreeNode
{
    public bool filledOut; //TODO: come up with better name 
    public ComplexUnit.Unit unit;



    //BASIC CONSTRUCTORS
    public ImaginaryNumber(string vN, string vD) : base(vN, vD) {}

	public ImaginaryNumber() : base () {}
    public ImaginaryNumber(IConstrainedOutput<ImaginaryNumber> o) : base(o) {}

    //SINGLE VALUE/NODE: assume real only
    public ImaginaryNumber(double num)
    {
        
    }
    public ImaginaryNumber(ComplexElementNode r)
    {
        
    }

    //DOUBLE VALUE AND/OR NODE: assign
    public ImaginaryNumber(double num, ComplexUnit.Unit u) : base(new O_ImaginaryAssignment(new Real(num), new ComplexUnit(u)))
    {
        unit = u;
    }
    public ImaginaryNumber(double num, ComplexUnit i)
    {
        unit = i.unit;
    }
    public ImaginaryNumber(ComplexElementNode r, ComplexUnit.Unit u) : base(new O_ImaginaryAssignment(r, new ComplexUnit(u)))
    {
        unit = u;
    }
    public ImaginaryNumber(ComplexElementNode r, ComplexUnit i)
    {
        unit = i.unit;
    }

    

    public ComplexUnit GetUnit()
    {
        return filledOut ? (ComplexUnit)SubNode2 : INVALID_ELEMENT<ComplexUnit>("Imaginary Number unit identifier not set properly");
    }
    public ComplexElementNode GetNumber()
    {
        //WARNING: creation warning suppressed, even though it can be returned conditionally
        ComplexElementNode invalid = INVALID_ELEMENT<ComplexElementNode>("Imaginary Number real component not set properly", true);

        if (!filledOut)
        {
            return invalid;
        }

        if (SubNode1 is ComplexElementNode c)
        {
            return c;
        } else if (SubNode1 is IUnconstrainedComplexElement i)
        {
            return new(i);
        } else
        {
            return invalid;
        }
    }

    //TODO: Check if these methods break the standard architecture
    public ImaginaryNumber Add(ImaginaryNumber other)
    {
        if (other.unit != this.unit)
        {
            return INVALID_ELEMENT<ImaginaryNumber>("Cannot add imaginary numbers of differing components " + other.unit + " (other) and " + this.unit + " (this)");
        }

        ComplexElementNode num = new(this.GetNumber().node!.Add(other.GetNumber().node!));
        return new(num, unit);
    }

    public ImaginaryNumber Subtract(ImaginaryNumber other)
    {
        if (other.unit != this.unit)
        {
            return INVALID_ELEMENT<ImaginaryNumber>("Cannot subtract imaginary numbers of differing components " + other.unit + " (other) and " + this.unit + " (this)");
        }

        ComplexElementNode negativeOther = new(other.GetNumber().node!);
        negativeOther.node!.Negate();

        ComplexElementNode num = new(this.GetNumber().node!.Add(negativeOther.node!));
        return new(num, unit);
    }

    public static ImaginaryNumber operator +(ImaginaryNumber left, ImaginaryNumber right)
    {
        return left.Add(right);
    }

    public static ImaginaryNumber operator -(ImaginaryNumber left, ImaginaryNumber right)
    {
        return left.Subtract(right);
    }

    public static ImaginaryNumber operator *(ImaginaryNumber left, ImaginaryNumber right)
    {
        return left.Multiply(right);
    }

    /*
        MULTIPLICATION RULES:

        i^2 = j^2 = k^2 = ijk = -1

        ij = k, and ji = -k
        jk = i, and kj = -i
        ki = j, and ik = -j
    */

    //Possibly TODO: Given the possibility of having i*j*k as its own multiplication, add a three-multiply
    //WARNING: INTENDED TO BE CALLED WITH this AS SubNode1 AND other AS SubNode2
    public ImaginaryNumber Multiply(ImaginaryNumber other)
    {
        //TODO: Confirm logic
        ComplexUnit c;
        bool negate = false;

        if (this.unit == other.unit)
        {
            c = new(ComplexUnit.Unit.r);
            negate = true;
        } else if (this.unit != ComplexUnit.Unit.r && other.unit != ComplexUnit.Unit.r)
        {
            ComplexUnit.Unit[] comps = [ComplexUnit.Unit.i, ComplexUnit.Unit.j, ComplexUnit.Unit.k];
            int ind1 = comps.IndexOf(this.unit);
            int ind2 = comps.IndexOf(other.unit);

            int diff = ind2 - ind1;
            negate = !(diff > 0 || (ind1 == 2 && ind2 == 0));
            int newInd = (ind2 + diff + 3) % 3;

            c = new(comps[newInd]);
        } else
        {
            if (this.unit == ComplexUnit.Unit.r)
            {
                c = new(other.unit);
            } else
            {
                c = new(this.unit);
            }
        }

        ComplexElementNode num = new(this.GetNumber().node!.Multiply(other.GetNumber().node!));
        if (negate)
        {
            num.node!.Negate();
        }

        return new(num, c);
    }

    //TODO: Improve
    public ImaginaryNumber Negated()
    {
        GetNumber().node!.Negate();
        return this;
    }
}