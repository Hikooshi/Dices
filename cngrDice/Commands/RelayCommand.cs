using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cngrDice.Commands
{
    public class RelayCommand : BaseCommand
    {
        private readonly Action execute;

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public override void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
