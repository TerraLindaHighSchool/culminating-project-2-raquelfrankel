using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private float startDelay = 6;
    private float repeatRate = 3.5f;
    private PlayerController playerControllerScript;
    private int level = 1;
    public TextMeshProUGUI debugText;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);

        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void SpawnObstacle ()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }

        if (playerControllerScript.score > level * 5)
        {
            CancelInvoke("SpawnObstacle");
            InvokeRepeating("SpawnObstacle", startDelay, repeatRate-1.5f);
            level++;

        }

        if (repeatRate < 1)
        {
            gameOver = true;
            
        }
    }
}

