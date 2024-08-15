using System;
using Xilium.CefGlue.Common;
using Xilium.CefGlue.EtoForms;
using Eto.Forms;
using Xilium.CefGlue.Common.Handlers;

namespace Xilium.CefGlue.Demo.EtoForms
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            CefSettings settings = new CefSettings()
            {
                UserAgent = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 CefGlue/{typeof(CefApp).Assembly.GetName().Version}",

#if WINDOWLESS
                WindowlessRenderingEnabled = true
#else
                WindowlessRenderingEnabled = false
#endif
                ,
                LogSeverity = CefLogSeverity.Verbose
            };
            CefRuntimeLoader.Initialize(settings);

            new Application(Eto.Platform.Detect).Run(new Eto.Forms.Form()
            {
                Width = 600,
                Height = 600,
                Content = new EtoCefBrowser()
                {
                    Address = "https://www.google.com",
                    LifeSpanHandler = new BrowserLifeSpanHandler(),
                }
            });

            return 0;
        }


        private class BrowserLifeSpanHandler : LifeSpanHandler
        {
            protected override bool OnBeforePopup(
                CefBrowser browser,
                CefFrame frame,
                string targetUrl,
                string targetFrameName,
                CefWindowOpenDisposition targetDisposition,
                bool userGesture,
                CefPopupFeatures popupFeatures,
                CefWindowInfo windowInfo,
                ref CefClient client,
                CefBrowserSettings settings,
                ref CefDictionaryValue extraInfo,
                ref bool noJavascriptAccess)
            {
                CefRectangle bounds = windowInfo.Bounds;
                if (bounds.Width <= 0 || bounds.Height <= 0 || bounds.X < 0 || bounds.Y < 0)
                {
                    bounds = new CefRectangle(0, 0, 600, 600);
                }

                Application.Instance.AsyncInvoke(() =>
                {
                    new Form()
                    {
                        Width = bounds.Width,
                        Height = bounds.Height,
                        Location = new Eto.Drawing.Point(bounds.X, bounds.Y),
                        Title = targetUrl,
                        Content = new EtoCefBrowser()
                        {
                            Address = targetUrl
                        }
                    }.Show();
                });
                return true;
            }
        }
    }
}
