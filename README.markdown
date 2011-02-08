ParrotSharp (Or "Parrot#") is a binding library for the Parrot Virtual
Machine. Parrot is a virtual machine designed to host dynamic languages and
runtimes. This is a stark contrast to the .Net CLR, which primarily has static
languages in mind. Sometimes one way is better than the other. Sometimes both
are needed.

ParrotSharp allows you to embed libparrot in a project written in C# or other
languages which support the common language runtime. You can execute programs
written for Parrot from your C# program, or call functions and methods in
libraries written for Parrot.

How to use:

1. Download the source for ParrotSharp, and build. Currently, only Mono is
   supported.
2. Run the tests (Requires NUnit)
3. Make sure libparrot is in a location where Mono can find it. When in doubt,
   set the library path in LD_LIBRARY_PATH
4. Run it or do whatever you want.

ParrotSharp is released under the terms of the Artistic License 2.0.
