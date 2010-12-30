using System;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
    public class Parrot_String : ParrotPointer
	{
		private string strcache;
		private IntPtr raw; // a char* pointer to the internal buffer. Must be freed

        public Parrot_String(Parrot parrot, IntPtr ptr) : base(parrot, ptr)
		{
			this.raw = IntPtr.Zero;
		}
		
		public Parrot_String(Parrot parrot, string str) : base(parrot)
		{
			IntPtr pstring = IntPtr.Zero;
			int result = Parrot_api_string_import_ascii(this.Parrot.RawPointer, str, out pstring);
			if (result != 1)
				parrot.GetErrorResult();
			this.ptr = pstring;
		}
		
		~Parrot_String()
		{
			if (this.raw == IntPtr.Zero)
				return;
			int result = Parrot_api_string_free_exported_ascii(this.Parrot.RawPointer, this.raw);
			if (result != 1)
				this.Parrot.GetErrorResult();
		}
		
		[DllImport("parrot", CharSet = CharSet.Ansi)]
		private static extern int Parrot_api_string_import_ascii(IntPtr interp, string str, out IntPtr pstring);

		[DllImport("parrot", CharSet = CharSet.None)]
		private static extern int Parrot_api_string_export_ascii(IntPtr interp, IntPtr str, out IntPtr strout);

		[DllImport("parrot")]
		private static extern int Parrot_api_string_free_exported_ascii(IntPtr interp, IntPtr str);

		private string GetRawString()
		{
			IntPtr temp = IntPtr.Zero;
			int result = Parrot_api_string_export_ascii(this.Parrot.RawPointer, this.RawPointer, out temp);
			if (result != 1)
				this.Parrot.GetErrorResult();
			this.raw = temp;
			return Marshal.PtrToStringAnsi(temp);
		}

		public override string ToString()
		{
			if (this.strcache != null)
				return this.strcache;
			this.strcache = this.GetRawString();
			return this.strcache;
		}


    }
}
