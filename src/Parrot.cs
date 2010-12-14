using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Parrot
{
    public class Parrot : IParrotPointer
    {
        private Parrot_PMC Interp;

		# region IParrotPointer

		public IntPtr RawPointer { get { return this.Interp.RawPointer; } }
		public Parrot IParrotPointer.Parrot { get { return this; } }

		# endregion

		# region Constructor

		[DllImport("parrot")]
        private static extern int Parrot_api_make_interpreter(IntPtr parent,
            IntPtr args, ref IntPtr interp);

		[DllImport("parrot", CharSet=CharSet.Ansi)]
		private static extern int Parrot_api_set_executable_name(IntPtr interp, string exename);

        public Parrot() {
            IntPtr interp_raw = new IntPtr();
            int result = Parrot_api_make_interpreter(IntPtr.Zero, IntPtr.Zero, ref interp_raw);
            if (result != 1)
                this.GetErrorResult();
            this.Interp = new Parrot_PMC(this, interp_raw);
        }

		public Parrot(string exename) : this()
		{
			int result = Parrot_api_set_executable_name(this.RawPointer, exename);
			if (result != 1)
				this.GetErrorResult();
		}

        # endregion

		# region Error Handling

		[DllImport("parrot")]
        private static extern int Parrot_api_get_result(IntPtr interp_pmc,
            out int is_error, out IntPtr exception, out int exit_code,
            out IntPtr errmsg);

        public void GetErrorResult() {
            int is_error = 0;
            IntPtr exception_raw = IntPtr.Zero;
            int exit_code = 0;
            IntPtr errmsg_raw = IntPtr.Zero;
            int result = Parrot_api_get_result(this.Interp.RawPointer,
                out is_error, out exception_raw, out exit_code, out errmsg_raw);
            if (result == 0)
                throw new ParrotException(this, "Catastrophic failure. Could not get result.");
            Parrot_PMC exception = new Parrot_PMC(this, exception_raw);
            throw new ParrotException(this, exception);
		}

		# endregion

		# region Run bytecode

		[DllImport("parrot")]
		private static extern int Parrot_api_run_bytecode(IntPtr interp, IntPtr bc_pmc, IntPtr arg_pmc);

		public void RunBytecode(Parrot_PMC bytecode, Parrot_PMC args)
		{
			int result = Parrot_api_run_bytecode(this.RawPointer, bytecode.RawPointer, args.RawPointer);
			if (result != 1)
				this.GetErrorResult();
		}

		# endregion


	}
}
