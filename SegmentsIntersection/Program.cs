namespace SegmentsIntersection
{
    public class Vector3D
    {
        public double X;
        public double Y;
        public double Z;

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static bool operator ==(Vector3D v1, Vector3D v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z)
                return true;
            return false;
        }

        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            if (v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z)
                return false;
            return true;
        }
    }

    public class Segment3D
    {
        public Vector3D Start;
        public Vector3D End;

        public Segment3D(Vector3D start, Vector3D end)
        {
            Start = start;
            End = end;
        }        
    }

    internal class Program
    {
        static Vector3D? Intersect(Segment3D first, Segment3D second)
        {
            double eps = 1e-15;

            if (first.Start == second.Start)
                return first.Start;

            if (first.Start == second.Start)
                return first.Start;

            if (first.End == second.Start)
                return first.End;

            if (first.End == second.End)
                return first.End;

            double a1 = first.Start.X - first.End.X, b1 = second.End.X - second.Start.X, c1 = second.End.X - first.End.X;
            double a2 = first.Start.Y - first.End.Y, b2 = second.End.Y - second.Start.Y, c2 = second.End.Y - first.End.Y;
            double a3 = first.Start.Z - first.End.Z, b3 = second.End.Z - second.Start.Z, c3 = second.End.Z - first.End.Z;

            double D = a1 * b2  - a2 * b1;

            if (Math.Abs(D) > eps)
            {
                double Dt = c1 * b2 - c2 * b1;
                double Ds = c2 * a1 - c1 * a2;

                double t = Dt / D;
                double s = Ds / D;

                if (t >= 0 && t <= 1 && s >= 0 && s <= 1 && (a3 * t + b3 * s - c3 < eps))
                {
                    double t1 = 1.0 - t;
                    return new(t * first.Start.X + t1 * first.End.X,
                               t * first.Start.Y + t1 * first.End.Y,
                               t * first.Start.Z + t1 * first.End.Z);
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Test {i + 1}:");
                Vector3D a, b, c, d;
                int test = i;
                switch (test)
                {
                    case 0: // Intersection in (1, 1, 1)
                        a = new(0, 0, 0);
                        b = new(2, 2, 2);
                        c = new(0, 2, 2);
                        d = new(2, 0, 0);
                        break;
                    case 1: // No intersection
                        a = new(0, 0, 0);
                        b = new(1, 1, 1);
                        c = new(0, 0, 1);
                        d = new(1, 1, 2);
                        break;
                    case 2: // No intersection(same line)
                        a = new(0, 0, 0);
                        b = new(1, 1, 1);
                        c = new(2, 2, 2);
                        d = new(3, 3, 3);
                        break;
                    case 3: // No intersection 
                        a = new(0, 0, 0);
                        b = new(2, 2, 2);
                        c = new(1, 1, 1);
                        d = new(3, 3, 3);
                        break;
                    case 4: // No intersection (skew)
                        a = new(0, 0, 0);
                        b = new(1, 0, 0);
                        c = new(0, 1, 0);
                        d = new(0, 1, 1);
                        break;
                    case 5: // Intersection in (1, 1, 0)
                        a = new(0, 0, 0);
                        b = new(1, 1, 0);
                        c = new(1, 1, 0);
                        d = new(1, 1, 1);
                        break;
                    case 6: // Intersection in (1, 1, 1)
                        a = new(1, 1, 1);
                        b = new(1, 1, 1);
                        c = new(1, 1, 1);
                        d = new(1, 1, 1);
                        break;
                    case 7: // No intersection
                        a = new(0, 0, 0);
                        b = new(0, 0, 0);
                        c = new(1, 1, 1);
                        d = new(1, 1, 1);
                        break;
                    case 8: // Intersection in ~(1, 1, 0)
                        a = new(0, 0, 0);
                        b = new(1, 1, 0);
                        c = new(0.999, 1.001, 0);
                        d = new(1.001, 0.999, 0);
                        break;
                    default:
                        a = new(0, 0, 0);
                        b = new(0, 0, 0);
                        c = new(0, 0, 0);
                        d = new(0, 0, 0);
                        break;
                }

                Vector3D? Intersection = Intersect(new(a, b), new(c, d));
                if (Intersection is not null)
                    Console.WriteLine($"Segments intersection: ({Intersection.X}, {Intersection.Y}, {Intersection.Z})");
                else
                    Console.WriteLine("Segments are not intersect");
            }
        }
    }
}
