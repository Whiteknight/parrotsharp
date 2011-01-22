using System;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
    public class Parrot_PMC : ParrotPointer, IParrot_PMC
	{
        public Parrot_PMC(Parrot parrot, IntPtr ptr) : base(parrot, ptr) { }

		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_string(IntPtr interp, IntPtr pmc, out IntPtr str);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_string(IntPtr interp, IntPtr pmc, IntPtr str);

		public Parrot_String GetParrotString()
		{
			IntPtr str = IntPtr.Zero;
			int result = Parrot_api_pmc_get_string(this.Parrot.RawPointer, this.RawPointer, out str);
			if (result != 1)
				this.Parrot.GetErrorResult();
			return new Parrot_String(this.Parrot, str);
		}
		
		public void SetParrotString(Parrot_String str)
		{
			int result = Parrot_api_pmc_set_string(this.Parrot.RawPointer, this.RawPointer, str.RawPointer);
			if (result != 1)
				this.Parrot.GetErrorResult();
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
		
		public Parrot_PMC FindMethod(string name)
		{
			return this.Parrot.PmcNull;
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_find_method(IntPtr interp_pmc, IntPtr pmc_obj, IntPtr ps_name, out IntPtr method);
		
		//TODO add a better way to modify the signature
		public void InvokeMethod(Parrot_String name, Pmc.CallContext signature) {
			IntPtr sub_ptr;
			int result = Parrot_api_pmc_find_method(this.Parrot.RawPointer, this.RawPointer, name.RawPointer, out sub_ptr);
			if (result != 1) {
					this.Parrot.GetErrorResult();
			}
			else {
				result = Parrot_api_pmc_invoke(this.Parrot.RawPointer, sub_ptr, signature.RawPointer);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}
		
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_string(IntPtr interp, IntPtr pmc, IntPtr value_ptr);
		
		//[DllImport("parrot")]
		//private static extern int Parrot_api_pmc_get_string(IntPtr interp, IntPtr pmc, out IntPtr value_ptr);
		
		protected Parrot_String StringValue {
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_string(this.Parrot.RawPointer, this.RawPointer, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return new Parrot_String(this.Parrot, value_ptr);
			}
			set {
				int result = Parrot_api_pmc_set_string(this.Parrot.RawPointer, this.RawPointer, value.RawPointer);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
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
		
		protected IntPtr this[Parrot_String key]
		{
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_keyed_string(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return value_ptr;
			}
			set {
				int result = Parrot_api_pmc_set_keyed_string(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, value);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}
		
		protected IntPtr this[string key]
		{
			get { return this[key.ToParrotString(this.Parrot)]; }
			set { this[key.ToParrotString(this.Parrot)] = value; }
		}
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_keyed(IntPtr interp, IntPtr pmc, IntPtr key, out IntPtr value);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_keyed(IntPtr interp, IntPtr pmc, IntPtr key, IntPtr value);
		
		protected IntPtr this[IParrot_PMC key]
		{
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_keyed(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return value_ptr;
			}
			set {
				int result = Parrot_api_pmc_set_keyed(this.Parrot.RawPointer, this.RawPointer, key.RawPointer, value);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}

	}
}
