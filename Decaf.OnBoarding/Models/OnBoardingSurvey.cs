using System;
using System.ComponentModel;

namespace Decaf.OnBoarding.Models
{
	public class OnBoardingSurvey : BindableBase
	{
		private bool isChecked;
		public bool IsChecked
		{
			get => isChecked;
			set
			{
				isChecked = value;

				if (value)
					onActiveIsChecked?.Invoke(Text);

				OnPropertyChanged(new(nameof(IsChecked)));
			}
		}

		private string text;
		public string Text
		{
			get => text;
			set
			{
				text = value;
				OnPropertyChanged(new(nameof(Text)));
			}
		}

		private Action<string> onActiveIsChecked;

		public OnBoardingSurvey(Action<string> onActiveIsChecked)
		{
			this.onActiveIsChecked = onActiveIsChecked;
		}
	}
}

