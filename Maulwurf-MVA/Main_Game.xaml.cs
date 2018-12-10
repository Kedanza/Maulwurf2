using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Maulwurf_MVA
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Main_Game : Page
    {
        private Moles[] mole = new Moles[9];
        private Player player;
        private DispatcherTimer gameTimer;

        public Main_Game()
        {
            this.InitializeComponent();
            InitGame();
            InsertNewMole();
        }

        private void InitGame()
        {
            for (int i = 0; i < mole.Length; i++)
            {
                mole[i] = new Moles();        
            }
            player = new Player { Score = 0, Lives = 3 };
            Lives.Count = player.Lives;
            ScoreText.Text = player.Score.ToString();
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 1);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
                      
        }

        private void GameTimer_Tick(object sender, object e)
        {
            CheckAllMoles();
        }

        private void MinusPoint()
        {
            player.Lives -= 1;
            Lives.Count = player.Lives;
            if (player.Lives <=0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            throw new NotImplementedException();
        }

        private void CheckAllMoles()
        {
            for (int i = 0; i < mole.Length; i++)
            {
                if (mole[i].Timer >= 0)
                {
                    mole[i].Timer -= 1;
                }
                else
                {
                    if (mole[i].Img != null)
                    {
                        gameField.Children.Remove(mole[i].Img);
                        mole[i].Img = null;
                        mole[i].Timer = -1;
                        MinusPoint();
                    }
                }
            }
        }

        private void AddScore(int scoreValue)
        {
            player.Score += scoreValue;
            ScoreText.Text = player.Score.ToString();
        }

        private void InsertNewMole()
        {
            var pos = 8;
            mole[pos].Generate(pos);
            mole[pos].Img.Tag = mole[pos];
            mole[pos].Img.Tapped += Img_Tapped;
            gameField.Children.Add(mole[pos].Img);
        }

        private void Img_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var img = sender as Image;
            if (img == null)
            {
                return;
            }
            var tempMole = img.Tag as Moles;
            if (tempMole == null)
            {
                return;
            }
            AddScore(1);
            gameField.Children.Remove(tempMole.Img);
        }
    }
}
