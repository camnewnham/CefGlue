using System;
using Xilium.CefGlue.Common;
using Xilium.CefGlue.EtoForms;
using Eto.Forms;

namespace Xilium.CefGlue.Demo.EtoForms
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            CefSettings settings = new CefSettings()
            {
#if WINDOWLESS
                WindowlessRenderingEnabled = true
#else
                WindowlessRenderingEnabled = false
#endif
                ,
                LogSeverity = CefLogSeverity.Verbose
            };
            CefRuntimeLoader.Initialize(settings);

            new Eto.Forms.Application(Eto.Platform.Detect).Run(new Eto.Forms.Form()
            {
                Width = 600,
                Height = 600,
                Content = new EtoCefBrowser()
                {
                    Address = "https://www.google.com"
                }
            });

            return 0;
        }
    }
}
