using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 storePos;


    private int boardHeight = 0;
    private int boardWidth = 0;

    private Vector3 colPos;
    
    private Vector3 gridPos;

    bool hitGrid = false;
    bool checkPos = true;

    private int colom;
    private int row;
    

    Game game;

    GameObject board;

    void Start()
    {
        board = GameObject.Find("Board");
        game = GameObject.Find("Game Manager").GetComponent<Game>();
        
    }

    void OnMouseDown()
    {
        checkPos = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        storePos = gameObject.transform.position;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        transform.position = new Vector3(curPosition.x, curPosition.y, transform.position.z);

    }

    void OnMouseUp()
    {

        if (game.Grid[row, colom] == 0)
        {
            if (hitGrid)
            {
                transform.position = colPos;
                hitGrid = false;
                checkPos = false;
                game.CheckPosition(row, colom, gameObject.transform.name);
            }
            else
            {
                gameObject.transform.position = storePos;
            }
        }
        else
        {
            gameObject.transform.position = storePos;
        }

        
                
                
       
            
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.parent == board.transform)
        {
            string obName = col.transform.name;
            row = int.Parse(obName.Substring(0, 1));
            colom = int.Parse(obName.Substring(1, 1));
            
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (checkPos)
        {
            if (col.transform.parent == board.transform)
            {
                
                colPos = col.transform.position;

                hitGrid = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        
        if (col.transform.parent == board.transform)
        {
            
            colPos = storePos;
            
        }
    }

   

    

    
}