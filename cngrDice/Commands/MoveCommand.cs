using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cngrDice.Commands
{
    public class RelayCommandParameters : BaseCommand
    {
        private readonly Action<string> execute;

        public RelayCommandParameters(Action<string> execute)
        {
            this.execute = execute;
        }

        public override void Execute(object parameter)
        {
            execute.Invoke((string)parameter);
        }
    }
}
