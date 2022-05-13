using System;
using System.Collections.Generic;
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

namespace MatchGame
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfElapsedTime;
        int matchFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐵", "🐵", 
                "🐺", "🐺",
                "🐭", "🐭",
                "🐲", "🐲",
                "🦄", "🦄",
                "🐱", "🐱",
                "🦁", "🦁",
                "🦒", "🦒"
            };

            Random random = new Random();

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != timeTextBlock.Name)
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    textBlock.Text = animalEmoji[index];
                    animalEmoji.RemoveAt(index);
                }
            }
            tenthsOfElapsedTime = 0;
            matchFound = 0;
            timer.Start();
        }

        TextBlock lastClickedTextBlock = null;
        bool isItfistClick = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock clickedTextBlock = sender as TextBlock;

            if(isItfistClick == false)
            {
                isItfistClick = true;
                lastClickedTextBlock = clickedTextBlock;
                clickedTextBlock.Visibility = Visibility.Hidden;
            }
            else if(clickedTextBlock.Text == lastClickedTextBlock.Text)
            {
                isItfistClick = false;
                clickedTextBlock.Visibility = Visibility.Hidden;
                matchFound++;
            }
            else
            {
                isItfistClick = false;
                clickedTextBlock.Visibility = Visibility.Visible;
                lastClickedTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchFound == 8)
            {
                SetUpGame();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfElapsedTime++;
            timeTextBlock.Text = (tenthsOfElapsedTime / 10f).ToString("0.0s");

            if (matchFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text += " Play Again?"; 
            }
        }
    }
}
