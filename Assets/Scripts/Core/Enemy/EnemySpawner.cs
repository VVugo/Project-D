using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] listOfPrefabEnemy;
    private GameManager gameManager;
    private int xMapLimitation;
    private int yMapLimitation;
    private int lastRound = 0;
    private bool enemyIsSpawn = false; 
    // Start is called before the first frame update
    void Start()
    {
        // Défini la limitation de la carte
        xMapLimitation = yMapLimitation = 4;

        gameManager = GameObject.FindObjectOfType<GameManager>();

        //SpawnAnEnemy();
    }

    private void FixedUpdate() {
        if(lastRound != gameManager.round)
        {
            SpawnAnEnemy();
            lastRound = gameManager.round;
        }
    }

    private void SpawnAnEnemy()
    {
        Vector3 newEnemyPosition;

        // Génère la position aléatoire du nouvel ennemi
        if(gameManager.listOfNode.Count > 0)
        {
            int randKeyId = UnityEngine.Random.Range(0, gameManager.listOfNode.Count);
        
            newEnemyPosition = gameManager.listOfNode[randKeyId];

            // Supprime la position du node du nouvel enemie dans la liste des vectors de node
            gameManager.updateListOfNodes(newEnemyPosition);

            // Instancie le nouvel ennemi
            int randTypeOfEnemy = UnityEngine.Random.Range(0, listOfPrefabEnemy.Length);
            Instantiate(listOfPrefabEnemy[randTypeOfEnemy], newEnemyPosition + new Vector3(0,1,0), listOfPrefabEnemy[randTypeOfEnemy].transform.rotation);
        }
        else{
            gameManager.isGameOver();
        }
    }
}
