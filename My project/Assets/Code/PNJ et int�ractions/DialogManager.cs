using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public Animator Animator;
    public GameObject DialogUI;
    private Coroutine typingCoroutine;
    private Queue<string> sentences;
    public TMP_Text nameText;
    public TMP_Text dialogText;
    private NPCGuideManager npcGuideManager;
    public PlayerMovement playerMovement;
    private int currentSentenceIndex = 0;
    public string requiredSceneForImage = "Couloir";
    public GameObject Cle;
    public GameObject Carte;
    public GameObject diapo1;
    public GameObject diapo2;
    public GameObject diapo3;

    private HashSet<string> playedDialogues = new HashSet<string>(); // Pour suivre les dialogues déjà joués

    public static DialogManager Instance;
    public bool IsDialogueActive { get; private set; }
    private bool isTyping = false;
    private bool presentation = false;

    private void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
        npcGuideManager = FindObjectOfType<NPCGuideManager>();
        Cle = GameObject.Find("Cle");
        Carte = GameObject.Find("Carte");
        diapo1 = GameObject.Find("diapo1");
        diapo2 = GameObject.Find("diapo2");
        diapo3 = GameObject.Find("diapo3");

        if (Cle != null)
        {
            Cle.SetActive(false); // Disable Cle at the start of the game
        }
        if (Carte != null)
        {
            Carte.SetActive(false); 
        }
        if (diapo1 != null)
        {
            diapo1.SetActive(false); 
        }
        if (diapo2 != null)
        {
            diapo2.SetActive(false);
        }
        if (diapo3 != null)
        {
            diapo3.SetActive(false);
        }

    }

    //commente ma fonction StartDialog
    //est appelée par le script DialogTrigger
    //permet de lancer un dialogue
    //prend en paramètre un objet de type Dialog

    public void StartDialog(Dialog dialog)
    {
        IsDialogueActive = true;
        currentSentenceIndex = 0;

        // met en pause le mouvement du joueur
        if (playerMovement != null)
        {
            playerMovement.SetCanMove(false);
        }
        Animator.SetBool("isOpen", true);
        nameText.text = dialog.name;
        sentences.Clear();

        if (dialog.name == "Secretaire 2" && !ElementalInventory.Instance.contains("Carte d'étudiant", 1))
        {
            // Add a specific sentence instructing to speak to "Secretaire 1" first
            sentences.Enqueue("Bonjour");
            sentences.Enqueue("Vous êtes venu chercher la clé pour la salle 1 ?");
            sentences.Enqueue("Il semble que je ne puisse pas vous fournir la clé, car vous n'avez pas de carte d'étudiant");
            sentences.Enqueue("Vous devez d'abord parler à Secretaire 1 pour obtenir la carte d'etudiant.");
            
        }
        else if(dialog.name == "Secretaire 1" && !presentation)
        {
            sentences.Enqueue("Bonjour, avant d'obtenir votre carte d'étudiant, vous devez en savoir plus sur notre université");
            sentences.Enqueue("Près du mur, il y a un ordinateur où vous pouvez consulter la présentation de l'université");
            sentences.Enqueue("Cela vous aidera à mieux comprendre comment se déroule l'enseignement ici");
            sentences.Enqueue("Après avoir regardé la présentation, je pourrai vous remettre votre carte d'étudiant");
        }
        else
        {
            foreach (string sentence in dialog.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();

        // Marquer le dialogue comme joué
        playedDialogues.Add(dialog.name);
    }

    //est appelée par le script DialogTrigger
    //permet d'afficher la phrase suivante
    //prend en paramètre un objet de type Dialog
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            StopCoroutine(typingCoroutine);
            EndDialog();
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            if (nameText.text == "Secretaire 1")
            {
                if (Carte != null && presentation)
                {
                    Carte.SetActive(true);
                }
            }
            if (nameText.text == "Secretaire 2" && ElementalInventory.Instance.contains("Carte d'étudiant", 1))
            {
                if (Cle != null)
                {
                    Cle.SetActive(true);
                }
            }
            if (nameText.text == "Presentation IUT")
            {
                diapo1.SetActive(false);
                diapo2.SetActive(false);
                diapo3.SetActive(false);
                presentation = true;
            }
            return;
        }
        if (nameText.text == "Presentation IUT" && currentSentenceIndex == 0)
        {
            diapo1.SetActive(true);
        }
        if (nameText.text == "Presentation IUT" && currentSentenceIndex == 3)
        {
            diapo2.SetActive(true);
        }
        if (nameText.text == "Presentation IUT" && currentSentenceIndex == 7)
        {
            diapo3.SetActive(true);
        }

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }

        string sentence = sentences.Dequeue();
        currentSentenceIndex++;
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    //Gere l'affichage lettre par lettre des phrases
    private IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
    }

    public void EndDialog()
    {
        IsDialogueActive = false;
        Animator.SetBool("isOpen", false);

        if (npcGuideManager != null)
        {
            npcGuideManager.OnDialogueComplete();
        }

        if (playerMovement != null)
        {
            playerMovement.SetCanMove(true);
        }
    }
}
