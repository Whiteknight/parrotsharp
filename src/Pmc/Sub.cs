using System;

namespace ParrotSharp.Pmc
{
	public class Sub : Parrot_PMC, IPMCRole_Invocable
	{
		public Sub (Parrot parrot, IntPtr ptr) : base(parrot, ptr) { }
		
		public IParrot_PMC[] Invoke(string sig, params IParrot_PMC[] args)
		{
			CallContext cc = CallContext.GetFactory(this.Parrot).Instance();
			cc.StringValue = sig;
			for (int i = 0; i < args.Length; i++)
				cc.AddArgument(args[i]);
			return this.Invoke(cc);
		}
		
		public new IParrot_PMC[] Invoke(IParrot_PMC signature)
		{
			return base.Invoke(signature);
		}		
		
		public static IPMCFactory GetFactory(Parrot parrot)
		{
			return new PMCFactory<Sub>(parrot, "Sub");
		}		
	}
}

