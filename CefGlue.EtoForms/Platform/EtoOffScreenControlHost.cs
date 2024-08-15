using Eto.Drawing;
using Eto.Forms;
using System;
using System.Threading.Tasks;
using Xilium.CefGlue.Common.Helpers;
using Xilium.CefGlue.Common.Platform;

namespace Xilium.CefGlue.EtoForms.Platform
{
    /// <summary>
    /// The Eto control wrapper.
    /// </summary>
    internal class EtoOffScreenControlHost : EtoControl, IOffScreenControlHost
    {
        private ImageView _viewport;
        // TODO eto: get value from OS
        private const int MouseWheelDelta = 100;
        //private ToolTip _tooltip;
        //private DispatcherTimer _tooltipTimer;

        private Point _browserScreenLocation;

        public event Action LostFocus;
        public event Common.Platform.KeyEventHandler KeyDown;
        public event Common.Platform.KeyEventHandler KeyUp;
        public event TextInputEventHandler TextInput;
        public event Action<IOffScreenControlHost, CefMouseEvent, CefMouseButtonType, int> MouseButtonPressed;
        public event Action<CefMouseEvent, CefMouseButtonType> MouseButtonReleased;
        public event Action<CefMouseEvent> MouseLeave;
        public event Action<CefMouseEvent> MouseMoved;
        public event Action<CefMouseEvent, int, int> MouseWheelChanged;
        public event Action<CefMouseEvent, CefDragData, CefDragOperationsMask> DragEnter;
        public event Action<CefMouseEvent, CefDragOperationsMask> DragOver;
        public event Action DragLeave;
        public event Action<CefMouseEvent, CefDragOperationsMask> Drop;
        public event Action<float> ScreenInfoChanged;
        public event Action<bool> VisibilityChanged;

        public EtoOffScreenControlHost(Drawable control) : base(control)
        {
            control.CanFocus = true;
            /*
            _tooltip = new ToolTip();
            _tooltip.StaysOpen = true;
            _tooltip.Visibility = Visibility.Collapsed;
            _tooltip.Closed += OnTooltipClosed;

            _tooltipTimer = new DispatcherTimer();
            _tooltipTimer.Interval = TimeSpan.FromSeconds(0.5);
            */
            _viewport = new ImageView()
            {
                
#if DEBUG
                BackgroundColor = Colors.Aqua
#endif
            };
            control.AllowDrop = true;

            //imageView.IsVisibleChanged += OnIsVisibleChanged;
            control.LostFocus += OnLostFocus;

            control.MouseMove += OnMouseMove;
            control.MouseLeave += OnMouseLeave;
            control.MouseDown += OnMouseDown;
            control.MouseUp += OnMouseUp;
            control.MouseWheel += OnMouseWheel;

            control.DragEnter += OnDragEnter;
            control.DragOver += OnDragOver;
            control.DragLeave += OnDragLeave;
            control.DragDrop += OnDrop;

            control.Load += OnLoaded;
            control.UnLoad += OnUnloaded;

            control.KeyDown += OnKeyDown;
            control.KeyUp += OnKeyUp;

            control.TextInput += OnTextInput;
            

            SetContent(_viewport);
            RenderSurface = new EtoRenderSurface(_viewport);
        }

        public OffScreenRenderSurface RenderSurface { get; }

