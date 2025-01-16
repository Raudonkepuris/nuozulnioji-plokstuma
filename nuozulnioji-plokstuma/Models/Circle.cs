namespace nuozulnioji_plokstuma.Models
{
    public class Circle : Figure
    {
        private const string NAME = "Apskritimas";
        private const string SOURCE = "Assets/circle.png";
        private const int DEFAULT_POSITION_PARAM = 0;

        public Circle() : base(NAME, SOURCE, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM) { }

        public Circle(Position position) : base(){
            name = NAME;
            source = SOURCE;
            this.position = position;
        }

        public override long AnimationTime(double frictionCoefficient, double platformWidth)
        {
            throw new NotImplementedException("Simuliacija apskritimui dar nesukurta.");
        }
    }
}
