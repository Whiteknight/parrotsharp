using System;
using ParrotSharp;

namespace ParrotSharpTest
{
	// Helper class for test tasks
	public class TestManager
	{
		private static Parrot interp = null;
		
		public static Parrot Interp
		{
			get {
				if (interp != null)
					return interp;
				else {
					interp = new Parrot();
					return interp;
				}
			}
		}
		
		public static void Reset()
		{
			interp = null;
		}		
	}
}

