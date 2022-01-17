using System;

namespace BoardSystem
{
    public class Tile
    {
        public event EventHandler HighlightStatusChanged;
        public event EventHandler TileTaken;
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

        internal void Taken()
        {
            OnHexenPieceTaken(EventArgs.Empty);
        }

        protected virtual void OnHexenPieceTaken(EventArgs arg)
        {
            EventHandler handler = TileTaken;
            handler?.Invoke(this, arg);
        }
    }
}
