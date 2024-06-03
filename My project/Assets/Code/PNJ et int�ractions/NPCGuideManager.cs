using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCGuideManager : MonoBehaviour
{
    public Animator characterAnimator; 
    public DialogManager dialogManager; 
    public GameObject characterSprite; 
    public string characterName; 
    public string[] dialogueLines; 
    private PlayerMovement playerMovement; 
    public CinematicBars cinematicBars;
    private static bool cinematicCouloir = true;

    // pour reinitialisé en dehors de la class
    public static NPCGuideManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        Debug.Log("Cinematic couloir : " + cinematicCouloir);
        // Verifie si c'est la premi�re fois que la cin�matique est lanc�e dans cette session de jeu
        if (cinematicCouloir || SceneManager.GetActiveScene().name.Contains("Tutoriel"))
        {

            if (playerMovement != null)
            {
                playerMovement.SetCanMove(false);
            }

            StartCinematic();

            // Comme cinématique déjà effectué on ne la refait pas
            ToggleCinematicCouloir();
        }
        else
        {
            if (playerMovement != null)
            {
                playerMovement.SetCanMove(true);
            }
        }
    }

    public void StartCinematic()
    {
        characterAnimator.SetTrigger("EnterScreen");
        cinematicBars.ShowBars();
        Invoke(nameof(StartDialogue), 1.5f);
    }

    void StartDialogue()
    {
        Dialog dialog = new Dialog
        {
            name = characterName,
            sentences = dialogueLines
        };

        dialogManager.StartDialog(dialog);
    }

    public void OnDialogueComplete()
    {
        Invoke(nameof(EndCinematic), 0.5f);
    }

    public void EndCinematic()
    {
        characterAnimator.SetTrigger("ExitScreen");
        cinematicBars.HideBars();

        if (playerMovement != null)
        {
            playerMovement.SetCanMove(true);
        }
    }

    public void ToggleCinematicCouloir()
    {
        cinematicCouloir = !cinematicCouloir;
    }
}
