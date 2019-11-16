using DysproseTwo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DysproseTwo.Structs
{
    public struct SessionLength
    {
        private int length;
        private TimeUnit unitOfLength;

        public int Length { get => length; set => length = value; }
        public TimeUnit UnitOfLength { get => unitOfLength; set => unitOfLength = value; }
    }
}
