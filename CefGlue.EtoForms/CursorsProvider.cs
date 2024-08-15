using Eto.Forms;
using System.Collections.Generic;

namespace Xilium.CefGlue.EtoForms
{
    /// <summary>
    /// Provides handles for mouse cursors.
    /// </summary>
    internal class CursorsProvider
    {
        private static readonly IDictionary<CefCursorType, CursorType> _cefCursorTypeToEtoMap = new Dictionary<CefCursorType, CursorType>() {
            { CefCursorType.Pointer, CursorType.Arrow },
            { CefCursorType.Cross, CursorType.Crosshair },
            { CefCursorType.Hand, CursorType.Pointer },
            { CefCursorType.IBeam, CursorType.IBeam },
            { CefCursorType.EastResize, CursorType.SizeRight },
            { CefCursorType.NorthResize, CursorType.SizeTop },
            { CefCursorType.NorthEastResize, CursorType.SizeTopRight },
            { CefCursorType.NorthWestResize, CursorType.SizeTopLeft },
            { CefCursorType.SouthResize, CursorType.SizeBottom },
            { CefCursorType.SouthEastResize, CursorType.SizeBottomRight },
            { CefCursorType.SouthWestResize, CursorType.SizeBottomLeft },
            { CefCursorType.WestResize, CursorType.SizeLeft },
            { CefCursorType.NorthSouthResize, CursorType.SizeAll },
            { CefCursorType.EastWestResize, CursorType.SizeAll }
        };

        private static readonly IDictionary<CefDragOperationsMask, CursorType> _cefDragOpsToEtoMap = new Dictionary<CefDragOperationsMask, CursorType>() {
            { CefDragOperationsMask.Copy, CursorType.Default },
            { CefDragOperationsMask.Link, CursorType.Default },
            { CefDragOperationsMask.Move, CursorType.Default }
        };

        public static Cursor GetCursorFromCefType(CefCursorType cursorType)
        {
            if (_cefCursorTypeToEtoMap.TryGetValue(cursorType, out CursorType etoCursor))
            {
                return new Cursor(etoCursor);
            }
            return Cursors.Default;
        }

        public static Cursor GetCursorFromCefType(CefDragOperationsMask ops)
        {
            if (_cefDragOpsToEtoMap.TryGetValue(ops, out CursorType etoCursor))
            {
                return new Cursor(etoCursor);
            }
            return new Cursor(CursorType.Default);
        }
    }
}
