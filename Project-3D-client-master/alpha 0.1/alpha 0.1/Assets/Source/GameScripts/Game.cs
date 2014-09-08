using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    private int[,] grid;
    private GameObject[] easyBlocks;
    List<GameObject> gridPos = new List<GameObject>();

    private int gridWidth;
    private int gridHeight;

    private int count;

    private string state;
    GameObject gridOb;

    string[] color = new string[6] { "", "", "", "","","" };
    string[] shape = new string[6] { "", "", "", "","","" };

    Reposition repos = new Reposition();
    


    List<GameObject> playObjects = new List<GameObject>();

	// Use this for initialization
	void Awake() {
        createGrid();
        createObjects();
	}

    private void createGrid() 
    {
        GameObject board = new GameObject("Board");
        gridWidth = 4;
        gridHeight = 4;
        grid = new int[4,4];
        for (int y = 0; y < grid.GetLength(1); y++) {
            for (int x = 0; x < grid.GetLength(0); x++) {
                grid[y,x] = 0;
                GameObject gridBlock = Resources.Load("Prefabs/GamePrefabs/Grid")as GameObject;
                gridOb = GameObject.Instantiate(gridBlock,new Vector3(x,y,0.1f),Quaternion.identity)as GameObject;
                gridOb.name = y + "" + x;
                gridOb.transform.parent = board.transform;
                gridPos.Add(gridOb);
            }
        }
    }

    private void createObjects() {
        int count = 0;
        int height = 0;
        easyBlocks = Resources.LoadAll<GameObject>("Prefabs/GamePrefabs/EasyBlocks");
        foreach (GameObject b in easyBlocks) {
            if (count == 4) { count = 0; height++; }
            
            GameObject.Instantiate(b, new Vector3(count - gridWidth * 1.5f, height, -0.1f), new Quaternion(90,0,0,90));
            count++;
            
        }
        for (int i = 0; i < easyBlocks.Length; i++) {
            addToList(i);
        }
    }

    private void addToList(int i) {
        playObjects.Add(easyBlocks[i]);
    }

    public List<GameObject> PlayObjects {
        get { return playObjects; }
        set { playObjects = value; }
    }

    public int[,] Grid
    {
        get { return grid; }
        set { grid = value; }
    }

    public void CheckPosition(int row, int colom, string objectName, GameObject ob, GameObject gridPos, Vector3 storePos)
    {
            if (grid[row, colom] == 0)
            {
                grid[row, colom] = 1;
                checkColor(row, colom, objectName, ob, gridPos, storePos);
                checkShape(row, colom, objectName, ob, gridPos, storePos);
            }
            else
            {
                Debug.Log("Invalid move");
            }
    }

    public void PlaceObject(GameObject ob, GameObject grid) {
        if (grid.transform.childCount == 0)
        {
            ob.transform.parent = grid.transform;
        }
    }

    private void checkColor(int row, int colom, string objectName, GameObject ob, GameObject gridPos, Vector3 storePos)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            if (grid[row, i] == 1 && objectName.Contains(color[row]))
            {
                if (grid[row, colom] == 0) PlaceObject(ob, gridPos);
            }
            else if (grid[row, i] == 1 && !objectName.Contains(color[row]))
            {
                Debug.Log("Wrong Color Position DUDE");
                ob.transform.position = storePos;
            }
            else if (grid[row, i] == 0 && color[row] == "")
            {
                if (objectName.Contains("Green")) { color[row] = "Green"; }
                else if (objectName.Contains("Yellow")) { color[row] = "Yellow"; }
                else if (objectName.Contains("Purple")) { color[row] = "Purple"; }
                else if (objectName.Contains("Red")) { color[row] = "Red"; }
            }
        }
    }
    private void checkShape(int row, int colom, string objectName, GameObject ob, GameObject gridPos, Vector3 storePos)
    {
        for (int p = 0; p < grid.GetLength(1); p++)
        {
            if (grid[p,colom] == 1 && objectName.Contains(shape[colom]))
            {
                if (grid[row, colom] == 0) PlaceObject(ob, gridPos);
                
            }
            else if (grid[p,colom] == 1 && !objectName.Contains(shape[colom]))
            {
                Debug.Log("Wrong Shape Position DUDE");
                ob.transform.position = storePos;
            }
            else if (grid[p,colom] == 0 && shape[colom] == "")
            {
                if (objectName.Contains("Sphere")) { shape[colom] = "Sphere"; }
                else if (objectName.Contains("Block")) { shape[colom] = "Block"; }
                else if (objectName.Contains("Cylinder")) { shape[colom] = "Cylinder"; }
                else if (objectName.Contains("Capsule")) { shape[colom] = "Capsule"; }
            }
        }
    }
}
