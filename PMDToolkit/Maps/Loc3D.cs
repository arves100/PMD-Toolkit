﻿/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMDToolkit.Maps {
    public struct Loc3D : IEquatable<Loc3D>
    {

        public int X;
        public int Y;
        public int Z;

        public Loc3D(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
        }

        public Loc3D(Loc3D loc) {
            X = loc.X;
            Y = loc.Y;
            Z = loc.Z;
        }

        public Loc2D To2D() {
            return new Loc2D(X, Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Loc3D && Equals((Loc3D)obj);
        }

        public bool Equals(Loc3D other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Loc3D param1, Loc3D param2) {
            return param1.Equals(param2);
        }

        public static bool operator !=(Loc3D param1, Loc3D param2) {
            return !param1.Equals(param2);
        }


        public static Loc3D operator +(Loc3D param1, Loc3D param2) {
            return new Loc3D(param1.X + param2.X, param1.Y + param2.Y, param1.Z + param2.Z);
        }

        public static Loc3D operator -(Loc3D param1, Loc3D param2) {
            return new Loc3D(param1.X - param2.X, param1.Y - param2.Y, param1.Z - param2.Z);
        }
    }
}
