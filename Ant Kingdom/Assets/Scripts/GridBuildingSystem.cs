using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;
    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap BuildingTilemap;
    public Tilemap TempTilemap;
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private Building tempBuilding;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    #region Unity Methods

    private void Awake() {
        current = this;
        string tilePath = @"BuildingTiles\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.Taken, Resources.Load<TileBase>(tilePath + "White Tile"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "Green Tile"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "Red Tile"));
    }

    private void Start() {

    }

    private void Update() {
        if (!tempBuilding) return;
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

        if (prevPos != cellPos) {
            tempBuilding.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                + new Vector3(.5f, .5f, 0f));
            prevPos = cellPos;
            FollowBuilding();
        }

        if (Input.GetMouseButtonDown(0)) {
            if (tempBuilding.CanBePlaced()) {
                tempBuilding.Place();
                tempBuilding = null;
            }
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            ClearArea();
            Destroy(tempBuilding.gameObject);
            tempBuilding = null;
        }
    }

    #endregion 

    #region Tilemap Management

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap) {
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int counter = 0;

        foreach (var v in area.allPositionsWithin) {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap) {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileType type) {
        for (int i = 0; i < arr.Length; i++) {
            arr[i] = tileBases[type];
        }
    }

    public void SetBuildingTilesAvailable(BoundsInt area) {
        SetTilesBlock(area, TileType.Empty, BuildingTilemap);
    }

    public void SetBuildingTilesUnavailable(BoundsInt area) {
        SetTilesBlock(area, TileType.Taken, BuildingTilemap);
    }

    #endregion

    #region Building Placement

    public void InitialiseWithBuilding(GameObject building) {
        if (tempBuilding != null) {
            Destroy(tempBuilding.gameObject);
            tempBuilding = null;
        }
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
        tempBuilding = Instantiate(building, cellPos, Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    public void ClearArea() {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }
 
    private void FollowBuilding() {
        ClearArea();
        tempBuilding.area.position = gridLayout.WorldToCell(tempBuilding.gameObject.transform.position);
        BoundsInt buildingArea = tempBuilding.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, BuildingTilemap);
        TileBase[] baseMainArray = GetTilesBlock(buildingArea, MainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++) {
            if (baseMainArray[i] != tileBases[TileType.Empty] && baseArray[i] != tileBases[TileType.Taken]) {
                tileArray[i] = tileBases[TileType.Green];
            } else {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area) {
        TileBase[] baseArray = GetTilesBlock(area, BuildingTilemap);
        TileBase[] baseMainArray = GetTilesBlock(area, MainTilemap);

        for (int i = 0; i < baseArray.Length; i++) {
            if (baseMainArray[i] == tileBases[TileType.Empty] || baseArray[i] == tileBases[TileType.Taken]) {
                return false;
            } 
        }

        return true;
    }

    public void TakeArea(BoundsInt area) {
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Taken, BuildingTilemap);
    }

    #endregion
}

public enum TileType {
    Empty,
    Taken,
    Green,
    Red
}