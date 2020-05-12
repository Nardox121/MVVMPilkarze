using System;
using System.Windows.Input;

namespace MVVMPilkarze.ViewModel.Base
{
    class RelayCommand : ICommand
    {

        readonly Action<object> _execute;

        readonly Predicate<object> _canExecute;


        #region konstruktor

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else
                _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region Składowe interfejsu ICommand
        //składowe interfesu ICommand w odniesienu do wstrzykniętych z zewnątrz metod
        //referencje do których przechowujemy w prywatnych polach

        //metoda określająca czy polecenie może zostać wykonane
        public bool CanExecute(object parameter)
        {
            //zakładmy, że brak tej metody oznacza domyślnie wartośc true
            return _canExecute == null ? true : _canExecute(parameter);
        }

        //zdarzenie informujące o możliwości wykonania polecenie
        //dodatkowo metody dołączone do zdarzenia rejestrujemy w menadżerze poleceń co
        //gwarantuje, że brak możliwości wykonania polecenia będzie automatycznie
        //wyłączać włąsność isEnable kontrolki, do któej to  polecenie zostanie podpięte
        public event EventHandler CanExecuteChanged
        {
            //wykonywane wówczas gdy jakąś metodę (value) dołączamy do zdarzenia
            add
            {
                if (_canExecute != null) CommandManager.RequerySuggested += value;
            }
            //wykonywane wówczas, gdy jakąś metodę odpinamy od zdarzenia
            remove
            {
                if (_canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        //metoda wykonywana przez polecenie
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion
    }
}
