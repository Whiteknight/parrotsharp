using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ParrotSharp
{
    public class ParrotException : Exception
	{
		#region Private Fields
		
		private Parrot Parrot;
        private IParrot_PMC raw_exception;
        private string msg;
		private string backtrace;
		
		#endregion
		
		#region Constructor

        public ParrotException(Parrot parrot, string msg)
		{
            this.msg = msg;
			this.Parrot = parrot;
        }

        public ParrotException(Parrot parrot, IParrot_PMC exception)
		{
            this.raw_exception = exception;
			this.msg = exception.ToString();
			this.Parrot = parrot;
			// TODO: Verify that this is an exception PMC
        }

        public ParrotException(Parrot parrot, Parrot_String msg)
		{
			this.msg = msg.ToString();
			this.Parrot = parrot;
            // TODO: Capture the current C# backtrace
        }
		
		#endregion
		
		#region Exception

		public override string Message { get { return this.msg; } }

        [DllImport("parrot")]
        private static extern int Parrot_api_get_exception_backtrace(IntPtr interp_pmc, IntPtr exception,
		                                                      out IntPtr bt);

		public override string StackTrace
		{
			get 
			{ 
				if (this.backtrace != null)
					return this.backtrace;
				if (this.raw_exception != null) {
					IntPtr bt_raw = IntPtr.Zero;
					int result = Parrot_api_get_exception_backtrace(this.Parrot.RawPointer, this.raw_exception.RawPointer, out bt_raw);
					if (result != 1) {
						this.backtrace = "";
						throw new Exception("Parrot: Catastrophic error. Could not get backtrace.", this);
					}
					Parrot_String bt = new Parrot_String(this.Parrot, bt_raw);
					this.backtrace = bt.ToString();
					return this.backtrace;
				}
				else {
					this.backtrace = base.StackTrace;
				}
				return this.backtrace;
			}
		}
		
		#endregion
    }
}
