using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public PlayerInput character;

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Static") && other.CompareTag("Attack")) // recieve hit
        {
            character._animator.SetTrigger("Death");
        }
        else if (this.CompareTag("Attack") && other.CompareTag("Static")) // hit oponent
        {
            character._animator.SetTrigger("Win");
        }
    }
}
