using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernBoxes.Tool
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> m_execute;
        private readonly Predicate<object> m_canExectue;

        public RelayCommand(Action<object> m_execute)
        {
            this.m_execute = m_execute;
        }

        public RelayCommand(Action<object> m_execute, Predicate<object> m_canexectue)
        {
            this.m_canExectue = m_canexectue;
            this.m_execute = m_execute;
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (m_canExectue == null)
            {
                return true;
            }
            return m_canExectue(parameter);
        }

        public void Execute(object parameter)
        {
            this.m_execute(parameter);
        }

    }
}
