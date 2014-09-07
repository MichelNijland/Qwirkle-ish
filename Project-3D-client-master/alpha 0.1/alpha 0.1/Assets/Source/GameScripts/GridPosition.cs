using UnityEngine;
public class GridPosition{

    string colorID;
    string shapeID;

    GameObject obj;


    public GridPosition(GameObject ob) {
        Debug.Log(ob);
        obj = ob;
    }

    public void ChangeState() {
        
    }
}
