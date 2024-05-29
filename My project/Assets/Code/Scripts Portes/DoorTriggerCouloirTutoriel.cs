using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DoorTriggerCouloirTutoriel : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called");

        if (other.CompareTag("Player"))
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Video", "porte_anim.mp4");
            videoPlayer.url = videoPath;

            videoPlayer.Play();
            videoPlayer.loopPointReached += LoadCouloirTutorielScene;
        }
    }

    void LoadCouloirTutorielScene(VideoPlayer vp)
    {
        Scene currScene = SceneManager.GetActiveScene();

        string spawnPoint = null;
        if (currScene.name == "Salle Tutoriel")
        {
            spawnPoint = "Salle Tutoriel";
        }
        else
        {
            Debug.LogError(currScene.name);
        }

        PlayerPrefs.SetString("PointDeSpawn", spawnPoint);

        SceneManager.LoadScene("Couloir Tutoriel");
    }
}
