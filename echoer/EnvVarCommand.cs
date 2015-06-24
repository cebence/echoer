/// Author: https://github.com/cebence
/// License: MIT

using System;

namespace Echoer {
  /// <summary>
  /// A command that will print out the value of an environment variable
  /// to console's <c>stdout</c>.
  /// </summary>
  /// <remarks>
  /// If the variable is not set an exception will be thrown that will cause
  /// the tool to print the error to <c>stderr</c> and exit with an error code.
  /// </remarks>
  public class EnvVarCommand : Command {
    private const String ENVVAR_TEMPLATE = "%{0}%";
    private const String ERROR_MESSAGE = "EnvVar '{0}' is not set.";

    private String envvarName;
    private String envvarValue;

    /// <summary>
    /// Initializes a new instance of <see cref="EnvvarCommand"/> with the
    /// specified <paramref name="envvar"/> name.
    /// </summary>
    /// <param name="envvar">
    /// Name of the variable to print out to the console.
    /// </param>
    public EnvVarCommand(String envvar) {
      this.envvarName = envvar;

      // Get the envvar's value.
      String reference = String.Format(ENVVAR_TEMPLATE, envvarName);
      envvarValue = Environment.ExpandEnvironmentVariables(reference);

      // Reference should have been replaced, if it's not set it to null.
      if (envvarValue.Equals(reference)) {
        throw new Exception(String.Format(ERROR_MESSAGE, envvarName));
      }
    }

    public void Execute() {
      Console.WriteLine(envvarValue);
    }

    public override String ToString() {
      return String.Format("EnvVar '{0}' = '{1}'.", envvarName, envvarValue);
    }
  }
}
