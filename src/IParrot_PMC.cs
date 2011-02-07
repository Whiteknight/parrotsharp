using System;
namespace ParrotSharp
{
	public interface IParrot_PMC
	{
		Parrot Parrot { get; }
		IntPtr RawPointer { get; }
		IParrot_PMC FindMethod(Parrot_String name);
		IParrot_PMC[] InvokeMethod(Parrot_String name, IParrot_PMC signature);
	}
}

