using System.Reactive.Linq;
using System.Timers;

namespace TetrisKurs.Base
{
    public static class TimerExtensions
    {
        public static IObservable<ElapsedEventArgs> ElapsedAsObservable(this System.Timers.Timer self)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            return Observable.FromEvent<ElapsedEventHandler, ElapsedEventArgs>
            (
                h => (s, e) => h(e),
                h => self.Elapsed += h,
                h => self.Elapsed -= h
            );
        }
    }
}