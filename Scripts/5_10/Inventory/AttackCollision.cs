using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    [SerializeField]
    BoxCollider attackcollision;

    StatusManager status;

    private void Awake()
    {
        attackcollision = GetComponent<BoxCollider>();
        status= GetComponentInParent<StatusManager>();
    }
    public void AttackCollisionOn()
    {
        attackcollision.enabled=true;
    }

    public void AttackCollisionOff()
    {
        attackcollision.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            attackcollision.enabled=false;
            if (other.GetComponent<StatusManager>())
            {
                if(status!=null)
                    other.GetComponent<StatusManager>().GetHit(status.GetCurrentDMG());
            }
        }
        if (other.CompareTag("Player"))
        {
            attackcollision.enabled = false;
            if (other.GetComponent<StatusManager>())
            {
                if (status != null)
                    other.GetComponent<StatusManager>().GetHit(status.GetCurrentDMG());
            }
        }
    }
}