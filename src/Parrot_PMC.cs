using System;
using System.Runtime.InteropServices;

namespace ParrotSharp
{
    public abstract class Parrot_PMC : ParrotPointer, IParrot_PMC
	{
		#region Constructor
		
        public Parrot_PMC(Parrot parrot, IntPtr ptr) : base(parrot, ptr) { }
		
		#endregion

		#region Get / Set Strings
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_string(IntPtr interp, IntPtr pmc, out IntPtr str);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_string(IntPtr interp, IntPtr pmc, IntPtr str);

		public override string ToString()
		{
			return this.ParrotStringValue.ToString();
		}
		
		protected Parrot_String ParrotStringValue
		{
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
		
		#endregion
		
		#region Get/Set Integer Value
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_integer(IntPtr interp, IntPtr pmc, out int i);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_integer(IntPtr interp, IntPtr pmc, int i);
		
		protected int IntegerValue
		{
			get {
				int i;
				int result = Parrot_api_pmc_get_integer(this.Parrot.RawPointer, this.RawPointer, out i);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return i;
			}		
			set {
				int result = Parrot_api_pmc_set_integer(this.Parrot.RawPointer, this.RawPointer, value);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}		
				
		#endregion		
		
		#region Invoke
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_invoke(IntPtr interp, IntPtr sub, IntPtr signature);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_find_method(IntPtr interp_pmc, IntPtr pmc_obj, IntPtr ps_name, out IntPtr method);
		
		protected IParrot_PMC[] Invoke(IParrot_PMC signature)
		{
			int result = Parrot_api_pmc_invoke(this.Parrot.RawPointer, this.RawPointer, signature.RawPointer);
			if (result != 1)
				this.Parrot.GetErrorResult();
			// TODO: Iterate over the list of return values, wrap them up, return them.
			return null;
		}
		
		public IParrot_PMC FindMethod(Parrot_String name)
		{
			IntPtr sub_ptr;
			int result = Parrot_api_pmc_find_method(this.Parrot.RawPointer, this.RawPointer, name.RawPointer, out sub_ptr);
			if (result != 1)
				this.Parrot.GetErrorResult();
			return new Pmc.Sub(this.Parrot, sub_ptr);
		}
		
		public IParrot_PMC[] InvokeMethod(Parrot_String name, IParrot_PMC signature)
		{
			Pmc.Sub sub = this.FindMethod(name) as Pmc.Sub;
			return sub.Invoke(signature);
		}
		
		#endregion
		
		#region Integer-Keyed Indexing

		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_get_keyed_int(IntPtr interp, IntPtr pmc, int key, out IntPtr value);
		
		[DllImport("parrot")]
		private static extern int Parrot_api_pmc_set_keyed_int(IntPtr interp, IntPtr pmc, int key, IntPtr value);
		
		protected IntPtr this[int key]
		{
			get {
				IntPtr value_ptr = IntPtr.Zero;
				int result = Parrot_api_pmc_get_keyed_int(this.Parrot.RawPointer, this.RawPointer, key, out value_ptr);
				if (result != 1)
					this.Parrot.GetErrorResult();
				return value_ptr;
			}
			set {
				int result = Parrot_api_pmc_set_keyed_int(this.Parrot.RawPointer, this.RawPointer, key, value);
				if (result != 1)
					this.Parrot.GetErrorResult();
			}
		}
		
		#endregion
		
		#region String-Keyed Indexing
		
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
		
		#endregion
		
		#region PMC-Keyed Indexing
		
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
		
		#endregion

	}
}
