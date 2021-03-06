/// Author: https://github.com/cebence
/// License: MIT

using System;

namespace Echoer {
  /// <summary>
  /// A command that will print out a string to console's <c>stdout</c>
  /// or <c>stderr</c> stream as configured.
  /// </summary>
  public class PrintCommand : Command {
    private String text;
    private Boolean toStdOut;

    /// <summary>
    /// Initializes a new instance of <see cref="PrintCommand"/> with the
    /// specified <paramref name="text"/> and whether to print it out
    /// to <c>stdout</c> or <c>stderr</c> stream.
    /// </summary>
    /// <param name="text">
    /// Text to print out to the console.
    /// </param>
    /// <param name="toStdOut">
    /// If <see langword="true"/> text will be printed to <c>stdout</c>,
    /// otherwise it will be printed to <c>stderr</c>.
    /// Default is <see langword="true"/>.
    /// </param>
    public PrintCommand(String text, Boolean toStdOut = true) {
      this.text = text;
      this.toStdOut = toStdOut;
    }

    public void Execute() {
      if (toStdOut) {
        Console.WriteLine(text);
      }
      else {
        PrintToError(text);
      }
    }

    public override String ToString() {
      return String.Format("Print '{0}' to {1}.",
          text, toStdOut ? "stdout" : "stderr");
    }

    /// <summary>
    /// Prints out the <paramref name="text"> to <c>stderr</c> in bright red.
    /// </summary>
    /// <param name="text">
    /// Text to print out to <c>stderr</c>.
    /// </param>
    public static void PrintToError(String text) {
      ConsoleColor savedColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Red;

      Console.Error.WriteLine(text);

      Console.ForegroundColor = savedColor;
    }
  }
}
