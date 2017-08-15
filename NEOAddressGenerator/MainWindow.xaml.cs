using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System;
using System.Linq;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
//using Neo.Wallets;
using AntShares.Wallets;


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
        bool requireAll = false;
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
            textWorkingStatus.Text = pause ? "Paused": "Working...";
            textThreadCount.Text = count.ToString();
            (sender as Button).Content = pause ? "Start" : "Stop";
        }

        public void Run()
        {
            byte[] privateKey = new byte[32];
            while (!pause)
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(privateKey);
                }
                Account key = new Account(privateKey);
                // KeyPair key = new KeyPair(privateKey);
                // Array.Clear(privateKey, 0, privateKey.Length);
                Generate(key);
            }
        }

        public void Generate(Account key)
        {
            var contract = Contract.CreateSignatureContract(key.PublicKey);
            var address = contract.Address;
            var length = contract.Address.Sum(p => p.Length());
            if (!requireAll)
            {
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
                            Privatekey = key.Export()
                        });
                    });
                }
            }
            if (requireAll)
            {
                if (
                    ((requireStartWith && startWith.Any(p => address.StartsWith(p))) || !requireStartWith) &&
                    ((requireContains && contains.Any(p => address.Contains(p))) || !requireContains) &&
                    ((requireEndWith && endWith.Any(p => address.EndsWith(p))) || !requireEndWith) &&
                    ((requireLength && length < goodLength) || !requireLength) &&
                    ((requireUppercase && contract.Address.Count(p => p >= 'A' && p <= 'Z') < uppercase) || !requireUppercase)
                    )
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        goodAddresses.Add(new GoodAddress()
                        {
                            Address = contract.Address,
                            Privatekey = key.Export()
                        });
                    });
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        private void StartWith_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireStartWith = requireStartWith ? false : true;
                startWith = File.ReadAllLines("StartWith.txt");
            }
        }

        private void EndWith_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireEndWith = requireEndWith ? false : true;
                endWith = File.ReadAllLines("EndWith.txt");
            }
        }

        private void Contains_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireContains = requireContains ? false : true;
                contains = File.ReadAllLines("Contains.txt");
            }
        }

        private void Length_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireLength = requireLength ? false : true;
                goodLength = Convert.ToInt32(File.ReadAllText("GoodLength.txt"));
            }
        }

        private void Uppercase_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireUppercase = requireUppercase ? false : true;
                uppercase = Convert.ToInt32(File.ReadAllText("Uppercase.txt"));
            }
        }

        private void MeetAll_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireAll= true;
            }
        }

        private void MeetAny_Checked(object sender, RoutedEventArgs e)
        {
            if (pause)
            {
                requireAll = false;

            }
        }
    }
}
