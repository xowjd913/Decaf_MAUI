using System.Diagnostics;
using System.Windows.Input;

namespace Decaf.Template;

public partial class OnBoardingCheckBoxControl : ContentView
{
	public static readonly BindableProperty TextProperty
		= BindableProperty.Create(nameof(Text), typeof(string), typeof(OnBoardingCheckBoxControl), default(string));

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public static readonly BindableProperty IsCheckedProperty
		= BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(OnBoardingCheckBoxControl), default(bool));

	public bool IsChecked
	{
		get => (bool)GetValue(IsCheckedProperty);
		set => SetValue(IsCheckedProperty, value);
	}

	public static readonly BindableProperty OnValueChangedCommandProperty
		= BindableProperty.Create(nameof(OnValueChangedCommand), typeof(ICommand), typeof(OnBoardingCheckBoxControl), null);

	public ICommand OnValueChangedCommand
	{
		get => (ICommand)GetValue(OnValueChangedCommandProperty);
		set
		{
			SetValue(OnValueChangedCommandProperty, value);
		}
	}

	public OnBoardingCheckBoxControl()
		=> InitializeComponent();


    private void OnCheckBoxTapped(object sender, TappedEventArgs e)
    {
		IsChecked = !IsChecked;
		if (OnValueChangedCommand != null)
            OnValueChangedCommand.Execute(null);
    }
}
