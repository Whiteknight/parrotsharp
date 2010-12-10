using System;
using System.Text;
using System.Runtime.InteropServices;

public static class Program {
    [DllImport("parrot")]
    private static extern System.IntPtr Parrot_new(System.IntPtr parent);

    [DllImport("parrot")]
    private static extern void Parrot_destroy(System.IntPtr interp);

    [DllImport("parrot", CharSet=CharSet.Ansi)]
    private static extern System.IntPtr Parrot_new_string(System.IntPtr interp,
        string text, int length, System.IntPtr whatever, int whatever2);

    [DllImport("parrot", CharSet=CharSet.Ansi)]
    private static extern System.IntPtr Parrot_compile_string(System.IntPtr interp,
        System.IntPtr compiler_name, string code, StringBuilder errmsg);

    [DllImport("parrot", CharSet=CharSet.Ansi)]
    private static extern void Parrot_ext_call(System.IntPtr interp,
        System.IntPtr sub, string sig);

    public static void Main(string[] args) {
        Console.WriteLine("Hello World from C#");

        System.IntPtr interp = Parrot_new(System.IntPtr.Zero);
        System.IntPtr pir = Parrot_new_string(interp, "PIR", 3, System.IntPtr.Zero, 0);
        System.IntPtr sub = Parrot_compile_string(interp, pir, @"
            .sub 'foo'
                say 'Hello from PIR'
            .end",
            new StringBuilder(256)
        );

        Parrot_ext_call(interp, sub, "->");
        Parrot_destroy(interp);

        Console.WriteLine("Hello again from C#");
    }
}