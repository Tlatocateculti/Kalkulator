using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalkulator
{

    public partial class MainWindow : Window
    {
        private ManualResetEvent resetEvent = new ManualResetEvent(false);
        private Task currentTask;
        public string shown;
        public decimal hideen;
        public string nso;
        public decimal savedn;
        bool equal = false;
        bool not = false;
        string zapis = "";
        public void calculate()
        {
            if (equal)
            {
                ekran_Copy.Text = ekran.Text;
                ekran.Text = hideen.ToString();
                shown = hideen.ToString();
               
            }
            nso = hideen.ToString();
            currentTask = null;
        }
        private void btnnumber_Click(object sender, RoutedEventArgs e)
        {
            ekran.Text = "";
            shown += ((Button)sender).Content.ToString();
            nso += ((Button)sender).Content.ToString();
            hideen = decimal.Parse(nso);
            ekran.Text += shown;
        }

        private void clickonoded(object sender, MouseButtonEventArgs e)
        {
            ekran.Text = "";
            shown += " " + ((Button)sender).Content.ToString() + " ";
            if (savedn != 0 && hideen != 0)
            {
                switch (zapis)
                {
                    case "+":
                        hideen += savedn;
                        break;
                    case "-":
                        hideen = savedn - hideen;
                        break;
                    case "*":
                        hideen *= savedn;
                        break;
                    case "/":
                        hideen = savedn / hideen;
                        break;
                }
            }
            savedn = hideen;
            hideen = 0;
            nso = "";
            ekran.Text = shown;
            zapis = ((Button)sender).Content.ToString();
            if (currentTask != null) return;
            currentTask = Task.Run(() =>
            {
                resetEvent.Reset();
                resetEvent.WaitOne();
                Dispatcher.Invoke(() =>
                {
                    switch (zapis)
                    {
                        case "+":
                            hideen += savedn;
                            savedn = 0;
                            break;
                        case "-":
                            hideen = savedn - hideen;
                            savedn = 0;
                            break;
                        case "*":
                            hideen *= savedn;
                            savedn = 0;
                            break;
                        case "/":
                            hideen = savedn / hideen;
                            savedn = 0;
                            break;
                    }
                    calculate();
                });
            });
        }
        private void btnnumber_Clickended(object sender, RoutedEventArgs e)
        {
            equal = true;
            resetEvent.Set();
        }
    }
}
