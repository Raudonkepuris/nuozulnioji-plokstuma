namespace nuozulnioji_plokstuma.Models
{
    /// <summary>
    /// Bazinė klasė, kurią turi naudoti visos figūros.
    /// </summary>
    public abstract class Figure
    {
        /// <summary>
        /// Laisvasis kritimo pagreitis.
        /// </summary>
        protected readonly double g;

        /// <summary>
        /// Figūros pavadinimas pasirinktina kalba.
        /// </summary>
        public string name;
        /// <summary>
        /// Figūros šaltinio vieta.
        /// </summary>
        public string source;
        /// <summary>
        /// Figūros pozicija.
        /// </summary>
        public Position position;

        /// <summary>
        /// Numatytasis konstruktorius.
        /// </summary>
        protected Figure()
        {
            g = 9.81;
        }

        private Figure(string _name, string _source) : this()
        {
            name = _name;
            source = _source;
        }

        /// <summary>
        /// Konstruktorius kviečiamas kai norima paduoti figūros pozicija skaičiais.
        /// </summary>
        /// <param name="_name">Figūros pavadinimas.</param>
        /// <param name="_source">Figūros šaltinio vieta.</param>
        /// <param name="_left">Figūros pozicija nuo kairio krašto.</param>
        /// <param name="_top">Figūros pozicija nuo viršutinio krašto.</param>
        /// <param name="_angle">Figūros pasisukimo kampas.</param>
        /// <param name="_width">Figūros plotis.</param>
        /// <param name="_mass">Figūros masė.</param>
        protected Figure(string _name, string _source, int _left, int _top, int _angle, int _width, int _mass) : this(_name, _source)
        {
            position.left = _left;
            position.top = _top;
            position.angle = _angle;
            position.width = _width;
            position.mass = _mass;
        }

        /// <summary>
        /// Konstruktorius kviečiamas kai figūros pozicija paduodama <see cref="Position"/> klasės pavidalu.
        /// </summary>
        /// <param name="_name">Figūros pavadinimas.</param>
        /// <param name="_source">Figūros šaltinio vieta.</param>
        /// <param name="_position">Figūros pozicija</param>
        protected Figure(string _name, string _source, Position _position) : this(_name, _source)
        {
            position = _position;
        }

        /// <summary>
        /// Metodas kuris turi apskaičiuoti kiek laiko figūra slys platforma.
        /// </summary>
        /// <param name="frictionCoefficient">Platformos trinties koeficientas.</param>
        /// <param name="platformWidth">Platformos ilgis.</param>
        /// <returns>Figūros slydimo trukmė milisekundėm</returns>
        public abstract long AnimationTime(double frictionCoefficient, double platformWidth);


        /// <summary>
        /// Metodas suskaičiuojantis ir grąžinantis tikslią figūros pozicija.
        /// </summary>
        /// <param name="platformLeft">Platformos pozicija nuo kairio krašto.</param>
        /// <param name="platformTop">Platformos pozicija nuo viršutinio krašto.</param>
        /// <returns>Tikslią figūros pozicija.</returns>
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
