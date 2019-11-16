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
        private int length;
        private TimeUnit unitOfLength;

        public int Length { get => length; set => length = value; }
        public TimeUnit UnitOfLength { get => unitOfLength; set => unitOfLength = value; }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(DysproseSessionLength left, DysproseSessionLength right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DysproseSessionLength left, DysproseSessionLength right)
        {
            return !(left == right);
        }

        public bool Equals(DysproseSessionLength other)
        {
            throw new NotImplementedException();
        }
    }
}
