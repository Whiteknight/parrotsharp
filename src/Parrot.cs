using System;
using System.IO;

namespace Parrot {
    public class Parrot {
        private Parrot_PMC Interp;
        private StreamWriter error_stream;

        [DllImport("parrot")]
        private static extern int Parrot_api_make_interpreter(IntPtr parent,
            IntPtr args, ref IntPtr interp);

        public Parrot() {
            this.error_stream = null;
            IntPtr interp_raw = new IntPtr();
            int result = Parrot_api_make_interpreter(IntPtr.Zero, IntPtr.Zero, ref interp_raw);
            if (result != 1)
                this.CheckParrotError();
            this.Interp = new PMC();
            this.Interp.RawPMCPointer = interp_raw;
        }

        public Parrot(StreamWriter error_stream) : this() {
            this.error_stream = error_stream;
        }

        [DllImport("parrot")]
        private static extern int Parrot_api_get_result(IntPtr interp_pmc,
            out int is_error, out IntPtr exception, out int exit_code,
            out IntPtr errmsg);

        private get_result() {
            int is_error = 0;
            IntPtr exception_raw = IntPtr.Zero;
            exit_code = 0;
            IntPtr errmsg_raw = IntPtr.Zero;
            int result = Parrot_api_get_result(this.Interp.RawPMCPointer,
                out is_error, out exception_raw, out exit_code, out errmsg_raw);
            if (result == 0)
                throw new ParrotException("Parrot: Catastrophic failure");
            Parrot_PMC exception = new Parrot_PMC(exception_raw);
            //Parrot_String errmsg = new Parrot_String(errmsg_raw);
            throw new ParrotException(exception);
        }
    }
}
