using System.Collections;
using UnityEngine;

public class MouseHoveringTileIndexChecker
{
    float cellSize;
    Camera camera;
    public float up { get; private set; }
    public float down { get; private set; }
    public float left { get; private set; }
    public float right { get; private set; }


    public MouseHoveringTileIndexChecker(float tileSize, float tileGap, Vector2 startingPos)
    {
        cellSize = tileSize + tileGap;

        //Assuming that starting point is negative.
        up = -startingPos.y;
        down = startingPos.y;
        left = startingPos.x;
        right = -startingPos.x;

        camera = Camera.main;
    }

    public bool TryGetIndex (ref Vector2Int index)
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < left || mousePos.x > right ||
            mousePos.y < down || mousePos.y > up)
            return false;

        index.x = Mathf.FloorToInt(mousePos.x / cellSize);
        index.y = Mathf.FloorToInt(mousePos.y / cellSize);
        return true;
    }
}