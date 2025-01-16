using nuozulnioji_plokstuma.Exceptions;

namespace nuozulnioji_plokstuma.Models
{
    public class Square : Figure
    {
        private const string NAME = "Kvadratas";
        private const string SOURCE = "Assets/square.png";
        private const int DEFAULT_POSITION_PARAM = 0;

        [Obsolete]
        public Square() : base(NAME, SOURCE, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM) { }

        public Square(Position position) : base(NAME, SOURCE, position) { }

        public override long AnimationTime(double frictionCoefficient, double platformWidth)
        {
            var angleRad = Math.PI * Math.Abs(position.angle) / 180.0;

            var a = g * (Math.Sin(angleRad) - frictionCoefficient * Math.Cos(angleRad));

            if (a > 0)
            {
                return Convert.ToInt64(Math.Round((Math.Sqrt(2 * (platformWidth/1000) / a) * 1000)));
            }
            else
            {
                throw new AccelerationException("Padidink kampą arba sumažink trinties koeficientą, kad kvadratas galėtų slysti.");
            }
        }

    }
}
