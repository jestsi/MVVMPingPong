using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ApplicationModels
{
    public class RocketModel : INotifyPropertyChanged
    {
        public const int OffsetFromBound = 20;

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

        public override int GetHashCode()
        {
            const uint fnvOffsetBasic = 0x811c9dc5;
            const int fnvPrime = 0x01000193;
            
            var hash = fnvOffsetBasic ^
                       (Height.GetHashCode()/10 ^ Width.GetHashCode()%124 + X.GetHashCode() + Y.GetHashCode());
            hash *= fnvPrime;

            return (int)hash;
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