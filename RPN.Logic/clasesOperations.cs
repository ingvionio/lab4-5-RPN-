using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic
{
    public abstract class Operation : Token
    {
        public abstract string Name { get; }
        public abstract int Priority { get; }

        public abstract int ArgsCount { get; }

        public abstract Number Execute(params Number[] numbers);

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    class Plus : Operation
    {
        public override string Name => "+";

        public override int Priority => 1;

        public override int ArgsCount => 2;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = num1.Value + num2.Value };
        }
    }

    class Minus : Operation
    {
        public override string Name => "-";

        public override int Priority => 1;

        public override int ArgsCount => 2;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = num1.Value - num2.Value };
        }
    }

    class Multiply : Operation
    {
        public override string Name => "*";

        public override int Priority => 1;

        public override int ArgsCount => 2;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = num1.Value * num2.Value };
        }
    }

    class Divide : Operation
    {
        public override string Name => "/";

        public override int Priority => 1;

        public override int ArgsCount => 2;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = num1.Value / num2.Value };
        }
    }

    class log : Operation
    {
        public override string Name => "log";

        public override int Priority => 3;

        public override int ArgsCount => 2;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = Math.Log(num1.Value, num2.Value) };
        }
    }

    class Power : Operation
    {
        public override string Name => "^";

        public override int Priority => 3;

        public override int ArgsCount => 2;


        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = Math.Pow(num1.Value, num2.Value) };
        }
    }

    class SquareRoot : Operation
    {
        public override string Name => "sqrt";

        public override int Priority => 3;

        public override int ArgsCount => 1;


        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];

            return new Number { Value = Math.Sqrt(num1.Value) };
        }
    }

    class Root : Operation
    {
        public override string Name => "rt";

        public override int Priority => 3;

        public override int ArgsCount => 2;


        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = Math.Pow(num2.Value, 1 / num2.Value) };
        }
    }

    abstract class Trigonometry : Operation
    {
        public override int Priority => 3;

        public override int ArgsCount => 1;

    }

    class Sine : Trigonometry
    {
        public override string Name => "sin";
        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];

            return new Number { Value = Math.Sin(num1.Value) };
        }
    }

    class Cosine : Trigonometry
    {
        public override string Name => "cos";
        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];

            return new Number { Value = Math.Cos(num1.Value) };
        }
    }

    class Tangent : Trigonometry
    {
        public override string Name => "tg";
        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];

            return new Number { Value = Math.Tan(num1.Value) };
        }
    }

    class Cotangent : Trigonometry
    {
        public override string Name => "ctg";
        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];

            return new Number { Value = Math.Cos(num1.Value) / Math.Sin(num1.Value) };
        }
    }
}

