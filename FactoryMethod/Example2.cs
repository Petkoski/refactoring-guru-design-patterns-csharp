using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    /**
     * Factory Method 
     */
    public class Point
    {
        private readonly double _x;
        private readonly double _y;

        // Making the constructor 'protected'
        protected Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        // You also want to initialize the point from polar coordinates
        // Issue: Type 'Point' already defines a member called '.ctor' with the same parameter types
        //public Point(double r, double theta)
        //{
        //    _x = r * Math.Cos(theta);
        //    _y = r * Math.Sin(theta);
        //}

        // Factory Method
        // Exposing some static methods for creating new points
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public override string ToString()
        {
            return $"({_x}, {_y})";
        }
    }

    /**
     * Factory
     */
    public class PointWithPublicConstructor
    {
        private readonly double _x;
        private readonly double _y;

        public PointWithPublicConstructor(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return $"({_x}, {_y})";
        }
    }

    public class PointFactory
    {
        public static PointWithPublicConstructor NewCartesianPoint(double x, double y)
        {
            return new PointWithPublicConstructor(x, y);
        }

        public static PointWithPublicConstructor NewPolarPoint(double rho, double theta)
        {
            return new PointWithPublicConstructor(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

    /**
     * Inner Factory
     */
    public class Point2
    {
        private readonly double _x;
        private readonly double _y;

        // Inner factories exist because inner classes can access the outer 
        // class's private members and, conversely, an outer class can access 
        // an inner class's private members.
        private Point2(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return $"({_x}, {_y})";
        }

        public static class Point2Factory
        {
            public static Point2 NewCartesianPoint(double x, double y)
            {
                return new Point2(x, y);
            }

            public static Point2 NewPolarPoint(double rho, double theta)
            {
                return new Point2(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }
}
