using System;

namespace ParrotSharp
{
    public abstract class ParrotPointer
	{
        protected IntPtr ptr = IntPtr.Zero;
		private readonly Parrot parrot;

        public IntPtr RawPointer {
            get { return this.ptr; }
        }

		public Parrot Parrot
		{
			get { return this.parrot; }
		}
		
		public ParrotPointer(Parrot parrot)
		{
			this.parrot = parrot;
			this.ptr = IntPtr.Zero;
		}		

        public ParrotPointer(Parrot parrot, IntPtr ptr) {
			this.parrot = parrot;
            this.ptr = ptr;
        }
    }
}
