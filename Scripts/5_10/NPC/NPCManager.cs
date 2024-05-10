using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider)),RequireComponent(typeof(AudioSource))]
public class NPCManager : MonoBehaviour
{
    AudioSource audio;
    Animator animator;


    [SerializeField]
    Dialogue dialogue;

    [SerializeField]
    private int questLevel;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameObject.tag = "NPC";
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            animator.SetBool("Talk", false);
        }
    }

    public Dialogue.QuestsDialogue QuestInformation()
    {
        return dialogue.questDialogue[questLevel];
    }

    public void QuestSucess()
    {
        if (questLevel < dialogue.questDialogue.Length)
        {
            dialogue.questDialogue[questLevel].isComplate = true;
            questLevel++;
            print("quest Success");
            print(gameObject.name + " 's current quest level = " + questLevel);
        }
    }

    public Dialogue GetDialogues()
    {
        return dialogue;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audio.Play();
            animator.SetBool("Talk", true);
        }
    }
}
