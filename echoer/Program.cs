/// Author: https://github.com/cebence
/// License: MIT

using System;
using System.Collections.Generic;

namespace Echoer {
  /// <summary>
  /// Application echoes command-line parameters to stdout/stderr.
  /// </summary>
  public class Program {
    public static void Main(String[] args) {
      try {
        Program p = new Program(args);
        p.Execute();
      }
      catch (Exception e) {
        PrintCommand.PrintToError(String.Format("ERROR: {0}", e.Message));
        if (e.InnerException != null) {
          PrintCommand.PrintToError(String.Format(
              "  Caused by: {0}", e.InnerException.Message));
        }
        Environment.Exit(1);
      }
    }

    #region Command-line options
    private const String ARG_DEBUG = "--debug";
    private const String ARG_HELP = "--help";
    private const String ARG_STDOUT = "-out";
    private const String ARG_STDERR = "-err";
    private const String ARG_ENVVAR = "-env";
    private const String ARG_WAIT = "-wait";
    private const String ARG_EXIT = "-exit";
    #endregion

    private Boolean debugMode;
    private Boolean justTheHelp;
    private List<Command> commands = new List<Command>(10);

    public Program(String[] args) {
      ParseArguments(args);
    }

    public void Execute() {
      // If help is requested do only that.
      if (justTheHelp) {
        ShowUsage();
        return;
      }

      // Go through commands in order, and execute them unless we're debugging.
      // In that case just print out what the command will do.
      foreach (Command cmd in commands) {
        if (debugMode) {
          Console.WriteLine(cmd.ToString());
        }
        else {
          cmd.Execute();
        }
      }
    }

    // Goes through arguments one by one, detects options (single argument)
    // and turns them on/off, detects commands with values (two arguments
    // in succession) and places them into a list for later execution.
    private void ParseArguments(String[] args) {
      int i = 0;
      while (i < args.Length) {
        String name = args[i++];
        switch (name) {
          case ARG_HELP:
            justTheHelp = true;
            break;

          case ARG_DEBUG:
            debugMode = true;
            break;

          case ARG_STDOUT:
          case ARG_STDERR:
          case ARG_ENVVAR:
          case ARG_WAIT:
          case ARG_EXIT:
            if (i < args.Length) {
              commands.Add(CreateCommand(name, args[i++]));
            }
            break;

          default:
            throw new Exception(String.Format("Unknown argument \"{0}\".", name));
        }
      }
    }

    /// <summary>
    /// Factory method for creating <see cref="Command"/> instances.
    /// </summary>
    public static Command CreateCommand(String name, String parameter) {
      switch (name) {
        case ARG_STDOUT:
        case ARG_STDERR:
          return new PrintCommand(parameter, ARG_STDOUT.Equals(name));

        case ARG_ENVVAR:
          return new EnvVarCommand(parameter);

        case ARG_WAIT:
          return new SleepCommand(Convert.ToInt32(parameter));

        case ARG_EXIT:
          return new ExitCommand(Convert.ToInt32(parameter));

        default:
          throw new Exception(String.Format("Unknown command \"{0}\".", name));
      }
    }

    private static void ShowUsage() {
      Console.WriteLine("Echoer - echoes command-line arguments to stdout/stderr.");
      Console.WriteLine("");
      Console.WriteLine("Usage: echoer <commands> [options]");
      Console.WriteLine("");
      Console.WriteLine("Options:");
      Console.WriteLine("  --help     Displays how the tool is supposed to be used.");
      Console.WriteLine("  --debug    Only prints out the commands as they would be executed,");
      Console.WriteLine("             doesn't actually run them.");
      Console.WriteLine("");
      Console.WriteLine("Commands (can repeat, executed in order):");
      Console.WriteLine("  -out <TEXT>         Echo the text to stdout.");
      Console.WriteLine("  -err <TEXT>         Echo the text to stderr.");
      Console.WriteLine("  -env <VAR_NAME>     Echo %VAR_NAME% to stdout (if variable is set).");
      Console.WriteLine("                      If variable is not set exit with error.");
      Console.WriteLine("  -wait <INTEGER>     Wait for the specified number of seconds.");
      Console.WriteLine("  -exit <INTEGER>     Exit with the specified exit code.");
      Console.WriteLine("");
      Console.WriteLine("Examples:");
      Console.WriteLine("- Count down to stdout, 'GO!' to stderr, with one second delay:");
      Console.WriteLine("  echoer -out 3 -wait 1 -out 2 -wait 1 -out 1 -wait 1 -err GO!");
      Console.WriteLine();
      Console.WriteLine("- Print the message, wait one minute, and exit with error 5:");
      Console.WriteLine("  echoer -out \"Working ...\" -wait 60 -exit 5");
      Console.WriteLine();
      Console.WriteLine("- Exit command should go last, otherwise nothing is executed:");
      Console.WriteLine("  echoer -exit 1 -out Ignored -wait 5 -out \"Also ignored\"");
    }
  }
}
