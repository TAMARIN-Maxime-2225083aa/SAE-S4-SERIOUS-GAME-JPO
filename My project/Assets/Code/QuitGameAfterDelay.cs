using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor; // Nécessaire pour acc�der � EditorApplication.ExitPlaymode
#endif

public class QuitGameAfterDelay : MonoBehaviour
{
    public float delay = 10f; // Délai en secondes avant de quitter le jeu

    void Start()
    {
        StartCoroutine(QuitAfterDelay());
    }

    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Reinitialise l'inventaire et par conséquent le jeu
        CleManager.Instance.RemovePickupItems();

        // Remet le NPC à true dans le couloir
        NPCGuideManager.Instance.ToggleCinematicCouloir();

        // Revenir au menu principal
        SceneManager.LoadScene("MenuPrincipal");
    }
}