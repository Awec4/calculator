using Calculator.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region fields
        private int maxDisplayLength = 20;
        private ICommand _numPressedCommand;
        private ICommand _slashPressedCommand;
        private ICommand _calcCommand;
        private string display;
        public event PropertyChangedEventHandler PropertyChanged;

        // TODO: Implement Dark Mode, conditional styling is beyond the current scope of this project
        public bool DarkMode
        {
            get; set;
        }

        public string Display
        {
            get => display;
            set {
                if (value.Length <= maxDisplayLength)
                {
                    display = value;
                    OnPropertyChanged(nameof(Display));
                }
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            DarkMode = false;
            Display = "0";
        }

        #region Commands
        public ICommand NumPressedCommand
        {
            get
            {
                return _numPressedCommand ?? (_numPressedCommand = new CommandHandler<string>((param) => ExecuteKeyPressedCommand(param), CanExecuteKeyPressedCommand));
            }
        }

        public ICommand SlashPressedCommand
        {
            get
            {
                return _slashPressedCommand ?? (_slashPressedCommand = new CommandHandler<string>((param) => ExecuteSlashPressedCommand(param), CanExecuteSlashPressedCommand));
            }
        }

        public ICommand CalcCommand
        {
            get
            {
                return _calcCommand ?? (_calcCommand = new CommandHandler<string>((param) => ExecuteCalcCommand(param), CanExecuteCalcCommand));
            }
        }
        #endregion

        private bool CanExecuteKeyPressedCommand()
        {
            return Display.Length < maxDisplayLength;
        }

        private void ExecuteKeyPressedCommand(string param)
        {
            if (Display == "0")
            {
                Display = param;
            }
            else
            {
                Display = Display + param;
            }
        }

        private bool CanExecuteSlashPressedCommand()
        {
            // Can't already contain a slash and can't be the first or last character
            return !Display.Contains("/") && Display.Length > 0 && Display.Length < maxDisplayLength - 1;
        }

        private void ExecuteSlashPressedCommand(string param)
        {
            Display = Display + "/";
        }

        private bool CanExecuteCalcCommand()
        {
            return !Display.EndsWith("/");
        }

        private void ExecuteCalcCommand(string param)
        {
            Fraction fr = Fraction.TryParse(Display);
            Display = Fraction.Simplify(fr).ToString();
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
