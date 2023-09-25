namespace QuadEqTestsMauiApp;

public partial class TestPage : ContentPage
{
	public TestPage(User user)
	{
		InitializeComponent();
        btnBack.Clicked += async (o, e) => await Navigation.PopAsync();
        Loaded += TestPage_Loaded;
        curUser = user;
	}

    User curUser;

    public void TestPage_Loaded(object sender, EventArgs e)
    {
        GetABC();
    }

    private void GetABC()
    {
        Random rand = new Random();
        var a = rand.NextDouble();
        var b = rand.NextDouble() * 5;
        var c = rand.NextDouble();
        A.Text = Math.Round(a, 2).ToString();
        B.Text = Math.Round(b, 2).ToString();
        C.Text = Math.Round(c, 2).ToString();
    }

    private async void Test(object sender, EventArgs e)
    {
        float a, b, c;
        if (float.TryParse(A.Text, out a) &&
            float.TryParse(B.Text, out b) &&
            float.TryParse(C.Text, out c))
        {
            try
            {
                QuadEquation eq = new QuadEquation(a, b, c);
                List<double> x = eq.Solve();
                if (x.Count == 0)
                {
                    X1_.Text = "Нет корней !";
                    X2_.Text = "";
                    var cond1 = X1.Text == null || X1.Text == "";
                    var cond2 = X2.Text == null || X2.Text == "";
                    if (!(cond1 && cond2))
                        await DisplayAlert("Ошибка", "Ответ не верен!", "Ok");
                    else
                        await DisplayAlert("", "Верно !", "Ok");
                }
                else if (x.Count == 1)
                {
                    X1_.Text = "X1 = " + x[0].ToString();
                    X2_.Text = "";
                    double x1 = Double.Parse(X1.Text);
                    var cond1 = Math.Round(x1, 2) != Math.Round(x[0], 2);
                    var cond2 = X2.Text == null || X2.Text == "";
                    if (cond1 || !cond2)
                        await DisplayAlert("Ошибка", "Ответ не верен!", "Ok");
                    else
                        await DisplayAlert("", "Верно !", "Ok");
                }
                else
                {
                    X1_.Text = "X1 = " + x[0].ToString();
                    X2_.Text = "X2 = " + x[1].ToString();
                    double x1 = Double.Parse(X1.Text);
                    double x2 = Double.Parse(X2.Text);
                    if (Math.Round(x1, 2) != Math.Round(x[0], 2) ||
                        Math.Round(x2, 2) != Math.Round(x[1], 2))
                        await DisplayAlert("Ошибка", "Ответ не верен!", "Ok");
                    else
                        await DisplayAlert("", "Верно !", "Ok");
                }
                if (curUser != null) 
                {
                    double x1;
                    List < double> res = new List<double>();
                    if (Double.TryParse(X1.Text, out x1))
                        res.Add(x1);
                    if (Double.TryParse(X2.Text, out x1))
                        res.Add(x1);
                    SaveTest(eq, res);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Неправильный формат чисел !", "Ok");
            }
        }
        else
            await DisplayAlert("Ошибка", "Неправильный формат чисел !", "Ok");
    }

    private async void SaveTest(QuadEquation eq, List<double> res)
    {
        FileStream fs = new FileStream(FileSystem.AppDataDirectory + "\\tests.json", FileMode.OpenOrCreate);
        var options = new JsonSerializerOptions { WriteIndented = true };
        var tests = new List<Test>();
        if (fs.Length > 0)
            tests = await JsonSerializer.DeserializeAsync<List<Test>>(fs);
        fs.Close();
        fs = new FileStream(FileSystem.AppDataDirectory + "\\tests.json", FileMode.Truncate);
        Test test = new Test(DateTime.Now.ToString(), curUser.Email, 
            eq.A, eq.B, eq.C, eq.Solve(), res);
        tests.Add(test);
        await JsonSerializer.SerializeAsync<List<Test>>(fs, tests);
        fs.Close();
        await DisplayAlert("", "Тест пользователя " + curUser.LastName + " сохранен !", "Ok");
    }

}