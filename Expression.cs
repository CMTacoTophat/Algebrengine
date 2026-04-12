using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Expression : ENBase
{
    public bool IsInvalidResult = false;
    public Operation operation;
    public ENBase EN1;
    public ENBase EN2;
    public Expression(Operation op, ENBase n1, ENBase n2) {
        operation = op;
        EN1 = n1;
        EN2 = n2;
        isExpression = true;
    }
    public Expression() {

    }
    
    public enum Operation {
        Addition,
        Multiplication,
        Exponentiation,
        Logarithm,
        Trigonometric,
        Modulate,
    }
    public static Dictionary<Operation, Func<double, double, double>> Operators  = new Dictionary<Operation, Func<double, double, double>> {
        {Operation.Addition, (a, b) => {return a + b;}},
        {Operation.Multiplication, (a, b) => {return a * b;}},
        {Operation.Exponentiation, (a, b) => {return Math.Pow(a, b);}},
        {Operation.Logarithm, (a, b) => {return Math.Log(b, a);}}, //log BASE A of INPUT B
        {Operation.Trigonometric, (a, b) => {return TrigonomentricLookup[(int)a].Item1(b);}}, //trig function A of input B
        {Operation.Modulate, (a, b) => {return a % b;}},
    };
    public static Dictionary<int, Tuple<Func<double, double>, string>> TrigonomentricLookup = new Dictionary<int, Tuple<Func<double, double>, string>> {
        {0, new Tuple<Func<double, double>, string>(Math.Sin, "sin")},
        {1, new Tuple<Func<double, double>, string>(Math.Cos, "cos")},
        {2, new Tuple<Func<double, double>, string>(Math.Tan, "tan")},
        {3, new Tuple<Func<double, double>, string>(Math.Asin, "arcsin")},
        {4, new Tuple<Func<double, double>, string>(Math.Acos, "arccos")},
        {5, new Tuple<Func<double, double>, string>(Math.Atan, "arctan")},
    };

    public static string OperationToChar(Operation o, ENBase v1, ENBase v2, short v3) { //may also have to carry extra information (v3)
        switch (o) {
            case Operation.Addition:
                return "+";
            case Operation.Multiplication: 
                return "*";
            case Operation.Exponentiation:
                return "^";
            case Operation.Logarithm:
                return "log";
            case Operation.Trigonometric:
                return "trig";
            case Operation.Modulate:
                return "%";
            default:
                return "$";
        }
    }

    public class Algebraic {
        public static bool unlocked = true;
        public string name;
        public Func<Expression, Expression> Use;
        public Func<Expression, Expression> UseInverse;
        public Algebraic(string n, Func<Expression, Expression> u) {
            name = n;
            Use = u;
        }
        public Algebraic(string n, Func<Expression, Expression> u, Func<Expression, Expression> uI) {
            name = n;
            Use = u;
            UseInverse = uI;
        }
    }
    Algebraic DistributiveProperty = new Algebraic("Distributive Property", 
        (Expression e) => {
            Expression newE = new Expression();
            if (e.EN1.isExpression) {
                newE.EN1 = new Expression(Operation.Multiplication, ((Expression)e.EN1).EN1, e.EN2);
                newE.operation = Operation.Addition;
                newE.EN2 = new Expression(Operation.Multiplication, ((Expression)e.EN1).EN2, e.EN2);
            } else if (e.EN2.isExpression) {
                newE.EN1 = new Expression(Operation.Multiplication, ((Expression)e.EN2).EN1, e.EN1);
                newE.operation = Operation.Addition;
                newE.EN2 = new Expression(Operation.Multiplication, ((Expression)e.EN2).EN2, e.EN1);
            } else {
                newE.IsInvalidResult = true;
            }
            return newE;
        },

        (Expression e) => {
            Expression newE = new Expression();
            if (!e.EN1.Stringify().Equals(e.EN2.Stringify())) {
                newE.IsInvalidResult = true;
                return newE;
            }
            //TODO: Implement inverse D. P. logic
            return newE;
        }
    );

    Algebraic ComutativeProperty = new Algebraic("Commutative Property",
        (Expression e) => {
            Expression newE = new Expression();
            if (e.operation == Operation.Addition || e.operation == Operation.Multiplication) {
                newE.EN1 = e.EN2;
                newE.operation = e.operation;
                newE.EN2 = e.EN1;
            } else {
                newE.IsInvalidResult = true;
            }
            return newE;
        }
    );

    //NOTE: does not make it look pretty, only meant to be read and interpreted in Main
    public override string Stringify(/*string encoding*/) {
        string cumulator = "";
        StringifyRecurse(this, ref cumulator);
        return cumulator;
    }
    public void StringifyRecurse(Expression e, ref string previous) {
        previous += Expression.OperationToChar(e.operation, e.EN1, e.EN2, 0);
        
        if (!e.EN1.isExpression) {
            previous += ((NW)e.EN1).value;
        } else {
            previous += "(";
            StringifyRecurse((Expression)e.EN1, ref previous);
            previous += ")";
        }

        previous += ",";


        if (!e.EN2.isExpression) {
            previous += ((NW)e.EN2).value;
        } else {
            previous += "(";
            StringifyRecurse((Expression)e.EN2, ref previous);
            previous += ")";
        }

        
    }
}
