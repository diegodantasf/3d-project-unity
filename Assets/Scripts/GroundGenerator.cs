using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public Transform startPoint; //Point from where ground tiles will start
    public PlatformTile tilePrefab;
    public float movingSpeed = 1;
    public int tilesToPreSpawn = 15; //How many tiles should be pre-spawned
    public int tilesWithoutObstacles = 2; //How many tiles at the beginning should not have obstacles, good for warm-up

    List<PlatformTile> spawnedTiles = new List<PlatformTile>();
    int nextTileToActivate = -1;

    [HideInInspector]
    public bool gameOver = false;
    public bool gameStarted = false;
    public float score = 0;

    public static GroundGenerator instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Vector3 spawnPosition = startPoint.position;
        for (int i = 0; i < tilesToPreSpawn; ++i) {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            PlatformTile spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as PlatformTile;
            if (i >= tilesWithoutObstacles) {
                spawnedTile.ActivateRandomObstacle();
            } else {
                spawnedTile.DeactivateAllObstacles();
            }
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object upward in world space x unit/second.
        //Increase speed the higher score we get
        if (!gameOver && gameStarted)
        {
            transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * movingSpeed, Space.World);
            score += Time.deltaTime;
        }

        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
            tileTmp.ActivateRandomObstacle();
            spawnedTiles.Add(tileTmp);
        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gameOver) {
                //Restart current scene
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            } else if (!gameStarted) {
                gameStarted = true;
            }
        }
    }

    void OnGUI() {
        if (gameOver) {
            GUI.color = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Game Over\nYour score is: " + ((int)score) + "\nPress 'Space' to restart");
        } else if (!gameStarted) {
            GUI.color = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200), "Press 'Space' to start");
        }

        GUI.color = Color.green;
        GUI.Label(new Rect(5, 5, 200, 25), "Score: " + ((int)score));
    }
}