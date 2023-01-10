using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text textActionMovement;
    public int InitActionMovement = 3;

    public float ActionMovementAllow;
    public bool canMove = true;

    private void Start() {
        ActionMovementAllow = InitActionMovement;
        updateTextActionMovement();
    }

    public void updateTextActionMovement()
    {
        textActionMovement.text = ActionMovementAllow.ToString();
    }
}
