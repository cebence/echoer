/// Author: https://github.com/cebence
/// License: MIT

using System;

namespace Echoer {
  /// <summary>
  /// A command to be executed by the program (common interface).
  /// </summary>
  public interface Command {
    void Execute();
  }
}
