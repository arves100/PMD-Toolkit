/*The MIT License (MIT)

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

namespace PMDToolkit.Maps
{
    public struct FloorLink : IEquatable<FloorLink>
    {
        public int FloorNum;
        public int EntranceIndex;

        public FloorLink(int floorNum, int entranceIndex)
        {
            FloorNum = floorNum;
            EntranceIndex = entranceIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is FloorLink && Equals((FloorLink)obj);
        }

        public bool Equals(FloorLink other)
        {
            return FloorNum == other.FloorNum &&
                   EntranceIndex == other.EntranceIndex;
        }

        public override int GetHashCode()
        {
            var hashCode = 1727950620;
            hashCode = hashCode * -1521134295 + FloorNum.GetHashCode();
            hashCode = hashCode * -1521134295 + EntranceIndex.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(FloorLink link1, FloorLink link2)
        {
            return link1.Equals(link2);
        }

        public static bool operator !=(FloorLink link1, FloorLink link2)
        {
            return link1.Equals(link2);
        }
    }
}
