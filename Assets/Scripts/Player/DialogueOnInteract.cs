using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yarn.Unity;

public class DialogueOnInteract : MonoBehaviour, IPlayerInteraction
{
    [SerializeField] private DialogueRunner dialogueRunner;
    private Player player;
    private PlayerMovement playerMovement;
    [SerializeField] string node;

    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.GetComponent<Player>();
        playerMovement = playerGameObject.GetComponent<PlayerMovement>();
    } 

    public void Highlight()
    {
        
    }

    public void Interact()
    {
        //Free mouse and stop cam from following mouse
        player.UnlockMouse();
        playerMovement.unlockMouse = true;

        //Run Dialogue runner
        dialogueRunner.StartDialogue(node);
    }
}
