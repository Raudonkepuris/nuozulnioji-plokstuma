﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nuozulnioji_plokstuma.Models;

namespace nuozulnioji_plokstuma.Models
{
    public static class FigureFactory
    {
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

        public static Figure GetFigure(Figures figure)
        {
            var position = new Position();
            return GetFigure(figure, position);
        }
    }
}
