namespace QuadEqTestsMauiApp;

using AlgebraicEquations;
using QuadEqTestsMauiApp.Model;
using System.Text.Json;

public partial class TestListPage : ContentPage
{
	public TestListPage(User user)
	{
		InitializeComponent();
        btnBack.Clicked += async (o, e) => await Navigation.PopAsync();
        curUser = user;
        if (curUser != null)
        {
            User.Text = curUser.LastName;
            //Appearing += LoadTests;
            //TstLst = new List<string>() { "One", "Two" };//tests;
            LoadTests();
            BindingContext = this;
            //SetBinding(TestList, "TestList2");
        }
    }

    public List<Test> TstLst { get; set; }
    //public List<string> TstLst { get; set; }

    User curUser;

    //private void LoadTests(object sender, EventArgs e)
    private void LoadTests()
    {
        FileStream fs = new FileStream(FileSystem.AppDataDirectory + "\\tests.json", FileMode.OpenOrCreate);
        var options = new JsonSerializerOptions { WriteIndented = true };
        var tests = new List<Test>();
        if (fs.Length > 0)
            tests = JsonSerializer.Deserialize<List<Test>>(fs);
        fs.Close();
        TstLst = tests.FindAll(x => x.Email == curUser.Email);
        //BindingContext = this;
    }
}