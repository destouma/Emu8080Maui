namespace Emu8080Maui;

public partial class MainPage : ContentPage
{
	int count = 0;
	private readonly MainViewModel mainViewModel; 
	public MainPage()
	{
		InitializeComponent();
        BindingContext = mainViewModel = new MainViewModel();

    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		mainViewModel.ChangeLabel(count);

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


