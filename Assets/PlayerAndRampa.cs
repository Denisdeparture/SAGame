using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndRampa : MonoBehaviour
{

    public PlayerAnimatorManager PlayerAnimator;

    public GameObject Player;

    
    void Start()
    {
        var comp = Player.GetComponent<MovePlayerBeta>();
        if (comp is null)
        {
            throw new System.Exception("Player is not player :(");
        }
    }
}
