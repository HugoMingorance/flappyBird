using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int Score = 0;
    private GameObject clSpawner;
    private GameObject[] columns;

    // Start is called before the first frame update
    void Start()
    {

     clSpawner = GameObject.FindGameObjectWithTag("clSpawner");
     columns = GameObject.FindGameObjectsWithTag("columns");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementScore(){
       Score++;
       Debug.Log(Score);
    }

    public void GameOver(){
       Score = 0;
       Debug.Log("GameOver");

       foreach (GameObject column in columns)
        {
            Destroy(column);
        }

       clSpawner.gameObject.GetComponent<ColumnSpawner>().GameOver();

    }


}
