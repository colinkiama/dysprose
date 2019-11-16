using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Structs
{
    public struct DysproseSessionLength : IEquatable<DysproseSessionLength>
    {
        private int _length;
        private TimeUnit _unitOfLength;

        public int Length { get => _length; set => _length = value; }
        public TimeUnit UnitOfLength { get => _unitOfLength; set => _unitOfLength = value; }
        public bool Equals(DysproseSessionLength other)
        {
            return this._length == other._length && this._unitOfLength == other._unitOfLength;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is DysproseSessionLength other)
            {
                isEqual = this.Equals(other);
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + _length.GetHashCode();
            hash = (hash * 7) + _unitOfLength.GetHashCode();

            return hash;
        }

        public static bool operator ==(DysproseSessionLength left, DysproseSessionLength right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DysproseSessionLength left, DysproseSessionLength right)
        {
            return !(left == right);
        }

    }
}
