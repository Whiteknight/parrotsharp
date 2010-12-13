using System;

namespace Parrot {
    abstract class ParrotPointer {
        private readonly IntPtr ptr = IntPtr.Zero;

        public IntPtr RawPointer {
            get { return this.ptr; }
        }

        public ParrotPointer(IntPtr ptr) {
            this.ptr = ptr;
        }
    }
}
