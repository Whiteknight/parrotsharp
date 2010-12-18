using System;
namespace ParrotSharp
{
	public interface IParrot_PMC
	{
		Parrot_String GetParrotString();
		Parrot Parrot { get; }
		IntPtr RawPointer { get; }
	}
}

