using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolesReversedTrigger : MonoBehaviour
{
    public GameLogic gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            gameLogic.reverseRoles();
        }

    }
}
