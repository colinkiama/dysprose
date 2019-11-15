using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Structs
{
    public struct DysproseSessionSettings : IEquatable<DysproseSessionSettings>
    {
        private TimeSpan _sessionLength;
        private double _fontSize;
        private int _fadeInterval;

        public double FontSize { get => _fontSize; set => _fontSize = value; }
        public TimeSpan SessionLength { get => _sessionLength; set => _sessionLength = value; }
        public int FadeInterval { get => _fadeInterval; set => _fadeInterval = value; }


        public bool Equals(DysproseSessionSettings other)
        {
            bool isEqual = false;
            if (other is DysproseSessionSettings otherSettings)
            {
                isEqual = this._fontSize == otherSettings._fontSize && this._sessionLength == otherSettings._sessionLength
                    && this._fadeInterval == otherSettings._fadeInterval;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + _sessionLength.GetHashCode();
            hash = (hash * 7) + _fontSize.GetHashCode();
            hash = (hash * 7) + _fadeInterval.GetHashCode();

            return hash;
        }

        public static bool operator ==(DysproseSessionSettings left, DysproseSessionSettings right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DysproseSessionSettings left, DysproseSessionSettings right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DysproseSessionSettings otherSettings)
            {
                 isEqual = this.Equals(obj);
            }

            return isEqual;
        }
    }
}
