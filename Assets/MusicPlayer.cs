using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;
    private GameObject Gameplay;

    private AudioSource MainMusic;
    private AudioSource GameMusic;
    private string MusicTracker = "MusicTracker";

    [SerializeField]
    private float addend = 0.01f;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // Null check would be cool if working w more than 2 people

        MainMusic = MainMenu.GetComponent<AudioSource>();
        GameMusic = Gameplay.GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (GameObject.Find(MusicTracker).tag == "Music_MainMenu")
        {
            MainMusic.volume = 0.0f;
            MainMusic.Play();

            MainMusic.volume += addend;
            GameMusic.volume -= addend;

            if (GameMusic.volume == 0.0f)
            {
                GameMusic.Stop();
            }
        }
        else if (GameObject.Find(MusicTracker).tag == "Music_Gameplay")
        {
            
        }
    }
}

