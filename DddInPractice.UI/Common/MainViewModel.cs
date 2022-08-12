﻿using DddInPractice.Logic.Atm;
using DddInPractice.Logic.SnackMachines;
using DddInPractice.UI.Atms;
using DddInPractice.UI.SnackMachines;

namespace DddInPractice.UI.Common;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        //SnackMachine snackMachine = new SnackMachineRepository().GetById(1);
        //var viewModel = new SnackMachineViewModel(snackMachine);
        //_dialogService.ShowDialog(viewModel);

        Atm atm = new AtmRepository().GetById(1);
        var viewModel = new AtmViewModel(atm);
        _dialogService.ShowDialog(viewModel);
    }
}
