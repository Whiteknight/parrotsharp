using System;

namespace Parrot {
    public class Parrot_PMC {
        private IntPtr pmc = IntPtr.Zero;

        public IntPtr RawPMCPointer {
            get { return this.pmc; }
            set { this.pmc = value; }
        }
    }
}
