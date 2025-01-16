namespace nuozulnioji_plokstuma.Models
{
    /// <summary>
    /// Figūros pozicijos struktūra.
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// Figūros pozicija nuo kairiojo krašto.
        /// </summary>
        public double left { get; set; }
        /// <summary>
        /// Figūros pozicija nuo viršutinio krašto.
        /// </summary>
        public double top { get; set; }
        /// <summary>
        ///  Figūros pasisukimo kampas.
        /// </summary>
        public int angle { get; set; }
        /// <summary>
        /// Figūros plotis.
        /// </summary>
        public double width { get; set; }
        /// <summary>
        /// Figūros masė.
        /// </summary>
        public double mass { get; set; }
    }
}
