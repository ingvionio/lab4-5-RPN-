using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic
{
    class TokenCreate
    {
        private static List<Operation> _availableOperations;

        private TokenCreate()
        {
            _availableOperations = null;
        }

        public static Operation CreateOperation(string name)
        {
            Operation operation = FindAvilableByName(name);

            if (operation == null)
            {
                throw new ArgumentException($"Unknown operation {name}");
            }

            else
            {
                return operation;
            }
        }
        private static Operation FindAvilableByName(string name)
        {
            if (_availableOperations == null)
            {
                var parent = typeof(Operation);
                var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var types = allAssemblies.SelectMany(assembly => assembly.GetTypes());
                var inheritingTypes = types.Where(t => parent.IsAssignableFrom(t) && !t.IsAbstract).ToList();

                _availableOperations = inheritingTypes.Select(type => (Operation)Activator.CreateInstance(type)).ToList();
            }

            return _availableOperations.FirstOrDefault(op => op.Name.Equals(name));

        }
    }
}
