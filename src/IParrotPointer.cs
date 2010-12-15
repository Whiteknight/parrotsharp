using System;

namespace ParrotSharp
{
	public interface IParrotPointer
	{
		IntPtr RawPointer { get; }
		Parrot Parrot { get; }
	}
}