using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameManager gameManager;
    public Material shaderMovement;
    public Material shaderAttack;
    public Material shaderDefault;
    private bool isNodeFocus;

    private float zMovement;
    private float xMovement;
    private float totalMovement = 0;
    private bool isAttacking = false;

    private void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void LateUpdate() {
        if(Move() && !isNodeFocus && !this.gameManager.myPlayer.canAttack && this.gameManager.listOfNode.Contains(this.gameObject.transform.position))
        {
            gameObject.GetComponent<Renderer>().material = shaderMovement;
        }
        else if(Attack() && this.gameManager.myPlayer.canAttack && !isNodeFocus)
        {
            gameObject.GetComponent<Renderer>().material = shaderAttack;
        }
        else if(!isNodeFocus){
            gameObject.GetComponent<Renderer>().material = shaderDefault;
        }
    }

    private void OnMouseEnter() {
        var getPlayerPosition = gameManager.myPlayer.transform.position - new Vector3(0,1,0);

        if(Move() && getPlayerPosition != this.gameObject.transform.position && !this.gameManager.myPlayer.canAttack && this.gameManager.listOfNode.Contains(this.gameObject.transform.position))
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(105, 103, 0, 1);
            isNodeFocus = true;
        }
        else if(!isAttacking)
        {
            isNodeFocus = true;
            gameObject.GetComponent<Renderer>().material.color = new Color(50, 50, 200, 1);
            gameManager.myPlayer.anim.Play("Idle_SwordShield");
        }
    }

    private void OnMouseExit() {
        gameObject.GetComponent<Renderer>().material = shaderDefault;
        isNodeFocus = false;
    }

    private void OnMouseDown() {
        // Move the player
        if(Move() && !Attack() && this.gameManager.listOfNode.Contains(this.gameObject.transform.position) && !this.gameManager.myPlayer.canAttack)
        {   
            // Ajoute le Node du joueur dans le tableau de Node
            gameManager.listOfNode.Add(gameManager.myPlayer.transform.position - new Vector3(0,1,0));

            // Soustrait le nombre de déplacement autorisé
            gameManager.myPlayer.ActionMovementAllow -= totalMovement;
            gameManager.myPlayer.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
            gameManager.myPlayer.updateTextActionMovement();
            gameManager.updateListOfNodes(gameManager.myPlayer.transform.position - new Vector3(0,1,0));
        }
        else if(Attack())
        {
            isAttacking = true;
            // Décrémente le point d'attaque du joueur
            gameManager.myPlayer.meleeAttack.playerAttack();

            if(!gameManager.listOfNode.Contains(this.gameObject.transform.position))
            {
                foreach(Enemy enemy in gameManager.listOfEnemies)
                {
                    if(enemy.transform.position - new Vector3(0,1,0) == gameObject.transform.position)
                    {
                        gameManager.listOfNode.Add(enemy.transform.position - new Vector3(0,1,0));
                        enemy.anim.Play("Die");
                        Destroy(enemy.gameObject);
                    }
                }
            }
            gameManager.myPlayer.anim.Play("NormalAttack02_SwordShield");
            isAttacking = false;
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
        var zPos = Mathf.Abs(this.gameObject.transform.position.z - gameManager.myPlayer.transform.position.z);
        var xPos = Mathf.Abs(this.gameObject.transform.position.x - gameManager.myPlayer.transform.position.x);

        var totalPos = zPos + xPos;
        // Vérifie si l'utilisateur peut se attaquer
        if(totalPos == 1 && this.gameManager.myPlayer.canAttack)
        {
            return true;
        }
        else{
            return false;
        }
    }

}
