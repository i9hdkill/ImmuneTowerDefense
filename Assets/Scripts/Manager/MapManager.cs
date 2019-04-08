using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public static MapManager Instance { get; private set; }
    private Coord startCoord;
    private Coord endCoord;
    [SerializeField]
    private Map map;
    private Dictionary<Coord, Tile> tileByCoord { get; set; }
    private Stack<Tile> path;

    public Stack<Tile> WalkablePath {
        get {
            if (path == null) {
                CreatePath();
            }
            return new Stack<Tile>(path);
        }
    }

    public Coord StartCoord {
        get {
            return startCoord;
        }
    }

    [SerializeField]
    private GameObject mapParent;
    private int[,] predefinedMap;
    [SerializeField]
    private GameObject[] tileTypes;

    public Tile GetTileByCoord(Coord coord) {
        return tileByCoord[coord];
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() {
        startCoord = map.StartCoord;
        endCoord = map.EndCoord;
        predefinedMap = map.PredefinedMap;
        CreateMap();
    }

    private void CreateMap() {
        tileByCoord = new Dictionary<Coord, Tile>();
        for (int x = 0; x < predefinedMap.GetLength(0); x++) {
            for (int y = 0; y < predefinedMap.GetLength(1); y++) {
                PlaceTile(x, y, tileTypes[predefinedMap[x, y]]);
            }
        }
    }

    private void CreatePath() {
        Tile currentTile = tileByCoord[startCoord];
        List<Tile> alreadyAdded = new List<Tile>();
        Stack<Tile> tempPath = new Stack<Tile>();
        tempPath.Push(currentTile);
        alreadyAdded.Add(currentTile);
        int failTimer = 0;

        while (currentTile != tileByCoord[endCoord]) {
            failTimer++;
            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    Coord neighbourPos = new Coord(currentTile.Position.X - x, currentTile.Position.Y - y);

                    if (IsInMap(neighbourPos) && tileByCoord[neighbourPos].IsWalkable && !neighbourPos.Equals(startCoord) && !neighbourPos.Equals(currentTile.Position) && IsVerticalOrHorizontal(x,y) && !alreadyAdded.Contains(tileByCoord[new Coord(neighbourPos.X, neighbourPos.Y)])) {

                        Tile neighbour = tileByCoord[new Coord(neighbourPos.X, neighbourPos.Y)];
                        alreadyAdded.Add(neighbour);
                        tempPath.Push(neighbour);
                        currentTile = neighbour;
                    }
                }
            }
            if (failTimer > 200) {
                Debug.Log("PATH GENERATION FAILED. Check Tiles + Start + End");
                break;
            }
        }
        path = tempPath;
    }

    private bool IsInMap(Coord position) {
        return position.X >= 0 && position.Y >= 0 && position.X < predefinedMap.GetLength(0) && position.Y < predefinedMap.GetLength(1);
    }

    private bool IsVerticalOrHorizontal(int x, int y){
        return (Math.Abs(x - y) % 2 == 1);
    }

    private void PlaceTile(int x, int y, GameObject tile) {
        Tile temp = Instantiate(tile).GetComponent<Tile>();
        temp.Setup(new Coord(x, y), new Vector3(x, 1, y));
        temp.transform.parent = mapParent.transform;
        tileByCoord.Add(new Coord(x, y), temp);
    }
}

