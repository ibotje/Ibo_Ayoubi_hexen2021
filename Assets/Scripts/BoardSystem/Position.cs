namespace BoardSystem
{
    public struct Position
    {
        public int X;
        public int Y;
        public int Z;

        public Position(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Position(int q, int r)
        {
            this.X = q;
            this.Z = r;
            this.Y = -this.X - this.Z;
        }

    }

}