        private void OnTextInput(object sender, TextInputEventArgs e)
        {
            bool handled = false;
            TextInput?.Invoke(e.Text, out handled);
            e.Cancel = handled;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var handled = false;
            KeyUp?.Invoke(e.AsCefKeyEvent(true), out handled);
            e.Handled = handled;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var handled = false;
            KeyDown?.Invoke(e.AsCefKeyEvent(false), out handled);

            var key = e.Key;
            if (key == Keys.Tab  // Avoid tabbing out the web browser control
                || key == Keys.Home || key == Keys.End // Prevent keyboard navigation using home and end keys
                || key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right // Prevent keyboard navigation using arrows
            )
            {
                handled = true;
            }

            e.Handled = handled;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            Drop?.Invoke(e.AsCefMouseEvent(), e.AllowedEffects.AsCefDragOperationsMask());
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            DragLeave?.Invoke();
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            DragOver?.Invoke(e.AsCefMouseEvent(), e.AllowedEffects.AsCefDragOperationsMask());
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            DragEnter?.Invoke(e.AsCefMouseEvent(), e.GetDragData(), e.AllowedEffects.AsCefDragOperationsMask());
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            MouseWheelChanged?.Invoke(e.AsCefMouseEvent(), (int)(e.Delta.Width * MouseWheelDelta), (int)(e.Delta.Height * MouseWheelDelta));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseButtonReleased?.Invoke(e.AsCefMouseEvent(), e.Buttons.AsCefMouseButtonType());
            if (e.Buttons == MouseButtons.Primary)
            {
                _viewport.ReleaseMouseCapture();
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            MouseButtonPressed?.Invoke(this, e.AsCefMouseEvent(), e.Buttons.AsCefMouseButtonType(), 1);
            if (e.Buttons == MouseButtons.Primary)
            {
                _viewport.CaptureMouse(); // allow capturing mouse mouse when outside the webview (eg: grabbing scrollbar)
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            MouseLeave?.Invoke(e.AsCefMouseEvent());
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            MouseMoved?.Invoke(e.AsCefMouseEvent());
        }

        private void OnLostFocus(object sender, EventArgs e)
        {
            LostFocus?.Invoke();
        }

        private void OnIsVisibleChanged(object sender, EventArgs e)
        {
            VisibilityChanged?.Invoke(_viewport.Visible);
        }

        private void OnLoaded(object sender, EventArgs e)
        {

        }

        private void OnUnloaded(object sender, EventArgs e)
        {
            /*
            _tooltip.IsOpen = false;
            _tooltipTimer.Stop();
            */
        }

        public void Focus()
        {
            _viewport.Focus();
        }

        public CefPoint PointToScreen(CefPoint point, float deviceScaleFactor)
        {
            // calculate the point based on the browser stored location, 
            // since PointToScreen needs to be executed on the dispatcher thread
            // but calling Invoke at this stage can lead to dead locks
            return new CefPoint((int) (_browserScreenLocation.X + point.X * deviceScaleFactor), (int) (_browserScreenLocation.Y + point.Y * deviceScaleFactor));
        }

        public override bool SetCursor(IntPtr cursorHandle, CefCursorType cursorType)
        {
            Application.Instance.InvokeAsync(() =>
            {
                _viewport.Cursor = CursorsProvider.GetCursorFromCefType(cursorType);
            });
            return true;
        }

        public override void SetTooltip(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                UpdateTooltip(null);
            }
            else
            {
                /*
                _tooltipTimer.Tick += (sender, args) => UpdateTooltip(text);
                _tooltipTimer.Start();
                */
            }
        }

        public async Task<CefDragOperationsMask> StartDrag(CefDragData dragData, CefDragOperationsMask allowedOps, int x, int y)
        {
            /*
            var dataObject = new DataObject();

            dataObject.SetText(dragData.FragmentText ?? "", TextDataFormat.Text);
            dataObject.SetText(dragData.FragmentText ?? "", TextDataFormat.UnicodeText);
            dataObject.SetText(dragData.FragmentHtml ?? "", TextDataFormat.Html);

            var result = DragDropEffects.None;

            await _control.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() =>
                {
                    result = DragDrop.DoDragDrop(_control, dataObject, allowedOps.AsDragDropEffects());
                })
            );

            return result.AsCefDragOperationsMask();
            */
            return CefDragOperationsMask.None;
        }

        public void UpdateDragCursor(CefDragOperationsMask allowedOps)
        {
            // do nothing
        }

        private void UpdateTooltip(string text)
        {
            /*
            _control.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        _tooltip.IsOpen = false;
                    }
                    else
                    {
                        _tooltip.Placement = PlacementMode.Mouse;
                        _tooltip.Content = text;
                        _tooltip.IsOpen = true;
                        _tooltip.Visibility = Visibility.Visible;
                    }
                }));

            _tooltipTimer.Stop();
            */

        }

        private void OnTooltipClosed(object sender, EventArgs routedEventArgs)
        {/*
            _tooltip.Visibility = Visibility.Collapsed;
            _tooltip.Placement = PlacementMode.Absolute;
            */
        }

        /*
        private void OnHostWindowLocationChanged(object sender, EventArgs e)
        {
            _browserScreenLocation = GetBrowserScreenLocation();
        }
        */

        private void OnHostWindowStateChanged(object sender, EventArgs e)
        {
            var window = (Window)sender;

            switch (window.WindowState)
            {
                case WindowState.Normal:
                case WindowState.Maximized:
                    VisibilityChanged?.Invoke(_viewport.Visible);
                    break;

                case WindowState.Minimized:
                    VisibilityChanged?.Invoke(false);
                    break;
            }
        }

        protected virtual void SetContent(ImageView image)
        {
            ((Panel) _control).Content = image;
        }
    }
}
