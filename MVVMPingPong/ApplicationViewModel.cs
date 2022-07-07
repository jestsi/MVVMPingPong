using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MVVMPingPong
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public RocketModel Rocket { get; }
        public RocketModel Rocket2 { get; }
        
        private RelayCommand _keyEventHandelCommand;
        public RelayCommand KeyEventHandlerProp
        {
            get
            {
                return _keyEventHandelCommand ??
                       (_keyEventHandelCommand = new RelayCommand(obj => KeyEventHandler((KeyEventArgs)obj)));
            }
        }

        private Size _sizeWindow;
        
        public int RocketY
        {
            get => Rocket.Y;
            set
            {
                Rocket.Y = value;
                OnPropertyChanged("RocketY");
            }
        }
        public int Rocket2Y
        {
            get => Rocket2.Y;
            set
            {
                Rocket2.Y = value;
                OnPropertyChanged("Rocket2Y");
            }
        }
        public int RocketX
        {
            get => Rocket.X;
            set
            {
                Rocket.X = value;
                OnPropertyChanged("RocketX");
            }
        }
        public int Rocket2X
        {
            get => Rocket2.X;
            set
            {
                Rocket2.X = value;
                OnPropertyChanged("Rocket2X");
            }
        }
        public double RocketWidth
        {
            get => Rocket.Width;
            set
            {
                Rocket.Width = value;
                OnPropertyChanged("RocketWidth");
            }
        }
        public double Rocket2Width
        {
            get => Rocket2.Width;
            set
            {
                Rocket2.Width = value;
                OnPropertyChanged("Rocket2Width");
            }
        }
        public double RocketHeight
        {
            get => Rocket.Height;
            set
            {
                Rocket.Height = value;
                OnPropertyChanged("RocketHeight");
            }
        }
        public double Rocket2Height
        {
            get => Rocket2.Height;
            set
            {
                Rocket2.Height = value;
                OnPropertyChanged("Rocket2Height");
            }
        }

        private void KeyEventHandler(KeyEventArgs args)
        {
            Direction? dirToMove = null;
            switch (args.Key)
            {
                case Key.W:
                case Key.Up:
                    dirToMove = Direction.Up;
                    break;
                case Key.S:
                case Key.Down:
                    dirToMove = Direction.Down;
                    break;
            }
            
            if ( args.Key == Key.W || args.Key == Key.S )
                Rocket.Move(dirToMove);
            else
                Rocket2.Move(dirToMove);
        }
        
        public ApplicationViewModel(double width, double height)
        {
            _sizeWindow = new Size(width, height);
            Rocket = new RocketModel();
            Rocket2 = new RocketModel(_sizeWindow.Width, _sizeWindow.Height);
            
            Rocket.PropertyChanged += RocketOnPropertyChanged;
            Rocket2.PropertyChanged += RocketOnPropertyChanged;
        } 

        private void RocketOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is RocketModel rocket)) return;
            switch (e.PropertyName)
            {
                case "X":
                    if (rocket.Equals(Rocket))
                        RocketX = rocket.X;
                    else
                        Rocket2X = rocket.X;
                    break;
                case "Y":
                    if (rocket.Equals(Rocket))
                        RocketY = rocket.Y;
                    else
                        Rocket2Y = rocket.Y;
                    break;
                case "Width":
                    if (rocket.Equals(Rocket))
                        RocketWidth = rocket.Width;
                    else
                        Rocket2Width = rocket.Width;
                    break;
                case "Height":
                    if (rocket.Equals(Rocket))
                        RocketHeight = rocket.Height;
                    else
                        Rocket2Height = rocket.Height;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}