using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net;

namespace meteo
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public string a;
        async void Locatonpastnow()
        {
            try
            {
                ////var requst = new GeolocationRequest(GeolocationAccuracy.Medium);
                //var location = await Geolocation.GetLastKnownLocationAsync();
                var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                { a = $"Latitude:{location.Latitude},Longitude:{location.Longitude}, Altitude: { location.Altitude} "; }
            }
            catch (FeatureNotSupportedException fnsEx)
            { a = "Не поддерживается этим телефоном"; }
            catch (FeatureNotEnabledException fneEx)
            { a = "Не включено на телефоне"; }
            catch (PermissionException pEx)
            { a = "Нет  разрешения"; }
            catch (Exception Ex)
            { a = "Не возможно получить локацию"; }

        }
        public double x;
        public double y;
        async void Locatonpastnow2()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    x = location.Latitude;
                    y = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            { x = 0; }
            catch (FeatureNotEnabledException fneEx)
            { x = 0; }
            catch (PermissionException pEx)
            { x = 0; }
            catch (Exception Ex)
            { x = 0; }

        }
        

        public string Metod1()
        { Locatonpastnow2();
            string line = "";
            using (System.Net.WebClient wb = new WebClient())
                line = wb.DownloadString($"https://api.openweathermap.org/data/2.5/onecall?lat={x}&lon={y}&exclude=сurrent,minutely,hourly,alerts&appid=e882e66a1aea8064863f7d79b97e0798&units=metric&lang=ru");
            return line;
        }
        private void OnButtonClicked1(object sender, System.EventArgs e)
        {
            Locatonpastnow();
            Button button1 = (Button)sender;

            button1.Text = a;
            button1.BackgroundColor = Color.White;
            button1.TextColor = Color.Black;

        }
        //на сегодня
        private void OnButtonClicked2(object sender, System.EventArgs e)
        {
            Match mat = System.Text.RegularExpressions.Regex.Match(Metod1(), @"daily(.*?)day.\:(\-?\d*)(\.\d*)\,.min.\:(\-?\d*)(\.\d*)\,.max.\:(\-?\d*)(\.\d*)\,.night.\:(\-?\d*)(\.\d*)\,.eve.\:(\-?\d*)(\.\d*)\,.morn.\:(\-?\d*)(\.\d*)\}(.*?)pressure.\:(\d*.\d*)\,.humidity.\:(\d*)\,(.*?)wind_speed.\:(\d*)(\.\d*)\,.wind_deg.\:(\d*)(.*?)");
            button2.Text =
                 " утром " +  mat.Groups[12].Value
                 + " днем " +  mat.Groups[2].Value
                + " вечером " +  mat.Groups[10].Value
                + " ночюю " +  mat.Groups[8].Value
                + " min " +  mat.Groups[4].Value
                + " max " +  mat.Groups[6].Value
                + " давление " +  mat.Groups[15].Value
                + " влажность " +  mat.Groups[16].Value + " % "
                + " скорость ветра " +  mat.Groups[18].Value + " м/с"
                + " напровление ветра " +mat.Groups[20].Value;
            button2.BackgroundColor = Color.White;
            button2.TextColor = Color.Black;

        }
        //на сегодня и завтра
        private void OnButtonClicked3(object sender, EventArgs e)
        {
            string reg = @"day.\:(\-?\d*)(\.\d*)\,.min.\:(\-?\d*)(\.\d*)\,.max.\:(\-?\d*)(\.\d*)\,.night.\:(\-?\d*)(\.\d*)\,.eve.\:(\-?\d*)(\.\d*)\,.morn.\:(\-?\d*)(\.\d*)\}(.*?)pressure.\:(\d*.\d*)\,.humidity.\:(\d*)\,(.*?)wind_speed.\:(\d*)(\.\d*)\,.wind_deg.\:(\d*)(.*?)";
            MatchCollection mat1 = Regex.Matches(Metod1(), reg);
            string s = "";
            string z = "";
            for (int i = 0; i < 2; i++)
            {
                s = s + " утром " +  mat1[i].Groups[11].Value
                + " днем " +  mat1[i].Groups[1].Value
                + " вечером " + mat1[i].Groups[9].Value
                + " ночюю " +  mat1[i].Groups[7].Value
                + " min " +  mat1[i].Groups[3].Value
                + " max " +  mat1[i].Groups[5].Value
                + " давление " +  mat1[i].Groups[14].Value
                + " влажность " +  mat1[i].Groups[15].Value + "% "
                + " скорость ветра " +  mat1[i].Groups[17].Value + " м/с"
                + " напровление ветра " +  mat1[i].Groups[19].Value +  "\r\n";
                z = "на сегодня" + "\r\n" + s;
                button3.Text = z;
                button3.BackgroundColor = Color.White;
                button3.TextColor = Color.Black;

            }

        }
        // на 7 дней
        private void OnButtonClicked4(object sender, EventArgs e)
        {
            string reg = @"day.\:(\-?\d*)(\.\d*)\,.min.\:(\-?\d*)(\.\d*)\,.max.\:(\-?\d*)(\.\d*)\,.night.\:(\-?\d*)(\.\d*)\,.eve.\:(\-?\d*)(\.\d*)\,.morn.\:(\-?\d*)(\.\d*)\}(.*?)pressure.\:(\d*.\d*)\,.humidity.\:(\d*)\,(.*?)wind_speed.\:(\d*)(\.\d*)\,.wind_deg.\:(\d*)(.*?)";
            MatchCollection mat = Regex.Matches(Metod1(), reg);

            string b = " ";
            string c = " ";
            string d = " ";
            string h = " ";
            string f = " ";
            string g = " ";
            string k = " ";
            foreach (Match match in mat)
            {     
                b = b +  match.Groups[11].Value + "  ";
                c = c +  match.Groups[1].Value + " ";
                d = d +  match.Groups[9].Value + " ";
                h = h +  match.Groups[7].Value + " ";
                f = f +  match.Groups[17].Value + " м/с ";
                g = g +  match.Groups[15].Value + " % ";
                k = k +  match.Groups[14].Value + "  ";

            }
            button4.Text = "утром " + b + "\n" + " днем " + c + "\n" + " вечером " + d + "\n" + " ночью " + h +"\n" + "скорость ветера " + f + "\n" +  " влажность " + g + "\n" + " давление " + k;
            button4.BackgroundColor = Color.White;
            button4.TextColor = Color.Black;
        }
    }
}
