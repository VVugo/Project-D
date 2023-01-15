using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameManager gameManager;
    public bool isClicked = false;
    //public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void playerAttack()
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
}
