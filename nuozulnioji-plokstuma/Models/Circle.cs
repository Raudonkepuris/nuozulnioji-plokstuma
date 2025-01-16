namespace nuozulnioji_plokstuma.Models
{
    /// <summary>
    /// Apskritimo figūros klasė.
    /// </summary>
    public class Circle : Figure
    {
        private const string NAME = "Apskritimas";
        private const string SOURCE = "Assets/circle.png";
        private const int DEFAULT_POSITION_PARAM = 0;

        /// <summary>
        /// Numatytasis konstruktorius kai apskritimo figūros objektas yra kuriamas be parametrų.
        /// </summary>
        public Circle() : base(NAME, SOURCE, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM, DEFAULT_POSITION_PARAM) { }

        /// <summary>
        /// Konstruktorius kai apskritimo figūros objektas kuriamas su <paramref name="position"/> parametru.
        /// </summary>
        /// <param name="position">Turi nurodyti dabartinę figūros poziciją.</param>
        public Circle(Position position) : base(){
            name = NAME;
            source = SOURCE;
            this.position = position;
        }
        
        /// <summary>
        /// Metodas grąžinantis figūros slidimo laiką milisekundėm.
        /// </summary>
        /// <param name="frictionCoefficient">Platformos trinties koeficientas.</param>
        /// <param name="platformWidth">Platformos ilgis</param>
        /// <returns>Figūros slydimo trukmę.</returns>
        /// <exception cref="NotImplementedException">Exception kuris yra metamas jeigu metodas yra neįgyvendintas.</exception>
        public override long AnimationTime(double frictionCoefficient, double platformWidth)
        {
            throw new NotImplementedException("Simuliacija apskritimui dar nesukurta.");
        }
    }
}
