using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    // Drag your cursor texture into this field from the Inspector
    public Texture2D customCursor;

    void Start()
    {
        // Set the hotspot to be the center of the cursor texture
        Vector2 cursorHotspot = new Vector2(customCursor.width / 2, customCursor.height / 2);
        
        // Set the custom cursor with the specified hotspot and cursor mode
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }

    // Optional: Reset cursor to default when object is destroyed
    private void OnDestroy()
    {
        // Reset to the default system cursor
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}