using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Structs
{
    public struct DysproseGlobalSettings : IEquatable<DysproseGlobalSettings>
    {
        private double _fontSize;

        public double FontSize { get => _fontSize; set => _fontSize = value; }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DysproseGlobalSettings otherSettings)
            {
                isEqual = this.Equals(otherSettings);
            }

            return isEqual;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + _fontSize.GetHashCode();

            return hash;
        }

        public static bool operator ==(DysproseGlobalSettings left, DysproseGlobalSettings right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DysproseGlobalSettings left, DysproseGlobalSettings right)
        {
            return !(left == right);
        }

        public bool Equals(DysproseGlobalSettings other)
        {
            return this._fontSize == other._fontSize;
        }
    }
}
