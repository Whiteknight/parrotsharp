using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
    public class Parrot : IParrotPointer
    {
        private Parrot_PMC Interp;

		# region IParrotPointer

		public IntPtr RawPointer { get { return this.Interp.RawPointer; } }
		Parrot IParrotPointer.Parrot { get { return this; } }

		# endregion

		# region Constructor

		[DllImport("parrot")]
        private static extern int Parrot_api_make_interpreter(IntPtr parent, int flags,
            IntPtr args, out IntPtr interp);

		[DllImport("parrot", CharSet=CharSet.Ansi)]
		private static extern int Parrot_api_set_executable_name(IntPtr interp, string exename);

        public Parrot() {
            IntPtr interp_raw;
            int result = Parrot_api_make_interpreter(IntPtr.Zero, 0, IntPtr.Zero, out interp_raw);
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
		
		[DllImport("parrot")]
		private static extern int Parrot_api_destroy_interpreter(IntPtr interp);
		
		~Parrot()
		{
			int result = Parrot_api_destroy_interpreter(this.RawPointer);
			if (result != 1)
				this.GetErrorResult();
			this.Interp = null;
		}		

        # endregion
		
		# region Null
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_null(IntPtr interp, out IntPtr pmcnull);
		
		private Parrot_PMC pmcnull;
		
		public Parrot_PMC PmcNull {
			get {
				if (this.pmcnull != null)
					return this.pmcnull;
				IntPtr pmcnull_raw = IntPtr.Zero;
				int result = Parrot_api_pmc_null(this.RawPointer, out pmcnull_raw);
				if (result != 1)
					this.GetErrorResult();
				this.pmcnull = new Parrot_PMC(this, pmcnull_raw);
				return this.pmcnull;
			}
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

		# region Bytecode
		
		public Parrot_PMC GetParrotArgArray(string[] args)
		{
			return args.ToParrotStringArray(this);
		}		

		[DllImport("parrot", CharSet=CharSet.Ansi)]
		private static extern int Parrot_api_load_bytecode_file(IntPtr interp, string filename, out IntPtr pbc);
		
		public Parrot_PMC LoadBytecodeFile(string filename)
		{
			IntPtr pbc = IntPtr.Zero;
			int result = Parrot_api_load_bytecode_file(this.RawPointer, filename, out pbc);
			if (result != 1)
				this.GetErrorResult();
			return new Parrot_PMC(this, pbc);
		}		
		
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
