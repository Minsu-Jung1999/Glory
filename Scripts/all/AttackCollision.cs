using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    [SerializeField]
    BoxCollider attackcollision;

    private void Awake()
    {
        attackcollision = GetComponent<BoxCollider>();
    }
    public void AttackCollisionOn()
    {
        print("Collision On");
        attackcollision.enabled=true;
    }

    public void AttackCollisionOff()
    {
        print("Collision Off");
        attackcollision.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            attackcollision.enabled=false;
        }
    }
}