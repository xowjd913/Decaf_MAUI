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
        public ICommand OnRestartSurveyButtonClickCommand { get; private set; }
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

        private bool isVisibleCircleProgress;
        public bool IsVisibleCircleProgress
        {
            get => isVisibleCircleProgress;
            set
            {
                isVisibleCircleProgress = value;
                OnPropertyChanged(new(nameof(IsVisibleCircleProgress)));
            }
        }

        private bool isVisibleFinalPageCheckImage;
        public bool IsVisibleFinalPageCheckImage
        {
            get => isVisibleFinalPageCheckImage;
            set
            {
                isVisibleFinalPageCheckImage = value;
                OnPropertyChanged(new(nameof(IsVisibleFinalPageCheckImage)));
            }
        }

        private string lastSurveyPageCaffeineContent;
        public string LastSurveyPageCaffeineContent
        {
            get => lastSurveyPageCaffeineContent;
            set
            {
                lastSurveyPageCaffeineContent = value;
                OnPropertyChanged(new(nameof(LastSurveyPageCaffeineContent)));
            }
        }

        public OnBoardingPageViewModel(
                INavigationService navigationService
            )
            : base(navigationService)
        {
            Debug.WriteLine($"[{nameof(OnBoardingPageViewModel)}] [L] : Initialize....");

            IsVisibleCircleProgress = true;

            CreateOnBoardingSurveyPages();

            OnNextSurveyPageButtonClickCommand = new Command(OnNextSurveyPageButtonClick);
            OnPrevSurveyPageButtonClickCommand = new Command(OnPrevSurveyPageButtonClick);
            OnRestartSurveyButtonClickCommand = new Command(OnRestartSurveyButtonClick);

            NextPageButtonContent = "다음";


        }


        #region [ Command sources ]
        private void OnRestartSurveyButtonClick()
        {
            foreach (var surveyPage in SurveyPages)
            {
                if (surveyPage.Surveys != null)
                {
                    foreach (var surveyContent in surveyPage.Surveys)
                    {
                        if (surveyContent)
                            surveyContent.IsChecked = false;
                    }
                }
            }

            CurrentPage = 0;
            IsVisibleBackButton = false;
            IsVisibleResetSurveyButton = false;
            IsVisibleFinalPageCheckImage = false;

            IsVisibleCircleProgress = true;

            CircleProgressPercent = 0;

            NextPageButtonContent = "다음";
        }
        private void OnNextSurveyPageButtonClick()
        {
            if (CurrentPage == SurveyPages.Count - 1)
                return;

            CurrentPage++;

            var isLastPage = CurrentPage == SurveyPages.Count - 1;

            IsVisibleResetSurveyButton = isLastPage;
            IsVisibleFinalPageCheckImage = isLastPage;
            IsVisibleCircleProgress = !isLastPage;
            IsVisibleBackButton = CurrentPage != 0;

            CircleProgressPercent = (float)CurrentPage / (SurveyPages.Count - 1);

            NextPageButtonContent = isLastPage ? "완료"
                                               : "다음";

            //TEST Caffeine..
            LastSurveyPageCaffeineContent = "200mg";
        }
        private void OnPrevSurveyPageButtonClick()
        {
            if (CurrentPage == 0)
                return;

            CurrentPage--;

            var isLastPage = CurrentPage == SurveyPages.Count - 1;

            IsVisibleResetSurveyButton = isLastPage;
            IsVisibleFinalPageCheckImage = isLastPage;
            IsVisibleCircleProgress = !isLastPage;
            IsVisibleBackButton = CurrentPage != 0;

            CircleProgressPercent = (float)CurrentPage / (SurveyPages.Count - 1);

            NextPageButtonContent = isLastPage ? "완료"
                                               : "다음";
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

            SurveyPages.Add(new OnBoardingSurveyPage(false)
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
            SurveyPages.Add(new OnBoardingSurveyPage(false)
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
            SurveyPages.Add(new OnBoardingSurveyPage(false)
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
            SurveyPages.Add(new OnBoardingSurveyPage(true));
        }

        #endregion
    }
}

