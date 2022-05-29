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