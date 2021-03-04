using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TilesPrefabDirectory))]
public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [Header("Tile Generation")]
    [SerializeField] float tileGap = 0.02f; //The gap offset between tiles.
    [SerializeField] int tilesPerRow = 4;

    [Header("Tile Swapping")]

    //Classes and components
    TilesPrefabDirectory directory;
    BoardStatus status;

    #region MonoBehavior
    private void Awake()
    {
        Instance = this;

        //Classes and components
        directory = GetComponent<TilesPrefabDirectory>();
        directory.Initialize();
        status = new BoardStatus(directory.GetTileSize, tileGap, tilesPerRow);

        //Initialize
        status.tiles = BoardBuilder.CreateBoard(status, directory, transform);
    }

    void Update()
    {
        status.UpdateHoveringTileIndex();
        CheckForMouseInputs();
    }
    #endregion

    #region Tile interactions
    void CheckForMouseInputs () 
    {
        //Tile selection
        if (ClickedLMB && !HasSelectedTile && status.isMouseOnBoard)
        {
            if (status.TrySetHoveringTileAsActive())
            {
                status.selectedTile.ActiveStateEnter();
            }
        }

        //Tile dragging
        if (ClickAndDragLMB && HasSelectedTile)
        {
            //Make selected tile follow cursor
            status.selectedTile.ActiveStateUpdate();

            //When entering new tile space, swap tile index and positions between the 2 tiles

        }

        //Tile deselection
        if (ClickReleasedLMB && HasSelectedTile)
        {
            status.selectedTile.ActiveStateRelease();
            status.selectedTile.DirectLerpMoveTo(Tile.IndexToWorldPoint(status.selectedTile.TileIndex, 
                status.startPoint, status.cellSize));

            status.selectedTile = null;
        }
    }
    #endregion

    #region Feedback

    #endregion


    #region (old) Tile swapping - Click

    #endregion

    #region Helpers
    bool ClickedLMB => Input.GetMouseButtonDown(0);
    bool ClickReleasedLMB => Input.GetMouseButtonUp(0);
    bool ClickAndDragLMB => Input.GetMouseButton(0);
    bool HasSelectedTile => status.HasSelectedTile;
    #endregion

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 900, 20), "hoverIndex: "     + status.hoverIndex);
        GUI.Label(new Rect(20, 40, 900, 20), "isMouseOnBoard: " + status.isMouseOnBoard);
        GUI.Label(new Rect(20, 60, 900, 20), "IsInAnimation: "  + status.IsInAnimation);

        GUI.Label(new Rect(20, 100, 900, 20), "StartingPos: "        + status.startPoint);
        GUI.Label(new Rect(20, 120, 900, 20), "HasSelectedTile: "   + status.HasSelectedTile);
        if (status.HasSelectedTile)
        GUI.Label(new Rect(20, 140, 900, 20), "SelectedTileIndex: " + status.SelectedTileIndex);
        GUI.Label(new Rect(20, 160, 900, 20), "offsettedMousePos: " + status.offsettedMousePos);

        GUI.Label(new Rect(20, 200, 900, 20), "mouseWorldPos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        GUI.Label(new Rect(20, 220, 900, 20), "up: "    + status.up);
        GUI.Label(new Rect(20, 240, 900, 20), "down: "  + status.down);
        GUI.Label(new Rect(20, 260, 900, 20), "left: "  + status.left);
        GUI.Label(new Rect(20, 280, 900, 20), "right: " + status.right);
    }
}