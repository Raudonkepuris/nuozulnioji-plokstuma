using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuozulnioji_plokstuma.Models
{
    /// <summary>
    /// Singelton tipo klasė kuri suranda visų galimų figurų tipų pavadinimus.
    /// </summary>
    public class AvailableFigureNamesSingleton
    {
        private static AvailableFigureNamesSingleton _instance;

        private static readonly object _lock = new object();

        /// <summary>
        /// Masyvas kuriama saugomi visų galimų figurų pavadinimai.
        /// </summary>
        public string[] CalculatedArray { get; private set; }

        private AvailableFigureNamesSingleton()
        {
            CalculatedArray = CalculateArray();
        }

        /// <summary>
        /// Parametras kuriuo yra pasiekiamas klasės objektas
        /// </summary>
        public static AvailableFigureNamesSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AvailableFigureNamesSingleton();
                        }
                    }
                }
                return _instance;
            }
        }

        private string[] CalculateArray()
        {
            var arrLength = Enum.GetValues(typeof(Figures)).Length;
            var array = new String[arrLength];
            int index = 0;

            foreach (Figures figure in Enum.GetValues(typeof(Figures)))
            {
                var figureObject = FigureFactory.GetFigure(figure);
                array[index] = figureObject.name;
                index++;
            }

            return array;
        }
    }
}
