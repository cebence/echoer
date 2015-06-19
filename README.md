# Echoer utility

[![Build status](https://ci.appveyor.com/api/projects/status/ha27gy4l3m56xgn9?svg=true)](https://ci.appveyor.com/project/cebence/echoer)

A simple command-line utility that can be used to test console-redirection and/or parsing logic.

The tool can echo a message to the `stdout` or `stderr` console stream, exit with a specific exit code, and simulate long running processing.

The tool will exit with a `0` exit code unless specified otherwise.

## Command-line arguments

- `-out <text>` - Echo the text to stdout.
- `-err <text>` - Echo the text to stderr.
- `-wait <integer>` - Wait for the specified number of seconds.
- `-exit <integer>` - Exit with the specified exit code.
- `--help` - Displays how the tool is supposed to be used.
- `--debug` - Only prints out the commands as they would be executed, doesn't actually run them.

A command can be repeated any number of times, in any order to other commands.
Commands are executed in the order they are specified.

**Note:** Since the `exit` command really exits the application all commands specified after it will not be executed.

## Usage examples

The `help` option overrides all other arguments, i.e. the following command will not do any processing:

```
echoer -out 3 -wait 1 -out 2 -wait 1 --help
```

The following command will print `3`, `2`, `1` to *stdout*, and `GO!` to *stderr*, and wait one second between prints:

```
echoer -out 3 -wait 1 -out 2 -wait 1 -out 1 -wait 1 -err GO!
```

The same command, when `debug` switch is ON, will produce the following output:

```
> echoer -out 3 -wait 1 -out 2 -wait 1 -out 1 -wait 1 -err GO!

Print '3' to stdout.
Wait for 00:00:01.
Print '2' to stdout.
Wait for 00:00:01.
Print '1' to stdout.
Wait for 00:00:01.
Print 'GO!' to stderr.
```

This command will print `Working ...`, wait 15 seconds, and exit with error `5` thus simulating a failed long running process:

```
echoer -out "Working ..." -wait 15 -exit 5
```

## License
This project is licensed under the [MIT license](LICENSE) so feel free to use it and/or contribute.

If the tool is to be used in a strong-name environment feel free to sign it with the appropriate `.snk` file.

## TODOs
- [ ] Make it a [NuGet](https://www.nuget.org/) (or [Chocolatey](https://chocolatey.org/)?) package and publish it.
