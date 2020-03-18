using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gotFocused1 = false;
        private bool gotFocused2 = false;
        List<Player> playerList = new List<Player>();

        public MainWindow()
        {
            InitializeComponent();
            AgeRange();
            LoadList();
        }
        private void LoadList()
        {
            List<string> players = File.ReadAllLines("players.txt", Encoding.Default).ToList();
            foreach(string player in players)
            {
                string[] tmp = player.Split(';');
                playerList.Add(new Player(tmp[0], tmp[1], Int32.Parse(tmp[2]), Int32.Parse(tmp[3])));
            }
            box.ItemsSource = playerList;
        }

        private void AgeRange()
        {
            for (int i = 15; i <= 55; i++)
            {
                age.Items.Add(i.ToString());
            }
            age.Text = "15";
            age.SelectedItem = 15;
        }

        private void weight_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            weight.Content = slider.Value;
        }

        private void name1_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (gotFocused1 == false)
            {
                textbox.Clear();
                textbox.Foreground = Brushes.Black;
                gotFocused1 = true;
            }
        }

        private void name1_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text.Length == 0)
            {
                textbox.BorderBrush = Brushes.Red;
                textbox.BorderThickness = new Thickness(2);
                textbox.ToolTip = "Pole nie może być puste";
            }
            else
            {
                textbox.BorderBrush = Brushes.LightGray;
                textbox.BorderThickness = new Thickness(1);
                textbox.ClearValue(ToolTipProperty);
            }
        }

        private void name2_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (gotFocused2 == false)
            {
                textbox.Clear();
                textbox.Foreground = Brushes.Black;
                gotFocused2 = true;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (gotFocused1 == true && gotFocused2 == true && name1.Text.Length != 0 && name2.Text.Length != 0)
            {
                playerList.Add(new Player(name1.Text, name2.Text, Int32.Parse(age.Text), (int)weight_slider.Value));
                Reset();
            } 
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (box.SelectedItem != null)
            {
                playerList.RemoveAt(box.SelectedIndex);
                Reset();
            }
        }

        private void mod_Click(object sender, RoutedEventArgs e)
        {
            if (box.SelectedItem != null)
            {
                playerList[box.SelectedIndex].FirstName = name1.Text;
                playerList[box.SelectedIndex].SecondName = name2.Text;
                playerList[box.SelectedIndex].Age = Int32.Parse(age.Text);
                playerList[box.SelectedIndex].Weight = (int)weight_slider.Value;
                Reset();
            }
        }

        private void box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            if(listbox.SelectedItem != null)
            {
                name1.Text = playerList[listbox.SelectedIndex].FirstName;
                name2.Text = playerList[listbox.SelectedIndex].SecondName;
                name1.Foreground = Brushes.Black;
                name2.Foreground = Brushes.Black;
                age.Text = playerList[listbox.SelectedIndex].Age.ToString();
                age.SelectedItem = playerList[listbox.SelectedIndex].Age;
                weight_slider.Value = playerList[listbox.SelectedIndex].Weight;
                gotFocused1 = true;
                gotFocused2 = true;
            }
        }

        private void Reset()
        {
            box.Items.Refresh();
            name1.Text = "Wprowadź imię";
            name2.Text = "Wprowadź nazwisko";
            name1.Foreground = Brushes.LightGray;
            name2.Foreground = Brushes.LightGray;
            age.SelectedItem = 15;
            age.Text = "15";
            weight_slider.Value = 55;
            gotFocused1 = false;
            gotFocused2 = false;
            box.UnselectAll();
            grid1.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("players.txt"))
            {
                foreach (Player p in playerList)
                {
                    writer.WriteLine($"{p.FirstName};{p.SecondName};{p.Age};{p.Weight}");
                }
            }
        }
    }
}
