namespace QuadEqTestsMauiApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        p = new EnterRegPage();
        p.Unloaded += P_Unloaded;
    }

    EnterRegPage p;

    public User CurUser { get; set; }
	private async void Solve(object? sender, EventArgs e)
	{
        await Navigation.PushAsync(new SolvePage());
	}

    private async void Enter(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(p);
    }

    private void P_Unloaded(object sender, EventArgs e)
    {
        CurUser = p.CurUser;
        if (CurUser != null)
            lblUser.Text = "Пользователь: " + CurUser.LastName;
        else
            lblUser.Text = "Пользователь: Гость";
    }

    private async void DoTest(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestPage(CurUser));
    }

    private async void History(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestListPage(CurUser));
    }

    private void Exit(object? sender, EventArgs e)
    {
        CurUser = null;
        lblUser.Text = "Пользователь: Гость";
    }

}
