using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Weatherer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string APIKey = "2c5e0ffd4d8a0dbbd84a79998ff57a8c"; // the secret
        private string fileName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fileName = "http://api.openweathermap.org/data/2.5/weather?q=Chicago&mode=xml&units=imperial&APPID=" + APIKey;
            WeatherData.Text = CurrentTemperature();
        }

        public string CurrentTemperature()
        {
            string description = "Cloudy";
            string temperatureString = "60";
            float temperature = 0;

            XDocument xdoc = XDocument.Load(fileName);
            var tempList = xdoc.Descendants()
                          .Where(x => x.Name == "temperature" || x.Name == "weather");

            foreach (XElement node in tempList)
            {
                if (node.Name == "temperature")
                { temperatureString = node.Attribute("value").Value; }
                if (float.TryParse(temperatureString, out float t))
                { temperature = t; }
                if (node.Name == "weather")
                { description = node.Attribute("value").Value; }
            }
            return $"Temperature: {temperature} \n {description}";
        }
    }
}