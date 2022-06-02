using Match_3.Core;
using System.Windows;

namespace Match_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private const float GameTime = 60f;


        private MyGame _game;
        private GameStates _state;

        private void OnMainWindowLoaded(object sender, System.EventArgs e)
        {
            GameButton.Click += OnGameButtonClick;
            OnStateChanged(_state);
        }

        private void OnGameButtonClick(object sender, RoutedEventArgs e)
        {
            switch (_state)
            {
                case GameStates.Unloaded:

                    _game = new MyGame(GameCanvas);
                    _game.Load();
                    _game.Ticked += OnGameTicked;
                    _game.ScoreChanged += OnGameScoreChanged;
                    SetScore(0);
                    SetState(GameStates.Running);

                    break;
                case GameStates.Running:
                    break;
                case GameStates.Completed:

                    _game.Ticked -= OnGameTicked;
                    _game.Unload();
                    SetState(GameStates.Unloaded);

                    break;
            }
        }

        
        private void OnGameTicked(ITimeProvider timeProvider)
        {
            if (_state != GameStates.Running) return;

            float remainingTime = GameTime - timeProvider.Time;
            if(remainingTime < 0)
            {
                SetState(GameStates.Completed);
                return;
            }

            ShowGameText(string.Format("Timer: {0:0.00}", remainingTime));
        }

        private void OnGameScoreChanged(int score)
        {
            SetScore(score);
        }


        private void GameCanvas_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_state != GameStates.Running) return;

            _game?.OnMouseDown();
        }


        private void SetState(GameStates state)
        {
            if (_state == state) return;

            _state = state;
            OnStateChanged(_state);
        }

        private void OnStateChanged(GameStates state)
        {
            switch (state)
            {
                case GameStates.Unloaded:
                    ShowGameButton("Play");
                    HideGameText();
                    break;
                case GameStates.Running:
                    HideGameButton();
                    ShowGameText("Timer: 0");
                    break;
                case GameStates.Completed:
                    ShowGameText("Game over!");
                    ShowGameButton("Ok");
                    break;
                default:
                    break;
            }
        }

        private void SetScore(int score)
        {
            ScoreText.Content = score.ToString();
        }
        private void ShowGameText(string text)
        {
            GameText.Visibility = Visibility.Visible;
            GameText.Content = text;
        }
        private void HideGameText()
        {
            GameText.Visibility = Visibility.Hidden;
        }
        private void ShowGameButton(string text)
        {
            GameButton.Visibility = Visibility.Visible;
            GameButton.Content = text;
        }
        private void HideGameButton()
        {
            GameButton.Visibility = Visibility.Hidden;
        }


        private enum GameStates
        {
            Unloaded,
            Running,
            Completed
        }
    }
}