using System.Collections;
using UnityEngine;

public enum TileTypes { None, Red, Blue, Green, Yellow, Purple, Cyan, White, Black }


public class Tile : MonoBehaviour
{
    private static Vector2Int[] AdjacentDir = new Vector2Int[] {
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 0)};
    private static Tile previousSelected = null;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject selectionBorder;
    [SerializeField] private TileTypes type;

    private bool imCurrentlySelected;
    private Vector2 tileIndex;

    public SpriteRenderer SprRenderer => sr;
    public TileTypes Type => type;

    public void SetTileIndex (Vector2Int tileIndex)
    {
        this.tileIndex = tileIndex;
    }

    private void Select ()
    {
        imCurrentlySelected = true;
        selectionBorder.SetActive(true);
        previousSelected = this;
        //SFXManager.instance.PlaySFX(Clip.Select);
    }

    private void Deselect()
    {
        imCurrentlySelected = false;
        selectionBorder.SetActive(false);
        previousSelected = null;
    }

    private void OnMouseDown()
    {
        if (BoardManager.Instance.IsComboShifting)
            return;

        if (imCurrentlySelected) 
        {
            Deselect();
        }
        else if (!imCurrentlySelected && aTileIsAlreadySelected)
        {
            Select();
        }
        else if (!imCurrentlySelected && !aTileIsAlreadySelected)
        {
            BoardManager.Instance.SwapTiles(this, previousSelected);
            previousSelected.Deselect();
        }
    }

    bool aTileIsAlreadySelected => previousSelected != null;
}

//https://www.raywenderlich.com/673-how-to-make-a-match-3-game-in-unity