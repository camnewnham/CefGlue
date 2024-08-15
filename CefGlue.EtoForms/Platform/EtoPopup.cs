using Eto.Forms;
using System;
using System.Windows;
using Xilium.CefGlue.Common.Platform;

namespace Xilium.CefGlue.EtoForms.Platform
{
    /// <summary>
    /// The Eto popup wrapper.
    /// </summary>
    internal class EtoPopup : EtoOffScreenControlHost, IOffScreenPopupHost
    {
        public Form _window;
        public EtoPopup(Form window, Drawable drawable) : base(drawable)
        {
            _window = window;
        }

        public static EtoPopup CreatePopupWindow()
        {
            Drawable drawable = new Drawable();

            var form = new Form()
            {
                Content = drawable
            };
            return new EtoPopup(form, drawable);
        }


        public int Width => (int)_window.Width;

        public int Height => (int)_window.Height;

        public int OffsetX => (int)_window.Location.X;

        public int OffsetY => (int)_window.Location.Y;

        public void MoveAndResize(int x, int y, int width, int height)
        {
            Application.Instance.InvokeAsync(() =>
            {
                _window.Location = new Eto.Drawing.Point(x, y);
                _window.Width = width;
                _window.Height = height;
            });
        }

        public void Open()
        {
            SetIsOpen(true);
        }

        public void Close()
        {
            SetIsOpen(false);
        }

        private void SetIsOpen(bool isOpen)
        {
            Application.Instance.AsyncInvoke(() =>
            {
                if (isOpen)
                {
                    _window.Show();
                }
                else
                {
                    _window.Close();
                }
            });
        }

        protected override void SetContent(ImageView image)
        {
            base._control.Content = image;
        }
    }
}
