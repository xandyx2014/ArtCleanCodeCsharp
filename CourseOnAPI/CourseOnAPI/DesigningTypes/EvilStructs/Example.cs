using System;
using DesigningTypes.EvilStructs;

namespace DesigningTypes.EvilStructs {
    struct Mutable {
        public Mutable(int x): this() {
            X = x;
        }
        public int IncrementX() {
            X++;
            return X;
        }
        public int X { get; private set; }
    }
    class A {
        public A() {
            PropertyMutable = new Mutable(x: 1);
            ReadonlyMutable = new Mutable(x: 1);
            FieldMutable = new Mutable(x: 1);
        }

        public Mutable PropertyMutable { get; }

        public Mutable FieldMutable;
        public readonly Mutable ReadonlyMutable;
    }
    public class EntryPoint {
        public static void Main() {
            A a = new A();
            Console.WriteLine("Property case");
            Console.WriteLine(a.PropertyMutable.IncrementX());
            Console.WriteLine(a.PropertyMutable.IncrementX());

            Console.WriteLine("Readonly case");
            Console.WriteLine(a.ReadonlyMutable.IncrementX());
            Console.WriteLine(a.ReadonlyMutable.IncrementX());

            #region Secret
            //Mutable tmp1 = a.ReadonlyMutable;
            //Mutable tmp2 = a.ReadonlyMutable;
            //Console.WriteLine(tmp1.IncrementX());
            //Console.WriteLine(tmp2.IncrementX());
            #endregion

            Console.WriteLine("Field case");
            Console.WriteLine(a.FieldMutable.IncrementX());
            Console.WriteLine(a.FieldMutable.IncrementX());

            Console.ReadLine();
        }
    }
}

struct Point {
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y):this() {
        X = x;
        Y = y;
    }

    public Point Increment(int x, int y) {        
        return new Point(x, y);
    }
}

