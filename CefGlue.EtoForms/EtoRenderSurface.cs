using Eto.Drawing;
using Eto.Forms;
using System;
using System.Threading.Tasks;
using Xilium.CefGlue.Common.Helpers;

namespace Xilium.CefGlue.EtoForms
{
    /// <summary>
    /// The Eto builtin surface 
    /// </summary>
    internal class EtoRenderSurface : OffScreenRenderSurface
    {
        private Bitmap _bitmap;
        private IntPtr _destinationBuffer;

        public EtoRenderSurface(ImageView image)
        {
            View = image;
        }

        public ImageView View { get; }

        public override bool AllowsTransparency => true;
        protected override int BytesPerPixel => 4;

        protected override void CreateBitmap(int width, int height)
        {
            _bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgba);
            View.Image = _bitmap;
        }

        protected override Task ExecuteInUIThread(Action action)
        {
            return Application.Instance.InvokeAsync(() =>
            {
                action();
            });
        }

        protected override int RenderedHeight =>  _bitmap?.Height ?? 0;

        protected override int RenderedWidth => _bitmap?.Width ?? 0;


        protected override Action BeginBitmapUpdate()
        {
            var lockedBuffer = _bitmap.Lock();
            _destinationBuffer = lockedBuffer.Data;

            return () =>
            {
                _destinationBuffer = IntPtr.Zero;
                lockedBuffer.Dispose();
                View.Image = _bitmap;
                
            };
        }

        protected override void UpdateBitmap(IntPtr sourceBuffer, int sourceBufferSize, int stride, CefRectangle updateRegion)
        {
            unsafe
            {
                Buffer.MemoryCopy(sourceBuffer.ToPointer(), _destinationBuffer.ToPointer(), sourceBufferSize, sourceBufferSize);
            }
        }
    }
}
