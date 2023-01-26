using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text textTurn;
    public TMP_Text textGameOver;
    public Button nextTurn;
    public Player myPlayer;
    public int round = 0;

    public List<Vector3> listOfNode;

    public Enemy[] listOfEnemies;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Désactive le text du gameOver
        textGameOver.enabled = false;

        myPlayer = GameObject.FindObjectOfType<Player>();
        var Nodes = GameObject.FindObjectsOfType<Node>();

        foreach(var node in Nodes)
        {
            listOfNode.Add(node.transform.position);
        }

        updateListOfNodes(myPlayer.transform.position);
        updateListOfEnemies();
        updateTextTurn();
    }

    private void FixedUpdate() {
        if(gameOver)
        {
            myPlayer.ActionMovementAllow = 0;
        }
    }
    
    public void NextTurn()
    {
        updateListOfEnemies();

        round += 1;
        updateTextTurn();
        myPlayer.ActionMovementAllow = myPlayer.InitActionMovement;
        myPlayer.updateTextActionMovement();
        myPlayer.meleeAttack.updateMeleeAttack();
    }

    private void updateTextTurn(){
        textTurn.text = "Turn : " + round.ToString();
    }

    private void updateListOfEnemies() {
        // Actualise la liste des énemies présent dans le jeu
        listOfEnemies = GameObject.FindObjectsOfType<Enemy>();
    }

    public void updateListOfNodes(Vector3 newPosition) {
        for(int i = 0; i < listOfNode.Count; ++i)
        {
            // Si la nouvelle position est contenue dans le tableau des nodes, alors elle est supprimée
            if(newPosition == listOfNode[i])
            {
                listOfNode.Remove(newPosition);
            }
        }
    }

    public void isGameOver(){
        gameOver = true;
        textGameOver.enabled = true;
        nextTurn.interactable = false;
    }
}
