using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_IEnumerable
{
    public class Path : IEnumerable<Point3D>
    {
        private List<Point3D> points = new List<Point3D>();
        public IEnumerator<Point3D> GetEnumerator() => points.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => points.GetEnumerator();

        public void Add(Point3D pt) => points.Add(pt);
    }

    public class Point3D
    {
        private double x;
        private double y;
        private double z;

        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public static class Extensions
    {
        public static void Add(this Path path, double x, double y, double z) => path.Add(new Point3D(x, y, z));
    }
}
