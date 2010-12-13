using System;

namespace Parrot {
    public class ParrotException : Exception {
        private Parrot_PMC raw_exception;
        private string msg;

        public ParrotException() {
            this.raw_exception = null;
            this.msg = null;
        }

        public ParrotException(string msg) : this() {
            this.msg = msg;
            // TODO: Capture the current C# backtrace
        }

        public ParrotException(Parrot_PMC *exception) {
            this.raw_exception = exception;
            int result = Parrot_api_get_exception_backtrace(exception.Raw
            // TODO: Get the message from the exception and put it into msg
        }

        public ParrotException(Parrot_String msg) {
            // TODO: Get the string out of msg
            // TODO: Capture the current C# backtrace
        }

        [DllImport("parrot")]
        private static int Parrot_api_get_exception_backtrace(
            IntPtr interp_pmc, IntPtr exception, out IntPtr Parrot_String bt);

        private string backtrace;

        public string GetBacktrace() {
            if (this.backtrace != null)
                return this.backtrace;




    }
}
