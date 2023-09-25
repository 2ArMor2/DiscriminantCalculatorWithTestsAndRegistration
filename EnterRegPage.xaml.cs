namespace QuadEqTestsMauiApp;

public partial class EnterRegPage : ContentPage
{
	public EnterRegPage()
	{
		InitializeComponent();
        btnBack.Clicked += async (o, e) => await Navigation.PopAsync();
        CurUser = null;
    }

    public User CurUser { get; set; }

    private async void Register(object sender, EventArgs e)
    {
        FileStream fs = new FileStream(FileSystem.AppDataDirectory + "\\users.json", FileMode.OpenOrCreate);
        var users = new List<User>();
        var options = new JsonSerializerOptions { WriteIndented = true };
        if (fs.Length > 0)
            users = await JsonSerializer.DeserializeAsync<List<User>>(fs);
        fs.Close();
        fs = new FileStream(FileSystem.AppDataDirectory + "\\users.json", FileMode.Truncate);
        User user = new User(Name.Text, Lastname.Text, Email.Text);
        users.Add(user);
        await JsonSerializer.SerializeAsync<List<User>>(fs, users);
        CurUser = user;
        fs.Close();
        await DisplayAlert("", "Пользователь " + CurUser.LastName + " зарегистрирован !", "Ok");
        await Navigation.PopAsync();
    }

    private async void Enter(object sender, EventArgs e)
    {
        using (FileStream fs = new FileStream(FileSystem.AppDataDirectory + "\\users.json", FileMode.OpenOrCreate))
        {
            var users = new List<User>();
            var options = new JsonSerializerOptions { WriteIndented = true };
            if (fs.Length > 0)
            {
                users = await JsonSerializer.DeserializeAsync<List<User>>(fs);
                var user = users.Find(x => x.Email == Email.Text);
                if (user != null)
                {
                    CurUser = user;
                    await DisplayAlert("", "Пользователь " + CurUser.LastName +  " найден !", "Ok");
                    await Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Ошибка", "Пользователь с таким e-mail не найден !", "Ok");
            }
            fs.Close();
        }
    }
}