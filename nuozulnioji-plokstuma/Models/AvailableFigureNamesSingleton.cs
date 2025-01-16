using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuozulnioji_plokstuma.Models
{
    public class AvailableFigureNamesSingleton
    {
        private static AvailableFigureNamesSingleton _instance;

        private static readonly object _lock = new object();

        public string[] CalculatedArray { get; private set; }

        private AvailableFigureNamesSingleton()
        {
            CalculatedArray = CalculateArray();
        }

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
