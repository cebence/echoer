/// Author: https://github.com/cebence
/// License: MIT

using System;
using System.Threading;

namespace Echoer {
  /// <summary>
  /// A command that will wait for the specified time interval
  /// (via <see cref="Thread.Sleep"/>).
  /// </summary>
  public class SleepCommand : Command {
    public const int MINUTE = 60;
    public const int HOUR = 60 * MINUTE;
    public const int DAY = 24 * HOUR;

    /// <summary>
    /// Maximum allowed sleep interval is one day, i.e. 24 hours.
    /// </summary>
    public const int MAX_SECONDS = DAY;

    private TimeSpan interval;

    /// <summary>
    /// Initializes a new instance of <see cref="SleepCommand"/> with the
    /// specified <paramref name="span"/>.
    /// </summary>
    /// <param name="span">
    /// For how long to keep the thread asleep.
    /// </param>
    public SleepCommand(TimeSpan span) {
      this.interval = span;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SleepCommand"/> with the
    /// specified <paramref name="duration"/> in seconds.
    /// </summary>
    /// <param name="duration">
    /// For how many seconds to keep the thread asleep.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// If <paramref name="duration"/> is less than zero (going back in time),
    /// or grater than <see cref="MAX_SECONDS"/>.
    /// </param>
    public SleepCommand(int duration) {
      if (duration < 0) {
        throw new ArgumentException(String.Format(
            "{0} is an invalid waiting interval.", duration));
      }
      if (duration > MAX_SECONDS) {
        throw new ArgumentException(String.Format(
            "{0} is more than maximum waiting interval ({1}).",
            duration, MAX_SECONDS));
      }
      this.interval = TimeSpan.FromSeconds(duration);
    }

    public void Execute() {
      Thread.Sleep(interval);
   }

    public override String ToString() {
      return String.Format("Wait for {0}.", interval);
    }
  }
}
