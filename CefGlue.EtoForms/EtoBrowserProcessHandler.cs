using System;
using System.Threading.Tasks;
using System.Threading;
using Xilium.CefGlue.Common.Handlers;
using Eto.Forms;

namespace Xilium.CefGlue.EtoForms
{
    internal class EtoBrowserProcessHandler : BrowserProcessHandler
    {
        private IDisposable _current;
        private object _schedule = new object();

        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            lock (_schedule)
            {
                if (_current != null)
                {
                    _current.Dispose();
                }

                if (delayMs <= 0)
                {
                    delayMs = 1;
                }

                _current = Periodic(() =>
                {
                    Application.Instance.InvokeAsync(() =>
                    {
                        CefRuntime.DoMessageLoopWork();
                    });
                }, TimeSpan.FromMilliseconds(delayMs));
            }
        }

        public static async Task Periodic(Action action, TimeSpan interval,  CancellationToken cancellationToken = default)
        {
            while (true)
            {
                Task delayTask = Task.Delay(interval, cancellationToken);
                action();
                await delayTask;
            }
        }
    }
}
