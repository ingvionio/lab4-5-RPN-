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

        public abstract bool IsFunction { get; }

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

        public override bool IsFunction => false;

        public override Number Execute(params Number[] numbers)
        {
            var num1 = numbers[0];
            var num2 = numbers[1];

            return new Number { Value = num1.Value + num2.Value };
        }

        class Minus : Operation
        {
            public override string Name => "-";

            public override int Priority => 1;

            public override int ArgsCount => 2;

            public override bool IsFunction => false;

            public override Number Execute(params Number[] numbers)
            {
                var num1 = numbers[0];
                var num2 = numbers[1];

                return new Number { Value = num1.Value - num2.Value };
            }
        }

        class log : Operation
        {
            public override string Name => "log";

            public override int Priority => 3;

            public override int ArgsCount => 2;

            public override bool IsFunction => false;

            public override Number Execute(params Number[] numbers)
            {
                var num1 = numbers[0];
                var num2 = numbers[1];

                return new Number { Value = Math.Log(num1.Value , num2.Value) };
            }
        }
    }
}
