using System;

namespace Parrot {
    public class Parrot_PMC : ParrotPointer {
        public Parrot_PMC(IntPtr pmc_raw) : base(pmc_raw) { }
    }
}
