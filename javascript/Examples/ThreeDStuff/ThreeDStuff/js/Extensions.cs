﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;

namespace ThreeDStuff.js
{
    [Script]
    static class Extensions
    {
        public static IEnumerable<T> Range<T>(this int count, Func<int, T> s)
        {
            return count.Range().Select(s);
        }


        public static IEnumerable<int> Range(this int count)
        {
            return Enumerable.Range(0, count);
        }

        public static IEnumerable<T> ConcatSingle<T>(this IEnumerable<T> e, T x)
        {
            return e.Concat(new[] { x });
        }

        public static void Times(this int e, Action h)
        {
            for (int i = 0; i < e; i++)
            {
                h();
            }
        }
        public static object CreateInstance(this Type t)
        {
            return Activator.CreateInstance(t);
        }

        public static bool ByChance(this double e)
        {
            var v = new System.Random().NextDouble() <= e;

            return v;
        }

        public static double Random(this double e)
        {
            return new System.Random().NextDouble() * e;
        }

        public static int Random(this int i)
        {
            return new Random().Next(i);
        }

        public static int ToInt32(this int e)
        {
            return (int)Math.Round((double)e);
        }

        public static int ToInt32(this double e)
        {
            return (int)Math.Round((double)e);
        }

        public static int ToInt32(this double e, int p)
        {
            return (e / p).ToInt32() * p;
        }

        public static Timer AtTimeout(this int x, Action<Timer> h)
        {
            return new Timer(t => h(t), x, 0);
        }

        public static Timer AtInterval(this int x, Action<Timer> h)
        {
            return new Timer(t => h(t), x, x);
        }

        public static double GetDistance(this Point a, Point b)
        {
            return GetDistance(new Point<double> { X = a.X, Y = a.Y }, b.X, b.Y);
        }

        public static double GetDistance(this Point<double> a, double _x, double _y)
        {
            var dx = a.X - _x;
            var dy = a.Y - _y;

            return System.Math.Sqrt(dx * dx + dy * dy);
        }

        public static double GetRotation(this Point a, double _x, double _y)
        {
            return GetRotation(new Point<double> { X = a.X, Y = a.Y }, _x, _y);

        }

        public static double GetRotation(this Point<double> p, double _x, double _y)
        {
            var x = p.X - _x;
            var y = p.Y - _y;

            if (x == 0)
                if (_y < 0)
                    return System.Math.PI / 2;
                else
                    return (System.Math.PI / 2) * 3;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }



        public static double ToRadians(this int e)
        {
            return ToRadians((double)e);
        }

        public static double ToRadians(this double e)
        {
            return (Math.PI * 2d) * (e / 360d);
        }

        public static int ToDegrees(this double e)
        {
            return (360 * (e / (Math.PI * 2d))).ToInt32();
        }


        public static void ToConsole(this Exception ex)
        {
            Console.WriteLine("error: " + ex.Message);
        }

        public static Timer Until(this Timer t, Func<bool> c)
        {
            t.Tick +=
                x =>
                {
                    if (c())
                        x.Stop();
                };

            return t;
        }

        public static double Sqrt(this double e)
        {
            return Math.Sqrt(e);
        }

        public static void Spawn(this Type e)
        {
            e.SpawnTo(i => e.CreateInstance());
        }

        public static Timer Swap<T>(this T[] e, int interval, Action<T> h)
        {
            return new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    h(e[t.Counter % e.Length]);


                }, interval.Random(), interval);
        }
    }
}
