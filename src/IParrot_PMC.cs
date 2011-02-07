using System;
namespace ParrotSharp
{
	public interface IParrot_PMC
	{
		Parrot Parrot { get; }
		IntPtr RawPointer { get; }
		Parrot_PMC FindMethod(string name);
		void InvokeMethod(Parrot_String name, Pmc.CallContext signature);
	}
}

