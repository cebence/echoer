/// Author: https://github.com/cebence
/// License: MIT

using System;

namespace Echoer {
  /// <summary>
  /// A command that will exit the application with the specified exit code.
  /// </summary>
  public class ExitCommand : Command {
    private int exitCode;

    /// <summary>
    /// Initializes a new instance of <see cref="ExitCommand"/> with the
    /// specified <paramref name="exitCode"/>.
    /// </summary>
    /// <param name="exitCode">
    /// The exit code to pass to the OS when exiting the application.
    /// </param>
    public ExitCommand(int exitCode) {
      this.exitCode = exitCode;
    }

    public void Execute() {
      Environment.Exit(exitCode);
    }

    public override String ToString() {
      return String.Format("Exit with code {0}.", exitCode);
    }
  }
}
