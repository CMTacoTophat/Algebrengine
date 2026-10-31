namespace AlgebrengineLib.Basics;

using AlgebrengineLib.Complex;
using AlgebrengineLib.LinearAlgebra;

/*
EXAMPLE IMPLEMENTATION OF A NODE FOR TWO ALGEBRAS:

Here, the "Number" node can be used in either algebra as their own sort of root element. For instance, it can
be the scalar for the linear algebra library and the real for the complex library -
(ex. "Scalar s = new(new O_Addition_LAElement(this, this));").

It does this by using both of the namespaces, inheriting each library, and then filling out the required methods.
For more complex objects that DONT exhibit the same behavior, this type of inheritance is required (instead of
creating an implicit cast). For instance, if the "Number" needed to track how many times it was added, it could
do so in the methods, and the algebras would still call them to respect it.
*/
public class Number : TreeNode, IConstrainedComplexElement<Number, Number>, IConstrainedLinearAlgebraElement<Number, Number>
{
    public double NumberVal {get; private set;} = double.NaN;
	public Number(double val) : base(val) {
		NumberVal = val;
        //Example statement below: notice how there are no compiler warnings if mathematically valid
        Number n = new Number(new O_Addition(new Number(1), new Scalar(2)));
	}

    public Number(IConstrainedOutput<LinearAlgebraElementNode> O) : base(O, false) {}

    public Number Add(Number n)
    {
        return new(this.NumberVal + n.NumberVal);
    }

    public Number Multiply(Number n)
    {
        return new(this.NumberVal * n.NumberVal);
    }

    //Interface-specific methods =======================================

    //to be a Complex node
    public Number Add(Real r)
    {
        return new(this.NumberVal + r.RealVal);
    }

    public Number Multiply(Real r)
    {
        return new(this.NumberVal * r.RealVal);
    }

    //to be a Linear Algebra node

    public Number Add(Scalar s)
    {
        return new(this.NumberVal + s.ScalarVal);
    }

    public Number Multiply(Scalar s)
    {
        return new(this.NumberVal * s.ScalarVal);
    }

    //to be both (by the virtue of the methods being of the same form)
    public void Negate()
    {
        this.NumberVal *= -1;
    }
    public void Invert()
    {
        this.NumberVal = 1/this.NumberVal;
    }
}