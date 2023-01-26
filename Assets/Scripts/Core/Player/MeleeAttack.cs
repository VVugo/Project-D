using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    public GameManager gameManager;

    public Button buttonActionPlayer;

    public bool isClicked = false;

    //public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void buttonPlayerAttack()
    {
        isClicked = !isClicked;
        // Vérifie si l'utilisateur peut attaquer
        if(this.gameManager.myPlayer.ActionAttackAllow != 0 && isClicked)
        {
            this.gameManager.myPlayer.canAttack = true;
        }
        else{
            this.gameManager.myPlayer.canAttack = false;
        }
    }


    public void playerAttack()
    {
        this.gameManager.myPlayer.ActionAttackAllow--;
        buttonActionPlayer.interactable = false;
        this.gameManager.myPlayer.canAttack = false;
    }

    public void updateMeleeAttack() {
        this.gameManager.myPlayer.ActionAttackAllow = this.gameManager.myPlayer.InitActionAttack;
        buttonActionPlayer.interactable = true;
    }

}
