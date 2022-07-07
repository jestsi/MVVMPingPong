using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVMPingPong
{
    public class RocketModel : INotifyPropertyChanged
    {
        private const int OffsetFromBound = 20;

        public double Width { get; set; }
        public double Height { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public SolidColorBrush Color { get; set; }

        public int StepSize { get; set; }

        public RocketModel()
        {
            Width = 20;
            Height = 100;

            X = (int)Width + OffsetFromBound;
            Y = (int)Height/2;

            Color = Brushes.Black;
            StepSize = 5;
        }
        
        public RocketModel(double width, double height) : this()
        {
            X = (int)width - OffsetFromBound;
            Y = (int)height/2;
        }

        public override int GetHashCode()
        {
            const int fnvOffsetBasic = 1469598;
            const int fnvPrime = 1099511;
            
            var hash = fnvOffsetBasic ^ (Height.GetHashCode() + Width.GetHashCode() + X.GetHashCode() + Y.GetHashCode());
            hash *= fnvPrime;

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return GetHashCode() == obj.GetHashCode();
        }

        public void Move(Direction? dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    Y -= StepSize;
                    OnPropertyChanged("Y");
                    break;
                case Direction.Down:
                    Y += StepSize;
                    OnPropertyChanged("Y");
                    break;
                case Direction.Left:
                    X -= StepSize;
                    OnPropertyChanged("X");
                    break;
                case Direction.Right:
                    X += StepSize;
                    OnPropertyChanged("X");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}