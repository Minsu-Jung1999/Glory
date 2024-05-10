using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterRayController : MonoBehaviour
{
    [SerializeField]
    float rayDistance = 10f;
    [SerializeField]
    Camera camera;

    [SerializeField]
    GameObject questInfo;

    private MC_Interaction mc_Interaction;
    private MainCharacterMovement mc_movement;

    private void Start()
    {
        mc_Interaction = gameObject.GetComponent<MC_Interaction>();
        mc_movement = GetComponent<MainCharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        LayDetector();
    }

    private void LayDetector()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("NPC"))
            {
                GameManager.instance.canTalk = true;
                GameObject npcOB = hit.collider.gameObject;
                mc_Interaction.SetCommuicateObject(npcOB);
                GameManager.instance.InteractionState(true);
                NPCManager npcManager = npcOB.GetComponent<NPCManager>();
                npcManager.QuestInformation();
                QuestUIManager questui = questInfo.GetComponent<QuestUIManager>();
                questui.QuestUISetting(npcManager.QuestInformation());
            }
            else
            {
                GameManager.instance.canTalk = false;
                GameManager.instance.InteractionState(false);
                mc_Interaction.SetCommuicateObject(null);
            }
            if (hit.collider.CompareTag("Enemy") && hit.collider is CapsuleCollider)
            {
                mc_movement.SetTarget(hit.transform.gameObject);
            }
            else
            {
                mc_movement.SetTarget(null);
            }
        }
        else
        {
            GameManager.instance.canTalk = false;
            GameManager.instance.InteractionState(false);
            mc_Interaction.SetCommuicateObject(null);
            mc_movement.SetTarget(null);
        }
        

    }
}
