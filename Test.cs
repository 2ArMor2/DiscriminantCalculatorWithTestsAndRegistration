namespace QuadEqTestsMauiApp.Model
{
    public class Test
    {
        public string DTStamp { get; set; }
        //public QuadEquation Eq { get; set; }
        //public User TestUser { get; set; }

        public string Email  { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public List<double> X { get; set; }
        public List<double> Res { get; set; }

        //public Test(String dtstamp, User testuser, QuadEquation eq, List<double> res)
        public Test(string dtstamp,  string email, double a, double b, double c,
            List<double> x, List<double> res)
        {
            DTStamp = dtstamp; // DateTime.Now.ToString();
            Email = email;
            A = a; B = b; C = c;
            X = x;
            //TestUser = testuser;
            Res = res;
        }

    }
}