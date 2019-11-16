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
        private DysproseSessionLength _sessionLength;
        private int _fadeInterval;

        public DysproseSessionLength SessionLength { get => _sessionLength; set => _sessionLength = value; }
        public int FadeInterval { get => _fadeInterval; set => _fadeInterval = value; }


        public bool Equals(DysproseSessionSettings other)
        {
            return this._sessionLength == other._sessionLength
                   && this._fadeInterval == other._fadeInterval;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + _sessionLength.GetHashCode();
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
                isEqual = this.Equals(otherSettings);
            }

            return isEqual;
        }
    }
}
