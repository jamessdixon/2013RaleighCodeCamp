using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using CS = Tff.Ransom.CS;
using VB = Tff.Ransom.VB;
using FS = Tff.Ransom.FS;

namespace Tff.Ransom.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CSharpButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            CS.DollarProvider provider = new CS.DollarProvider();
            Int32 numberOfDollars = Int32.Parse(this.DollarsTextBox.Text);
            provider.GetDollars(numberOfDollars);
            stopWatch.Stop();

            this.CSharpResultsTextBox.Text = stopWatch.Elapsed.TotalSeconds.ToString() + " seconds";

        }

        private void RunVBButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            VB.DollarProvider provider = new VB.DollarProvider();
            Int32 numberOfDollars = Int32.Parse(this.DollarsTextBox.Text);
            provider.GetDollars(numberOfDollars);
            stopWatch.Stop();

            this.VBResultsTextBox.Text = stopWatch.Elapsed.TotalSeconds.ToString() + " seconds";

        }

        private void RunFSharpButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            FS.dollarProvider provider = new FS.dollarProvider();
            Int32 numberOfDollars = Int32.Parse(this.DollarsTextBox.Text);
            provider.GetDollars(numberOfDollars);
            stopWatch.Stop();

            this.FSharpResultsTextBox.Text = stopWatch.Elapsed.TotalSeconds.ToString() + " seconds";
        }
    }
}
