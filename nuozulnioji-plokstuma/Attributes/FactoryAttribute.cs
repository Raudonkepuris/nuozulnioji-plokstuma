using System;

namespace nuozulnioji_plokstuma.Attributes
{
    /// <summary>
    ///  Aiškinimo klasė "Simple factory pattern klasėm" 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class FactoryAttribute : Attribute
    {
        /// <summary>
        /// Parametras paaiškinimui. 
        /// </summary
        public string Description { get; }

        /// <summary>
        /// Konstruktorius priskiriantis vertę <paramref cref="Description"/>.
        /// <param description="description">Vertė kuri bus priskirta <paramref cref="Description"/>.</param>
        /// </summary>
        public FactoryAttribute(string description)
        {
            Description = description;
        }
    }
}