amespace QuadEqTestsMauiApp;

using AlgebraicEquations;

public partial class SolvePage : ContentPage
{
	public SolvePage()
	{
		InitializeComponent();
        btnBack.Clicked += async (o, e) => await Navigation.PopAsync();
	}

    private async void Solve(object sender, EventArgs e)
    {
        float a, b, c;
        if (float.TryParse(A.Text, out a) &&
            float.TryParse(B.Text, out b) &&
            float.TryParse(C.Text, out c))
        {
            QuadEquation eq = new QuadEquation(a, b, c);
            List<double> x = eq.Solve();
            if (x.Count == 0)
            {
                X1.Text = "Нет корней !";
                X2.Text = "";
            }
            else if (x.Count == 1)
            {
                X1.Text = "X1 = " + x[0].ToString();
                X2.Text = "";
            }
            else
            {
                X1.Text = "X1 = " + x[0].ToString();
                X2.Text = "X2 = " + x[1].ToString();
            }
        }
        else
            await DisplayAlert("Ошибка", "Неправильный формат чисел !", "Ok");
    }

}