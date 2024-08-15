using System.IO;
using Eto.Forms;

namespace Xilium.CefGlue.EtoForms
{
    internal static class InputExtensions
    {
        /// <summary>
        /// Convert a mouse event args into a cef mouse event.
        /// </summary>
        /// <param name="eventArgs">The mouse event args</param>
        /// <returns></returns>
        public static CefMouseEvent AsCefMouseEvent(this MouseEventArgs eventArgs)
        {
            var cursorPos = eventArgs.Location;
            var modifiers = CefEventFlags.None;

            if (eventArgs.Buttons.HasFlag(MouseButtons.Primary))
            {
                modifiers |= CefEventFlags.LeftMouseButton;
            }
            if (eventArgs.Buttons.HasFlag(MouseButtons.Alternate))
            {
                modifiers |= CefEventFlags.RightMouseButton;
            }
            if (eventArgs.Buttons.HasFlag(MouseButtons.Middle))
            {
                modifiers |= CefEventFlags.MiddleMouseButton;
            }

            return new CefMouseEvent((int) cursorPos.X, (int) cursorPos.Y, modifiers | Keyboard.Modifiers.AsCefKeyboardModifiers());
        }

        /// <summary>
        /// Convert a mouse button into a cef mouse button.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static CefMouseButtonType AsCefMouseButtonType(this MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Middle:
                    return CefMouseButtonType.Middle;
                case MouseButtons.Alternate:
                    return CefMouseButtonType.Right;
                default:
                    return CefMouseButtonType.Left;
            }
        }

        /// <summary>
        /// Convert a key event into a cef key event.
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <param name="isKeyUp"></param>
        /// <returns></returns>
        public static CefKeyEvent AsCefKeyEvent(this KeyEventArgs eventArgs, bool isKeyUp)
        {
            var modifiers = eventArgs.Modifiers.AsCefKeyboardModifiers();
            return new CefKeyEvent()
            {
                EventType = isKeyUp ? CefKeyEventType.KeyUp : CefKeyEventType.RawKeyDown,
                WindowsKeyCode = KeyInterop.VirtualKeyFromKey(eventArgs.Key),
                NativeKeyCode = 0,
                IsSystemKey = false,
                Modifiers = modifiers
            };
        }

        /// <summary>
        /// Convert keyboard modifiers into cef flags.
        /// </summary>
        /// <param name="keyboardModifiers"></param>
        /// <returns></returns>
        public static CefEventFlags AsCefKeyboardModifiers(this Keys keyboardModifiers)
        {
            CefEventFlags modifiers = new CefEventFlags();

            if (keyboardModifiers.HasFlag(Keys.Alt))
                modifiers |= CefEventFlags.AltDown;

            if (keyboardModifiers.HasFlag(Keys.Control))
                modifiers |= CefEventFlags.ControlDown;

            if (keyboardModifiers.HasFlag(Keys.Shift))
                modifiers |= CefEventFlags.ShiftDown;

            return modifiers;
        }

        /// <summary>
        /// Convert a drag event args into a cef mouse event.
        /// </summary>
        /// <param name="eventArgs">The drag event args</param>
        /// <returns></returns>
        public static CefMouseEvent AsCefMouseEvent(this DragEventArgs eventArgs)
        {
            var cursorPos = eventArgs.Location;

            return new CefMouseEvent((int)cursorPos.X, (int)cursorPos.Y, Keyboard.Modifiers.AsCefKeyboardModifiers());
        }

        /// <summary>
        /// Converts a drag drop effects to Cef Drag Operations
        /// </summary>
        /// <param name="dragDropEffects">The drag drop effects.</param>
        /// <returns></returns>
        public static CefDragOperationsMask AsCefDragOperationsMask(this DragEffects dragDropEffects)
        {
            var operations = CefDragOperationsMask.None;

            if (dragDropEffects.HasFlag(DragEffects.All))
            {
                operations |= CefDragOperationsMask.Every;
            }
            if (dragDropEffects.HasFlag(DragEffects.Copy))
            {
                operations |= CefDragOperationsMask.Copy;
            }
            if (dragDropEffects.HasFlag(DragEffects.Move))
            {
                operations |= CefDragOperationsMask.Move;
            }
            if (dragDropEffects.HasFlag(DragEffects.Link))
            {
                operations |= CefDragOperationsMask.Link;
            }

            return operations;
        }

        /// <summary>
        /// Gets the drag effects.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns></returns>
        public static DragEffects AsDragDropEffects(this CefDragOperationsMask mask)
        {
            if (mask.HasFlag(CefDragOperationsMask.Every))
            {
                return DragEffects.Copy | DragEffects.Move | DragEffects.Link;
            }
            if (mask.HasFlag(CefDragOperationsMask.Copy))
            {
                return DragEffects.Copy;
            }
            if (mask.HasFlag(CefDragOperationsMask.Move))
            {
                return DragEffects.Move;
            }
            if (mask.HasFlag(CefDragOperationsMask.Link))
            {
                return DragEffects.Link;
            }
            return DragEffects.None;
        }

        /// <summary>
        /// Gets the drag data
        /// </summary>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public static CefDragData GetDragData(this DragEventArgs e)
        {
            var dragData = CefDragData.Create();

            if (e.Data.ContainsUris)
            {
                dragData.SetLinkURL(e.Data.Uris[0].ToString());
            }

            if (e.Data.ContainsText)
            {
                dragData.SetFragmentText(e.Data.Text);
            }

            if (e.Data.ContainsHtml)
            {
                dragData.SetFragmentHtml(e.Data.Html);
            }

            // TODO: Eto doesn't support files?

            return dragData;
        }
    }
}
