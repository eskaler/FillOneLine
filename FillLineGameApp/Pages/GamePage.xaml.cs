using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using FillLineGame;

namespace FillLineGameApp.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {

        Game game;
       

        public GamePage()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Window),
     Keyboard.KeyDownEvent, new KeyEventHandler(KeyRead), true);
            //this.KeyDown += KeyRead;
        }


        private DispatcherTimer gameTimer;
        private TimeSpan gameTime;
        private DateTime gameStartedAt = DateTime.Now;
        private void gameTimerTick(object sender, EventArgs e)
        {
            gameTime = DateTime.Now - gameStartedAt;
            lGameTime.Content = "Time spent: " + GetGameTime();
        }
        public string GetGameTime()
        {
            return gameTime.ToString(@"mm\:ss\.ff");
        }

        
        private Dictionary<Key, int> moveDirections = 
            new Dictionary<Key, int> {
                { Key.A, 0 },
                { Key.W, 1 },
                { Key.D, 2 },
                { Key.S, 3 },
            };
        private void KeyRead(object sender, KeyEventArgs e)
        {
            if (game != null)
            {
                if (moveDirections.ContainsKey(e.Key))
                {
                    Debug.WriteLine("Got KEY: " + e.Key.ToString() + " Direction: " + moveDirections[e.Key].ToString());
                    game.Move(moveDirections[e.Key]);
                    lGameInfo.Content = "Moves: " + game.GetMovesCount().ToString();
                }
                
                RenderField();
                if (game.IsWin())
                {
                    gameTimer.Stop();
                    MessageBox.Show("WIN!");
                    game = null; 
                }
                return;
            }
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            string levelPath = @"./level1.txt";

            game = new Game(levelPath);
            lGameInfo.Content = "Moves: 0";
            lGameTime.Content = "00:00.00";

            rectWidth = Convert.ToInt32(cGame.ActualWidth / game.GameField.GetFieldWidth()); //rectWidth = 50; 
            rectHeight = Convert.ToInt32(cGame.ActualHeight / game.GameField.GetFieldHeight());//rectHeight = 50; 

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += new EventHandler(gameTimerTick);
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            gameTimer.Start();
            gameStartedAt = DateTime.Now;
            cGame.Focus();
            
            RenderField();
        }

        public int rectWidth, rectHeight;
        SolidColorBrush obstacleBrush = new SolidColorBrush(Color.FromRgb(0, 217, 187));
        SolidColorBrush filledBrush = new SolidColorBrush(Color.FromRgb(55, 71, 79));
        private void RenderField()
        {
            for (int i = 0; i < game.GameField.GetFieldHeight(); i++)
            {
                for (int j = 0; j < game.GameField.GetFieldWidth(); j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Width = rectWidth;
                    rect.Height = rectHeight;
                    Canvas.SetLeft(rect, j * rectWidth);
                    Canvas.SetTop(rect, i * rectHeight);

                    rect.Stroke = obstacleBrush;

                    if (game.GameField.IsCellAnObstacle(new Coords(i, j)))
                        rect.Fill = obstacleBrush;
                    else if (game.GameField.IsCellFilled(new Coords(i, j)))
                        rect.Fill = filledBrush;
                    else
                    {
                        rect.Fill = Brushes.White;
                    }
                    cGame.Children.Add(rect);
                }
            }
            
        }
    }
}
