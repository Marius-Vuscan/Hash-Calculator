using System;
using System.Collections.Generic;
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
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HashCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string text = "";
        string algorithm;

        private void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            text = "";

            try
            {
                TextReader tr = new StreamReader(@"../../TextFile1.txt");
                text = tr.ReadToEnd();
                ReadFileStatus.Foreground = new SolidColorBrush(Colors.Green);
                ReadFileStatus.Text = "Succes!";
            }
            catch (Exception)
            {
                ReadFileStatus.Foreground = new SolidColorBrush(Colors.Red);
                ReadFileStatus.Text="Error in file reading!";
            }
        }

        private void AlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            algorithm = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
        }

        static string GetHash(HashAlgorithm hashA, string input)
        {
            byte[] data = hashA.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));

            return sb.ToString();
        }

        private async void CalculateHashButton_Click(object sender, RoutedEventArgs e)
        {
            string hashString = "";
            await Task.Run(() => 
            {
                using (HashAlgorithm hashA = HashAlgorithm.Create(algorithm))
                {
                    hashString = GetHash(hashA, text);
                }
            });

            HashTextBlock.Text = "Hash: " + hashString;
        }
    }
}
