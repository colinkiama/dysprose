using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Structs
{
    public struct DysproseGlobalSettings : IEquatable<DysproseGlobalSettings>
    {
        private double fontSize;

        public double FontSize { get => fontSize; set => fontSize = value; }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
