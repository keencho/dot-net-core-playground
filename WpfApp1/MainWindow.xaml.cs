using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
        private readonly KeyboardClickEventModel _keyboardClickEventModel = new();
        private List<string> logs = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _keyboardClickEventModel;
            Start();
        }

        private void Start()
        {
            Debug.WriteLine("====================키보드 바인딩 이벤트 시작");
            _keyboardClickEventModel.IsListening = true;
            PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void Stop()
        {
            Debug.WriteLine("====================키보드 바인딩 이벤트 종료");
            _keyboardClickEventModel.IsListening = false;
            PreviewKeyDown -= MainWindow_PreviewKeyDown;
        }

        private void KeyboardClickEventHandler(object sender, RoutedEventArgs e)
        {
            if (_keyboardClickEventModel.IsListening)
                Stop();
            else
                Start();
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_keyboardClickEventModel.IsListening)
            {
                logs.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {e.Key.ToString()} is pressed!");
                TextBlock.Text = string.Join(Environment.NewLine, logs);
                // if (e.Key is Key.Q)
                // {
                //     Debug.WriteLine("Hello, World!");
                //     MessageBox.Show("Button Clicked!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                // }
            }
        }
    }
}