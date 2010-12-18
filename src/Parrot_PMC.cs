using System;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
    public class Parrot_PMC : ParrotPointer, IParrot_PMC
	{
        public Parrot_PMC(Parrot parrot, IntPtr ptr) : base(parrot, ptr) { }

		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_string(IntPtr interp, IntPtr pmc, out IntPtr str);

		public Parrot_String GetParrotString()
		{
			IntPtr str = IntPtr.Zero;
			int result = Parrot_api_pmc_get_string(this.Parrot.RawPointer, this.RawPointer, out str);
			if (result != 1)
				this.Parrot.GetErrorResult();
			return new Parrot_String(this.Parrot, str);
		}

		public override string ToString()
		{
			return this.GetParrotString().ToString();
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_invoke(IntPtr interp, IntPtr sub, IntPtr signature);
		
		protected void Invoke(Parrot_PMC signature)
		{
			int result = Parrot_api_pmc_invoke(this.Parrot.RawPointer, this.RawPointer, signature.RawPointer);
			if (result != 1)
				this.Parrot.GetErrorResult();
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_keyed_int(IntPtr interp, IntPtr pmc, int key, out IntPtr value);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_keyed_int(IntPtr interp, IntPtr pmc, int key, IntPtr value);
		
		protected Parrot_PMC this[int key]
		{
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_keyed_int(this.Parrot.RawPointer, this.RawPointer, key, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return new Parrot_PMC(this.Parrot, value_ptr);
			}
			set {
				int result = Parrot_api_pmc_set_keyed_int(this.Parrot.RawPointer, this.RawPointer, key, value.RawPointer);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_keyed_string(IntPtr interp, IntPtr pmc, IntPtr key, out IntPtr value);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_keyed_string(IntPtr interp, IntPtr pmc, IntPtr key, IntPtr value);
		
		protected Parrot_PMC this[Parrot_String key]
		{
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_keyed_string(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return new Parrot_PMC(this.Parrot, value_ptr);
			}
			set {
				int result = Parrot_api_pmc_set_keyed_string(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, value.RawPointer);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}
		
		protected Parrot_PMC this[string key]
		{
			get { return this[key.ToParrotString(this.Parrot)]; }
			set { this[key.ToParrotString(this.Parrot)] = value; }
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_keyed(IntPtr interp, IntPtr pmc, IntPtr key, out IntPtr value);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_keyed(IntPtr interp, IntPtr pmc, IntPtr key, IntPtr value);
		
		protected Parrot_PMC this[IParrot_PMC key]
		{
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_keyed(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return new Parrot_PMC(this.Parrot, value_ptr);
			}
			set {
				int result = Parrot_api_pmc_set_keyed(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, value.RawPointer);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}
	}
}
