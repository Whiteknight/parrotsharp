using System;

namespace Parrot
{
    abstract class ParrotPointer
	{
        private readonly IntPtr ptr = IntPtr.Zero;
		private readonly Parrot parrot;

        public IntPtr RawPointer {
            get { return this.ptr; }
        }

		public Parrot Parrot
		{
			get { return this.parrot; }
		}

        public ParrotPointer(Parrot parrot, IntPtr ptr) {
			this.parrot = parrot;
            this.ptr = ptr;
        }
    }
}
