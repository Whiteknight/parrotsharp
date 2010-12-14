using System;

namespace Parrot
{
	public interface IParrotPointer
	{
		IntPtr RawPointer { get; }
		Parrot Parrot { get; }
	}
}