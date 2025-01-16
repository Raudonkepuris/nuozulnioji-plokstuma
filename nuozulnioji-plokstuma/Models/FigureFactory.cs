using nuozulnioji_plokstuma.Attributes;

namespace nuozulnioji_plokstuma.Models
{
    /// <summary>
    /// "Simple factory" principo klasė norint gauti figūros objektą.
    /// </summary>
    [FactoryAttribute("Factory klasė")]
    public static class FigureFactory
    {
        /// <summary>
        /// Metodas gražinantis figūros objektą.
        /// </summary>
        /// <param name="figure">Figūros tipas iš <see cref="Figures"/> išvardijimo klasės.</param>
        /// <param name="position">Figūros pozicija.</param>
        /// <returns>Figūros objektą.</returns>
        /// <exception cref="ArgumentException">Exception grąžinamas jeigu paduodama nepalaikomas figūros tipas.</exception>
        public static Figure GetFigure(Figures figure, Position position)
        {
            if (figure.Equals(Figures.Circle))
            {
                return new Circle(position);
            }
            else if (figure.Equals(Figures.Square))
            {
                return new Square(position);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Metodas grąžinantis figūros objektą su standartine pozicija.
        /// </summary>
        /// <param name="figure">Figūros tipas iš <see cref="Figures"/> išvardijimo klasės.</param>
        /// <returns>Figūros objektą.</returns>
        public static Figure GetFigure(Figures figure)
        {
            var position = new Position();
            return GetFigure(figure, position);
        }
    }
}
