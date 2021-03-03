using System.Collections;
using UnityEngine;

public enum TileTypes { Up, Down, Left, Right}

public class Tile : MonoBehaviour
{
    private static Vector2Int[] AdjacentDir = new Vector2Int[] {
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 0)};
    private static Tile previousSelected = null;

    [SerializeField] private GameObject selectionBorder;
    [SerializeField] private TileTypes type;

    private bool imCurrentlySelected;
    private Vector2 tileIndex;

    public TileTypes Type => type;

    public void SetTileIndex (Vector2Int tileIndex)
    {
        this.tileIndex = tileIndex;
    }

    public  void Select ()
    {
        imCurrentlySelected = true;
        selectionBorder.SetActive(true);
        previousSelected = this;
        //SFXManager.instance.PlaySFX(Clip.Select);
    }

    public void Deselect()
    {
        imCurrentlySelected = false;
        selectionBorder.SetActive(false);
        previousSelected = null;
    }

    private void OnMouseDowndfdf()
    {
        if (BoardManager.Instance.IsComboShifting)
            return;

        if (imCurrentlySelected) 
        {
            Deselect();
        }
        else if (!imCurrentlySelected && previousSelected == null)
        {
            Select();
        }
        else if (!imCurrentlySelected && previousSelected != null)
        {
            BoardManager.Instance.SwapTiles(this, previousSelected);
            previousSelected.Deselect();
        }
    }
}

//https://www.raywenderlich.com/673-how-to-make-a-match-3-game-in-unity