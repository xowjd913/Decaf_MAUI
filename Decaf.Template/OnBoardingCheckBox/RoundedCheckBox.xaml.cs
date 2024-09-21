using System.Diagnostics;

namespace Decaf.Template;

public partial class RoundedCheckBox : ContentView
{
	public static readonly BindableProperty IsCheckedProperty
		= BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(RoundedCheckBox), false, BindingMode.TwoWay);

	public bool IsChecked
	{
		get => (bool)GetValue(IsCheckedProperty);
		set => SetValue(IsCheckedProperty, value);
	}

	public RoundedCheckBox()
	{
		InitializeComponent();
	}
}
