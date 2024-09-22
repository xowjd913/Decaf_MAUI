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
        public ICommand OnNextSurveyPageButtonClickCommand { get; private set; }
        public ICommand OnPrevSurveyPageButtonClickCommand { get; private set; }
        #endregion

        #region [ Properties ]
        public ObservableCollection<OnBoardingSurveyPage> SurveyPages { get; private set; }
        #endregion

        private int currentPage;
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(new(nameof(CurrentPage)));
            }
        }

        private string nextPageButtonContent;
        public string NextPageButtonContent
        {
            get => nextPageButtonContent;
            set
            {
                nextPageButtonContent = value;
                OnPropertyChanged(new(nameof(NextPageButtonContent)));
            }
        }

        private bool isVisibleBackButton;
        public bool IsVisibleBackButton
        {
            get => isVisibleBackButton;
            set
            {
                isVisibleBackButton = value;
                OnPropertyChanged(new(nameof(IsVisibleBackButton)));
            }
        }

        private bool isVisibleResetSurveyButton;
        public bool IsVisibleResetSurveyButton
        {
            get => isVisibleResetSurveyButton;
            set
            {
                isVisibleResetSurveyButton = value;
                OnPropertyChanged(new(nameof(IsVisibleResetSurveyButton)));
            }
        }

        private float circleProgressPercent;
        public float CircleProgressPercent
        {
            get => circleProgressPercent;
            set
            {
                circleProgressPercent = value;
                OnPropertyChanged(new(nameof(CircleProgressPercent)));
            }
        }

        public OnBoardingPageViewModel(
                INavigationService navigationService
            )
            : base(navigationService)
        {
            Debug.WriteLine($"[{nameof(OnBoardingPageViewModel)}] [L] : Initialize....");

            CreateOnBoardingSurveyPages();

            OnNextSurveyPageButtonClickCommand = new Command(OnNextSurveyPageButtonClick);
            OnPrevSurveyPageButtonClickCommand = new Command(OnPrevSurveyPageButtonClick);

            NextPageButtonContent = "다음";
        }


        #region [ Command sources ]
        private void OnNextSurveyPageButtonClick()
        {
            if (CurrentPage == SurveyPages.Count - 1)
                return;

            CurrentPage++;

            IsVisibleBackButton = CurrentPage != 0;
            IsVisibleResetSurveyButton = CurrentPage == SurveyPages.Count - 1;

            CircleProgressPercent = (float)CurrentPage / (SurveyPages.Count - 1);

            NextPageButtonContent = "다음";
        }
        private void OnPrevSurveyPageButtonClick()
        {
            if (CurrentPage == 0)
                return;

            CurrentPage--;

            IsVisibleBackButton = CurrentPage != 0;
            IsVisibleResetSurveyButton = CurrentPage == SurveyPages.Count - 1;

            CircleProgressPercent = (float)CurrentPage / (SurveyPages.Count - 1);

            NextPageButtonContent = "다음";
        }
        #endregion

        #region [ Private methods ]
        private void CreateOnBoardingSurveyPages()
        {
            void OnActiveIsChecked(string text)
            {
                var otherSurveys = SurveyPages[CurrentPage]
                    .Surveys
                    .Where(o => o.Text.Equals(text) == false)
                    .ToList();

                otherSurveys.ForEach(element =>
                {
                    element.IsChecked = false;
                });
            }

            SurveyPages = new ObservableCollection<OnBoardingSurveyPage>();

            SurveyPages.Add(new OnBoardingSurveyPage()
            {
                Title = "커피를 하루에 몇 잔\n드시나요?",
                Surveys = new ObservableCollection<OnBoardingSurvey>()
                {
                    new(OnActiveIsChecked)
                    {
                        Text = "3잔 이상",
                        IsChecked = false,
                    },
                    new(OnActiveIsChecked)
                    {
                        Text = "1자",
                        IsChecked = false,
                    },
                    new(OnActiveIsChecked)
                    {
                        Text = "아예 못 마심",
                        IsChecked = false,
                    },
                }
            });
            SurveyPages.Add(new OnBoardingSurveyPage()
            {
                Title = "카페인을 줄이려는\n이유가 뭡니까?",
                Surveys = new ObservableCollection<OnBoardingSurvey>()
                {
                    new(OnActiveIsChecked)
                    {
                        Text = "건강이 좋지 않아서",
                        IsChecked = false,
                    },
                    new(OnActiveIsChecked)
                    {
                        Text = "임신 중이라서",
                        IsChecked = false,
                    },
                    new(OnActiveIsChecked)
                    {
                        Text = "잠을 못 자서",
                        IsChecked = false,
                    },
                    new(OnActiveIsChecked)
                    {
                        Text = "청소년이라 걱정되서",
                        IsChecked = false,
                    },
                }
            });
            SurveyPages.Add(new OnBoardingSurveyPage()
            {
                Title = "차(tea)를\n좋아하십니까?",
                Surveys = new ObservableCollection<OnBoardingSurvey>()
                {
                    new(OnActiveIsChecked)
                    {
                        Text = "예",
                        IsChecked = false,
                    },
                    new(OnActiveIsChecked)
                    {
                        Text = "아니요",
                        IsChecked = false,
                    },
                }
            });
            SurveyPages.Add(null);
        }

        #endregion
    }
}

