using System.Windows.Controls;
using System.Xml.Linq;
using nuozulnioji_plokstuma.Exceptions;

namespace nuozulnioji_plokstuma.Models
{
    /// <summary>
    /// Kvadrato figūros klasė.
    /// </summary>
    public class Square : Figure
    {
        private const string NAME = "Kvadratas";
        private const string SOURCE = "Assets/square.png";
        private const int DEFAULT_POSITION_PARAM = 0;

        /// <summary>
        /// Numatytasis konstruktorius kai kvadrato figūros objektas yra kuriamas be parametrų.
        /// </summary>
        [Obsolete]
        public Square() : base(NAME, SOURCE, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM) { }

        /// <summary>
        /// Konstruktorius kai kvadrato figūros objektas kuriamas su <paramref name="position"/> parametru.
        /// </summary>
        /// <param name="position">Turi nurodyti dabartinę figūros poziciją.</param>

        public Square(Position position) : base(NAME, SOURCE, position) { }

        /// <summary>
        /// Metodas grąžinantis figūros slidimo laiką milisekundėm.
        /// </summary>
        /// <param name="frictionCoefficient">Platformos trinties koeficientas.</param>
        /// <param name="platformWidth">Platformos ilgis</param>
        /// <returns>Figūros slydimo trukmę.</returns>
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

        /// <summary>
        /// Funckija grąžinanti pagrindinius kvadrato parametrus
        /// </summary>
        /// <returns>Pagrindiniai kvadrato parametrai string formatu</returns>
        public override string ToString()
        {
            return $"Name: {NAME}; Src: {SOURCE}; Left: {base.position.left}; Top: {base.position.top}; Angle: {base.position.angle}";
        }

    }
}
