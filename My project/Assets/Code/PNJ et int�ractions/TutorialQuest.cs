using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialQuest : MonoBehaviour
{

    public static int QuestStep = 1; // Etape actuelle de la qu�te
    GameObject targetobject;
    Scene currentScene;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {


    }
    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        switch (QuestStep)
        {
            case 1: // Voir le PNG
                if (currentScene.name == "Couloir Tutoriel")
                {
                    targetobject = GameObject.Find("etudiant_Tutoriel");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                break;

            case 2: // Récupérer la clé
                if (currentScene.name == "Couloir Tutoriel")
                {
                    targetobject = GameObject.Find("Cle Tutoriel");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                break;

            case 3: // voir le boss la première fois
                if (currentScene.name == "Couloir Tutoriel")
                {
                    targetobject = GameObject.Find("Porte_salle_tutoriel");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                else if (currentScene.name == "Salle Tutoriel")
                {
                    targetobject = GameObject.Find("Boss Anonyme");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                break;
            case 4: // prendre premier indice
                if (currentScene.name == "Couloir Tutoriel")
                {
                    targetobject = GameObject.Find("Porte_salle_tutoriel");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                else if (currentScene.name == "Salle Tutoriel")
                {
                    targetobject = GameObject.Find("indice Graphe Cible");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                break;

            case 5: // prendre deuxième indice
                if (currentScene.name == "Couloir Tutoriel")
                {
                    targetobject = GameObject.Find("Porte_salle_tutoriel");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                else if (currentScene.name == "Salle Tutoriel")
                {
                    targetobject = GameObject.Find("indice Courir Cible");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                break;

            case 6: // revoir boss pour combat
                if (currentScene.name == "Couloir Tutoriel")
                {
                    targetobject = GameObject.Find("Porte_salle_tutoriel");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                else if (currentScene.name == "Salle Tutoriel")
                {
                    targetobject = GameObject.Find("Boss Anonyme");
                    ArrowOrbit.ChangeTarget(targetobject.transform);
                }
                break;

            default:
                break;
        }
    }
}
