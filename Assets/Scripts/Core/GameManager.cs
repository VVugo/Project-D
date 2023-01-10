using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text textTurn;
    public Player myPlayer;
    public int round = 0;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.FindObjectOfType<Player>();
        updateTextTurn();
    }
    
    public void NextTurn()
    {
        round += 1;
        updateTextTurn();
        myPlayer.ActionMovementAllow = myPlayer.InitActionMovement;
        myPlayer.updateTextActionMovement();
    }

    private void updateTextTurn(){
        textTurn.text = "Turn : " + round.ToString();
    }
}
