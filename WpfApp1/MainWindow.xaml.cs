using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public class KeyboardClickEventModel : INotifyPropertyChanged
    {
        private bool _isListening = false;

        public bool IsListening
        {
            get => _isListening;
            set
            {
                if (_isListening != value)
                {
                    _isListening = value;
                    OnPropertyChanged("IsListening");
                }
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
    
    public partial class MainWindow : Window
    {
        private readonly KeyboardClickEventModel _keyboardClickEventModel = new KeyboardClickEventModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _keyboardClickEventModel;
        }

        private void KeyboardClickEventHandler(object sender, RoutedEventArgs e)
        {
            if (_keyboardClickEventModel.IsListening)
            {
                Debug.WriteLine("====================키보드 바인딩 이벤트 종료");
                _keyboardClickEventModel.IsListening = false;
                PreviewKeyDown -= MainWindow_PreviewKeyDown;
            }
            else
            {
                Debug.WriteLine("====================키보드 바인딩 이벤트 시작");
                _keyboardClickEventModel.IsListening = true;
                PreviewKeyDown += MainWindow_PreviewKeyDown;
            }
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_keyboardClickEventModel.IsListening)
            {
                Debug.WriteLine(e.Key);
                // if (e.Key is Key.Q)
                // {
                //     Debug.WriteLine("Hello, World!");   
                // }
            }
        }
    }
}