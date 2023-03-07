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
        public string shown;
        public double hideen;
        public double savedn;
        private void btnnumber_Clickend(object sender, RoutedEventArgs e)
        {
            ekran.Text = "";
            shown += " " + ((Button)sender).Content.ToString() + " ";
            savedn = hideen;
            hideen = 0;
            ekran.Text = shown;
            switch (((Button)sender).Content.ToString())
            {
                case "+":
                    Task.Run(() =>
                    {
                        resetEvent.Reset();
                        resetEvent.WaitOne();
                        Dispatcher.Invoke(() =>
                        {
                            hideen += savedn;
                            
                        });
                    });
                    break;
                case "-":
                    Task.Run(() =>
                    {
                        resetEvent.Reset();
                        resetEvent.WaitOne();
                        Dispatcher.Invoke(() =>
                        {
                            hideen = savedn - hideen;

                        });
                    });
                    break;
                case "*":
                    Task.Run(() =>
                    {
                        resetEvent.Reset();
                        resetEvent.WaitOne();
                        Dispatcher.Invoke(() =>
                        {
                            hideen *= savedn;

                        });
                    });
                    break;
                case "/":
                    Task.Run(() =>
                    {
                        resetEvent.Reset();
                        resetEvent.WaitOne();
                        Dispatcher.Invoke(() =>
                        {
                            hideen = savedn / hideen;

                        });
                    });
                    break;
            }
        }


        private void btnnumber_Click(object sender, RoutedEventArgs e)
        {
            ekran.Text = "";
            shown += ((Button)sender).Content.ToString();
            hideen += double.Parse(((Button)sender).Content.ToString());
            ekran.Text += shown;
            resetEvent.Set();
        }

        private void btnnumber_Clickended(object sender, RoutedEventArgs e)
        {
            ekran_Copy.Text = ekran.Text;
            ekran.Text = hideen.ToString();
            shown = hideen.ToString();
        }
    }
}
