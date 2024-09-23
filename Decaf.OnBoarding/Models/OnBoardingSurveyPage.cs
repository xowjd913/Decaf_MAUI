using System;
using System.Collections.ObjectModel;

namespace Decaf.OnBoarding.Models
{
	public class OnBoardingSurveyPage : BindableBase
	{
		private bool isFinalPage;
		public bool IsFinalPage
		{
			get => isFinalPage;
			set
			{
				isFinalPage = value;
				OnPropertyChanged(new(nameof(IsFinalPage)));
			}
		}

		private string title;
		public string Title
		{
			get => title;
			set
			{
				title = value;
				OnPropertyChanged(new(nameof(Title)));
			}
		}

		private ObservableCollection<OnBoardingSurvey> surveys;
		public ObservableCollection<OnBoardingSurvey> Surveys
		{
			get => surveys;
			set
			{
				surveys = value;
				OnPropertyChanged(new(nameof(Surveys)));
			}
		}


		public OnBoardingSurveyPage(bool isFinalPage)
		{
			IsFinalPage = isFinalPage;
		}
	}
}

