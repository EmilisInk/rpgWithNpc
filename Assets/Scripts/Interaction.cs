using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour
{
    public Canvas Canvas;

    [Header("Interaction Settings")]
    public TextMeshProUGUI dialogueText;
    public Button[] buttons;
    public TextMeshProUGUI[] buttonTexts;
    public PlayerController player;
    private bool isDialogueOpen = false;

    Dictionary<int, string[]> nodes = new Dictionary<int, string[]>();
    //private int currentID = 0;

    private bool playerNearby = false;

    private void Start()
    {
        Canvas.gameObject.SetActive(false);
        LoadTXT();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerNearby = false;
            
            CloseDialogue();
        }
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if(!isDialogueOpen)
            {
                OpenDialogue();
            }
            else
            {
                 CloseDialogue();
            }

        }
    }

    public void OpenDialogue()
    {
        isDialogueOpen = true;

        Canvas.gameObject.SetActive(true);
        player.canMove = false;

        Show(0);
    }

    public void CloseDialogue()
    {
        isDialogueOpen = false;

        Canvas.gameObject.SetActive(false);
        player.canMove = true;
    }

    public void LoadTXT()
    {
        TextAsset file = Resources.Load<TextAsset>("dialogue");

        string[] lines = file.text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split("|");

            for(int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
            }

            int id = int.Parse(parts[0]);

            nodes[id] = parts;
        }
    }

    public void Show(int id)
    {
        string[] data = nodes[id];
        dialogueText.text = data[1];

        dialogueText.text = data[1];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
            buttons[i].onClick.RemoveAllListeners();
        }

        int btnIndex = 0;

        for (int i = 2; i < data.Length; i += 2)
        {
            if(btnIndex >= buttons.Length) break;
            if (i + 1 >= data.Length) break;

            string text = data[i];
            string nextStr = data[i + 1];

            buttons[btnIndex].gameObject.SetActive(true);
            buttonTexts[btnIndex].text = text;

            int index = btnIndex;

            if(nextStr == "END")
            {
                buttons[index].onClick.AddListener(() => CloseDialogue());
            }
            else if(int.TryParse(nextStr, out int next))
            {
                buttons[index].onClick.AddListener(() => Show(next));
            }

            btnIndex++;
        }
    }
}
