using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    /* Hide and show the cursor, respectively */
    public static void HideCursor() { Cursor.visible = false; }
    public static void ShowCursor() { Cursor.visible = true; }
}
