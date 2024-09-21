using System;
using System.Diagnostics;
using System.Windows.Input;
using Decaf.Base.MVVM;

namespace Decaf.OnBoarding.ViewModels
{
    public class OnBoardingPageViewModel : ViewModelBase
    {
        public ICommand IsTappedToggleCommand { get; private set; }

        public OnBoardingPageViewModel(
                INavigationService navigationService
            )
            : base(navigationService)
        {
            Debug.WriteLine($"[{nameof(OnBoardingPageViewModel)}] [L] : Initialize....");

            IsTappedToggleCommand = new Command(() =>
            {
                Debug.WriteLine($"[{nameof(OnBoardingPageViewModel)}] [L] : Tapped....");
                
            });
        }
    }
}

