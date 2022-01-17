using System;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HighlightStatusChanged;

        public Position Position { get; }
        public Tile(Position position)
        {
            this.Position = position;
        }

        private bool _isHighlighted = false;

        public bool IsHighlighted
        {
            get => _isHighlighted;
            internal set
            {
                _isHighlighted = value;
                OnHighlightStatusChanged(EventArgs.Empty);
            }
        }
        protected virtual void OnHighlightStatusChanged(EventArgs args)
        {
            EventHandler handler = HighlightStatusChanged;
            handler?.Invoke(this, args);
        }
    }
}
