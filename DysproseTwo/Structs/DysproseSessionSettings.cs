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
        private TimeSpan sessionLength;
        private double fontSize;

        public double FontSize { get => fontSize; set => fontSize = value; }
        public TimeSpan SessionLength { get => sessionLength; set => sessionLength = value; }

        public bool Equals(DysproseSessionSettings other)
        {
            bool isEqual = false;
            if (other is DysproseSessionSettings otherSettings)
            {
                isEqual = this.fontSize == otherSettings.fontSize && this.sessionLength == otherSettings.sessionLength;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + sessionLength.GetHashCode();
            hash = (hash * 7) + fontSize.GetHashCode();

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
