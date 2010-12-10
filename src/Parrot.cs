using System;

namespace Parrot {
    public class Parrot {
        private Parrot_PMC Interp;

        [DllImport("parrot")]
        private static extern int Parrot_api_make_interpreter(IntPtr parent, IntPtr args, ref IntPtr interp);

        public Parrot() {
            IntPtr interp_raw = new IntPtr();
            int result = Parrot_api_make_interpreter(IntPtr.Zero, IntPtr.Zero, ref interp_raw);
            if (result != 1)
                this.CheckParrotError();
            this.Interp = new PMC();
            this.Interp.RawPMCPointer = interp_raw;
        }


    }
}
