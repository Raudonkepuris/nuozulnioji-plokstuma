namespace nuozulnioji_plokstuma.Models
{
    public abstract class Figure
    {
        protected readonly double g;

        public string name;
        public string source;
        public Position position;

        protected Figure()
        {
            g = 9.81;
        }

        private Figure(string _name, string _source) : this()
        {
            name = _name;
            source = _source;
        }

        protected Figure(string _name, string _source, int _left, int _top, int _angle, int _width, int _mass) : this(_name, _source)
        {
            position.left = _left;
            position.top = _top;
            position.angle = _angle;
            position.width = _width;
            position.mass = _mass;
        }

        protected Figure(string _name, string _source, Position _position) : this(_name, _source)
        {
            position = _position;
        }

        public abstract long AnimationTime(double frictionCoefficient, double platformWidth);

        public (double left, double top) GetAccurateFigurePosition(double platformLeft, double platformTop)
        {
            var figureImageAdjustedLeft = platformLeft - position.width;
            var figureImageAdjustedTop = platformTop - position.width;

            position.left = figureImageAdjustedLeft;
            position.top = figureImageAdjustedTop;

            return (position.left, position.top);
        }
    }
}
