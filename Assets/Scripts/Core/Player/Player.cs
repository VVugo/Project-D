using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text textActionMovement;
    public int InitActionMovement = 3;
    public int InitActionAttack = 1;

    public float ActionMovementAllow;
    public float ActionAttackAllow;
    public bool canMove = true;
    public bool canAttack = false;

    private void Start() {
        ActionMovementAllow = InitActionMovement;
        ActionAttackAllow = InitActionAttack;
        
        updateTextActionMovement();
    }

    public void updateTextActionMovement()
    {
        textActionMovement.text = ActionMovementAllow.ToString();
    }
}
