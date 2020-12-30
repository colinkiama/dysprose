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
       
        public DysproseSessionLength SessionLength { get; set; }
        public int FadeInterval { get; set; }
        public bool AreBackEditsDisabled { get; set; }

        public bool Equals(DysproseSessionSettings other)
        {
            return this.SessionLength == other.SessionLength
                   && this.FadeInterval == other.FadeInterval
                   && this.AreBackEditsDisabled == other.AreBackEditsDisabled;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + SessionLength.GetHashCode();
            hash = (hash * 7) + FadeInterval.GetHashCode();
            hash = (hash * 7) + AreBackEditsDisabled.GetHashCode();

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
