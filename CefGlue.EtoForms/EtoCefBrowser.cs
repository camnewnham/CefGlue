using System;
using Xilium.CefGlue.Common;
using Xilium.CefGlue.Common.Platform;
using Xilium.CefGlue.EtoForms.Platform;

namespace Xilium.CefGlue.EtoForms
{
    /// <summary>
    /// The Eto CEF browser.
    /// </summary>
    public class EtoCefBrowser : BaseCefBrowser
    {
        static EtoCefBrowser()
        {
            if (CefRuntime.Platform == CefRuntimePlatform.MacOS && !CefRuntimeLoader.IsLoaded)
            {
                CefRuntimeLoader.Load(new EtoBrowserProcessHandler());
            }
        }
        public EtoCefBrowser() : this(null) { }

        public EtoCefBrowser(Func<CefRequestContext> cefRequestContextFactory)
            : base(cefRequestContextFactory)
        {
            //KeyboardNavigation.SetAcceptsReturn(this, true);
        }

        internal override IControl CreateControl()
        {
            return new EtoControl(this);
        }

        internal override IOffScreenControlHost CreateOffScreenControlHost()
        {
            return new EtoOffScreenControlHost(this);
        }

        internal override IOffScreenPopupHost CreatePopupHost()
        {
            return EtoPopup.CreatePopupWindow(ParentWindow);
        }
    }
}
