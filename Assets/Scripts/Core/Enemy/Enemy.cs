using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int InitActionMovement = 3;
    private int ActionMovementAllow;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        ActionMovementAllow = InitActionMovement;
    }

    public void MoveToPlayer()
    {
        
    }
}
