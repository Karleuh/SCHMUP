using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
		
	// Initialisation du Game Manager

        InvokeRepeating("SpawnEnemyAtRandom", 2.0f, spawnTimer);
        Instantiate(player, playerSpawnPoint);
       
    }

    private int k;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Transform topSpawnPoint;

    [SerializeField]
    private Transform botSpawnPoint;
    [SerializeField]
    private float spawnTimer;

    private void SpawnEnemyAtRandom() {

        Vector3 randomPosition = topSpawnPoint.position;
        randomPosition.y =  Random.Range(topSpawnPoint.position.y, botSpawnPoint.position.y);

        Instantiate(enemy, randomPosition, enemy.transform.rotation).GetComponent<EnemyAvatar>().GetGood(k);


        k++;
    }

    public void StopLevel(){
        CancelInvoke();
    }

}
