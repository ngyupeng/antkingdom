using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;
    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap BuildingTilemap;
    public Tilemap TempTilemap;

    // For blocking clicks on other objects in building mode
    public GameObject UIBlock;
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private Building tempBuilding;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    #region Unity Methods

    private void Awake() {
        current = this;
        tileBases = new Dictionary<TileType, TileBase>();
        string tilePath = @"BuildingTiles\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.Taken, Resources.Load<TileBase>(tilePath + "White Tile"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "Green Tile"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "Red Tile"));
    }

    private void Start() {

    }

    private bool isClicking;
    private Vector3 clickPosition;
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
            isClicking = true;
            clickPosition = Input.mousePosition;
        } 

        if (Input.GetMouseButtonUp(0)) {
            if (isClicking && clickPosition == Input.mousePosition) {
                if (tempBuilding.CanBePlaced()) {
                    tempBuilding.Place();
                    tempBuilding = null;
                    UIBlock.SetActive(false);
                }
            }
            isClicking = false;
        } 
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            ClearTempArea();
            tempBuilding.CancelPlacement();
            tempBuilding = null;
            isClicking = false;
            UIBlock.SetActive(false);
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
        SetBuilding(Instantiate(building, cellPos, Quaternion.identity).GetComponent<Building>());
        FollowBuilding();
    }

    public void SetShopItem(ShopItem item) {
        tempBuilding.shopItem = item;
    }

    // Call this for a new building instance
    public void SetBuilding(Building building) {
        tempBuilding = building;
        UIBlock.SetActive(true);
    }

    public void ClearTempArea() {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }
 
    private void FollowBuilding() {
        ClearTempArea();
        tempBuilding.area.position = gridLayout.WorldToCell(tempBuilding.gameObject.transform.position);
        BoundsInt buildingArea = tempBuilding.area;

        int size = buildingArea.size.x * buildingArea.size.y;
        TileBase[] tileArray = new TileBase[size];

        if (CanTakeArea(buildingArea)) {
            FillTiles(tileArray, TileType.Green);
        } else {
            FillTiles(tileArray, TileType.Red);
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

    
    public void ClearMainArea(BoundsInt area) {
        SetTilesBlock(area, TileType.Empty, BuildingTilemap);
    }

    #endregion
}

public enum TileType {
    Empty,
    Taken,
    Green,
    Red
}