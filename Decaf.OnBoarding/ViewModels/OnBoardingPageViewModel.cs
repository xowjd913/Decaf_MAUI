using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Decaf.Base.MVVM;
using Decaf.OnBoarding.Models;

namespace Decaf.OnBoarding.ViewModels
{

    public class OnBoardingPageViewModel : ViewModelBase
    {

        #region [ Command ]
        public ICommand IsTappedToggleCommand { get; private set; }
        #endregion

        #region [ Properties ]
        public ObservableCollection<OnBoardingSurvey> OnBoardingSurveys { get; private set; }
        #endregion

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

            CreateOnBoardingSurveys();
        }


        #region [ Private methods ]
        private void CreateOnBoardingSurveys()
        {
            void OnActiveIsChecked(string text)
            {
                Debug.WriteLine($"[L] : {text} CHECKED !!!!");

                var otherSurveys = OnBoardingSurveys
                    .Where(o => o.Text.Equals(text) == false)
                    .ToList();

                otherSurveys.ForEach(element =>
                {
                    element.IsChecked = false;
                });
            }

            OnBoardingSurveys = new ObservableCollection<OnBoardingSurvey>()
            {
                new(OnActiveIsChecked)
                {
                    Text = "3잔 이상",
                    IsChecked = false,
                },
                new(OnActiveIsChecked)
                {
                    Text = "1잔",
                    IsChecked = false,
                },
                new(OnActiveIsChecked)
                {
                    Text = "아예 못 마심",
                    IsChecked = false,
                },
            };
        }
        #endregion
    }
}

