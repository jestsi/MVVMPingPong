﻿namespace MVVMPingPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
    
            DataContext = new ApplicationViewModel(Width, Height);
        }
    }
}