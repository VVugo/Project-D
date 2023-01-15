using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameManager gameManager;
    public Material myShader;
    private bool isNodeFocus;

    private float zMovement;
    private float xMovement;
    private float totalMovement = 0;

    private void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void LateUpdate() {
        if(Move() && !isNodeFocus && !this.gameManager.myPlayer.canAttack)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0,50);
        }
        else if(Attack() && this.gameManager.myPlayer.canAttack && !isNodeFocus)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0,0,1,50);
        }
        else if (!isNodeFocus)
        {
            gameObject.GetComponent<Renderer>().material.color = myShader.color;
        }
    }

    private void OnMouseEnter() {
        var getPlayerPosition = gameManager.myPlayer.transform.position - new Vector3(0,1,0);

        if(Move() && getPlayerPosition != this.gameObject.transform.position && !this.gameManager.myPlayer.canAttack)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(105, 103, 0, 1);
            isNodeFocus = true;
        }
        else if(this.gameManager.myPlayer.canAttack)
        {
            isNodeFocus = true;
            Debug.Log("Player can attack");
        }
    }

    private void OnMouseExit() {
        gameObject.GetComponent<Renderer>().material.color = myShader.color;
        isNodeFocus = false;
    }

    private void OnMouseDown() {
        // Move the player
        if(Move() && !this.gameManager.myPlayer.canAttack)
        {   
            // Ajoute le Node du joueur dans le tableau de Node
            gameManager.listOfNode.Add(gameManager.myPlayer.transform.position);

            // Soustrait le nombre de déplacement autorisé
            gameManager.myPlayer.ActionMovementAllow -= totalMovement;
            gameManager.myPlayer.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
            gameManager.myPlayer.updateTextActionMovement();
            gameManager.updateListOfNodes(gameManager.myPlayer.transform.position);
        }
        else if(this.gameManager.myPlayer.canAttack)
        {
            Debug.Log("Player attack !");
        }
    }

    private bool Move() {
        zMovement = Mathf.Abs(this.gameObject.transform.position.z - gameManager.myPlayer.transform.position.z);
        xMovement = Mathf.Abs(this.gameObject.transform.position.x - gameManager.myPlayer.transform.position.x);

        totalMovement = zMovement + xMovement;
        // Vérifie si l'utilisateur peut se déplacer        
        if(totalMovement <= this.gameManager.myPlayer.ActionMovementAllow)
        {
            return true;
        }
        else{
            return false;
        }
    }

    private bool Attack()
    {
        zMovement = Mathf.Abs(this.gameObject.transform.position.z - gameManager.myPlayer.transform.position.z);
        xMovement = Mathf.Abs(this.gameObject.transform.position.x - gameManager.myPlayer.transform.position.x);

        totalMovement = zMovement + xMovement;
        // Vérifie si l'utilisateur peut se déplacer        
        if(totalMovement <= 1)
        {
            return true;
        }
        else{
            return false;
        }
    }

}
