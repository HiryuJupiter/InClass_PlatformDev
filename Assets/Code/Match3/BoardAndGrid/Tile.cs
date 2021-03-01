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

    private bool isSelected;
    private Vector2 tileIndex;

    public SpriteRenderer SprRenderer => sr;
    public TileTypes Type => type;

    public void InitialSpawn (Vector2Int tileIndex)
    {
        this.tileIndex = tileIndex;
    }

    private void Select ()
    {
        isSelected = true;
        selectionBorder.SetActive(true);
        previousSelected = this;
        //SFXManager.instance.PlaySFX(Clip.Select);
    }

    private void Deselect()
    {
        isSelected = false;
        selectionBorder.SetActive(false);
        previousSelected = null;
    }

    private void SwapTiles (Tile target)
    {
        if (this == target)
        {
            Debug.Log("This shouldn't happen");
            return;
        }
        StartCoroutine(DoSwapTiles(target));
    }

    IEnumerator DoSwapTiles (Tile target)
    {
        if (target == null)
            Debug.Log("null ref");

        for (float t = 0; t < 1f; t+= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, t);
            yield return null;
        }

        yield return null;
    }

    private void OnMouseDown()
    {
        if (BoardManager.Instance.IsShifting)
            return;

        if (isSelected)
        {
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            {
                Select();
            }
            else
            {
                Debug.Log("Deselect");
                previousSelected.Deselect();
                if (previousSelected != this)
                {
                    SwapTiles(previousSelected);
                    previousSelected.SwapTiles(this);
                }
            }
        }
    }
}

//https://www.raywenderlich.com/673-how-to-make-a-match-3-game-in-unity