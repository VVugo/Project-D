using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameManager gameManager;
    public Material myShader;
    private bool isFocus;

    private float zMovement;
    private float xMovement;
    private float totalMovement = 0;

    private void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void LateUpdate() {
        if(Move() && !isFocus)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0,50);
        }
        else if (!isFocus)
        {
            gameObject.GetComponent<Renderer>().material.color = myShader.color;
        }
    }

    private void OnMouseEnter() {
        var getPlayerPosition = gameManager.myPlayer.transform.position - new Vector3(0,1,0);

        if(Move() && getPlayerPosition != this.gameObject.transform.position)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(105, 103, 0, 1);
            isFocus = true;
        }
    }

    private void OnMouseExit() {
        gameObject.GetComponent<Renderer>().material.color = myShader.color;
        isFocus = false;
    }

    private void OnMouseDown() {
        // Move the player
        if(Move())
        {
            // Soustrait le nombre de déplacement autorisé
            gameManager.myPlayer.ActionMovementAllow -= totalMovement;
            gameManager.myPlayer.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
            gameManager.myPlayer.updateTextActionMovement();
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
}
