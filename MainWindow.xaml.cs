using System.Windows;
using AntShares.Wallets;
using System.Threading;
using System.Windows.Threading;
using System;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace AddressGenerator
{
    public partial class MainWindow : Window
    {
        bool pause = true;
        bool requireContains = false;
        bool requireStartWith = false;
        bool requireEndWith = false;
        bool requireLength = false;
        bool requireUppercase = false;
        int goodLength;
        int uppercase;
        string[] startWith;
        string[] contains;
        string[] endWith;
        ObservableCollection<GoodAddress> goodAddresses = new ObservableCollection<GoodAddress>();
        Thread[] threads = new Thread[8];
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textProcessorCount.Text = Environment.ProcessorCount.ToString();
            dataGrid1.ItemsSource = goodAddresses;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var count = 0;
            if (pause)
            {
                pause = false;
                for (int i = 0; i < Math.Min(Environment.ProcessorCount - 1, 7); i++)
                {
                    threads[i] = new Thread(Run);
                    threads[i].Start();
                    count++;
                }
            }
            else
            {
                pause = true;
            }

            textThreadCount.Text = count.ToString();
            (sender as Button).Content = pause ? "Start" : "Stop";
        }

        private void Button_Startwith_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireStartWith = requireStartWith ? false : true;
                startWith = File.ReadAllLines("StartWith.txt");
                (sender as Button).Content = requireStartWith ? "StartWith enabled" : "StartWith disabled";
            }

        }

        private void Button_Endwith_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireEndWith = requireEndWith ? false : true;
                endWith = File.ReadAllLines("EndWith.txt");
                (sender as Button).Content = requireEndWith ? "EndWith enabled" : "StartWith disabled";
            }

        }

        private void Button_Contains_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireContains = requireContains ? false : true;
                contains = File.ReadAllLines("Contains.txt");
                (sender as Button).Content = requireContains ? "Contains enabled" : "Contains disabled";
            }

        }

        private void Button_Length_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireLength = requireLength ? false : true;
                goodLength = Convert.ToInt32(File.ReadAllText("GoodLength.txt"));
                (sender as Button).Content = requireLength ? "Length enabled" : "Length disabled";
            }

        }

        private void Button_Uppercase_Click(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireUppercase = requireUppercase ? false : true;
                uppercase = Convert.ToInt32(File.ReadAllText("Uppercase.txt"));
                (sender as Button).Content = requireUppercase ? "Uppercase enabled" : "Uppercase disabled";
            }

        }

        public void Run()
        {
            byte[] privateKey = new byte[32];
            while (!pause)
            {
                using (CngKey key = CngKey.Create(CngAlgorithm.ECDsaP256, null, new CngKeyCreationParameters { ExportPolicy = CngExportPolicies.AllowPlaintextArchiving }))
                {
                    privateKey = key.Export(CngKeyBlobFormat.EccPrivateBlob);
                }
                Generate(privateKey);
            }
        }

        public void Generate(byte[] privateKey)
        {
            var account = new Account(privateKey);
            var contract = Contract.CreateSignatureContract(account.PublicKey);
            var address = contract.Address;
            var length = contract.Address.Sum(p => p.Length());
            if (
                (requireStartWith && startWith.Any(p => address.StartsWith(p))) || 
                (requireContains && contains.Any(p => address.Contains(p))) ||
                (requireEndWith && endWith.Any(p => address.EndsWith(p))) ||
                (requireLength && length < goodLength) ||
                (requireUppercase && contract.Address.Count(p => p >= 'A' && p <= 'Z') < uppercase)
                )
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    goodAddresses.Add(new GoodAddress()
                    {
                        Address = contract.Address,
                        Privatekey = account.Export()
                    });
                });
            }
           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
