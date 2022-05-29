using System.Windows;
using System.Windows.Threading;

namespace Match_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Drawer = new CanvasDrawer(GameCanvas);
            Timer = new DispatcherTimer();
        }


        private readonly CanvasDrawer Drawer;
        private readonly DispatcherTimer Timer;


        private void OnTimerTick(object sender, System.EventArgs e)
        {
        }

        private void OnMainWindowLoaded(object sender, System.EventArgs e)
        {
            Timer.Tick += OnTimerTick;
        }
    }
}

namespace Match_3.Core
{
    public class GameCore
    {
        public float Time { get; private set; }
        public float DeltaTime { get; private set; }


        public void Tick(float time)
        {
            DeltaTime = time - Time;
            Time = time;


        }
    }

    public class Grid
    {
    }

    public class Cell
    {
    }

    public class Gem
    {
    }
}
