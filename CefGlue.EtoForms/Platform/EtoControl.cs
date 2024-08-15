using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Windows;
using Xilium.CefGlue.Common.Helpers;
using Xilium.CefGlue.Common.Platform;

namespace Xilium.CefGlue.EtoForms.Platform
{
    /// <summary>
    /// The Eto control wrapper.
    /// </summary>
    internal class EtoControl : IControl
    {
        protected readonly Drawable _control;
        private NativeControlHost _nativeControl;
        
        public event Action GotFocus;
        public event Action<CefSize> SizeChanged;

        public EtoControl(Drawable control)
        {
            _control = control;

            control.GotFocus += OnGotFocus;
            control.SizeChanged += OnLayoutUpdated;

            //control.LayoutUpdated += OnLayoutUpdated;
        }

        private void OnGotFocus(object sender, EventArgs e)
        {
            GotFocus?.Invoke();
        }

        
        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            if (_control.Loaded || (_control.Width > 0 && _control.Height > 0))
            {
                // fire as soon as the control becomes loaded or the render size is not empty
                SizeChanged?.Invoke(new CefSize((int)_control.Width, (int)_control.Height));
            }
        }

        
        public IntPtr? GetHostViewHandle(int initialWidth, int initialHeight)
        {
            var window = _control.ParentWindow;
            if (window != null)
            {
                return _control.ParentWindow.NativeHandle;
            }

            return null;
        }

        public virtual void SetTooltip(string text)
        {
        }

        ContextMenu _contextMenu;
        public void OpenContextMenu(IEnumerable<MenuEntry> menuEntries, int x, int y, CefRunContextMenuCallback callback)
        {

            Application.Instance.AsyncInvoke(
                new Action(() =>
                {
                    _contextMenu = new ContextMenu();
                    foreach (var menuEntry in menuEntries)
                    {
                        MenuItem item;
                        if (menuEntry.IsSeparator)
                        {
                            item = new SeparatorMenuItem();
                        }
                        else
                        {
                            item = menuEntry.IsChecked.HasValue ?
                            new CheckMenuItem()
                            {
                                Text = menuEntry.Label,
                                Enabled = menuEntry.IsEnabled,
                                Checked = menuEntry.IsChecked ?? false,
                                //IsCheckable = menuEntry.IsChecked != null,
                            } : new ButtonMenuItem()
                            {
                                Text = menuEntry.Label,
                                Enabled = menuEntry.IsEnabled
                            };
                            var commandId = menuEntry.CommandId;
                            item.Click += delegate { callback.Continue(commandId, CefEventFlags.None); };
                        }
                        _contextMenu.Items.Add(item);
                    }

                    _contextMenu.Closed += delegate {
                        callback.Cancel();
                        _contextMenu = null;
                    };

                    _contextMenu.Show(_control, new Eto.Drawing.PointF(x, y));
                })
            );
        }

        public void CloseContextMenu()
        {
            Application.Instance.AsyncInvoke(() =>
            {
                _contextMenu?.Dispose();
                _contextMenu = null;
            });
        }

        public virtual bool SetCursor(IntPtr cursorHandle, CefCursorType cursorType)
        {
            return false;
        }

        public void InitializeRender(IntPtr browserHandle)
        {
            Application.Instance.AsyncInvoke(() =>
            {
                _nativeControl = new NativeControlHost(browserHandle);
                ((Panel)_control).Content = _nativeControl;
            });
        }

        public void DestroyRender()
        {
            Application.Instance.Invoke(() =>
            {
                _nativeControl?.Dispose();
            });
        }
    }
}
