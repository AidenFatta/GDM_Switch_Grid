using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip menuMusic;
    public AudioClip levelMusic;

    public AudioClip buttonClickSound;
    public AudioClip buttonExitSound;
    public AudioClip shootSound;

    List<string> menuScenes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //Building lists to easily manage what music should play for what scene
        menuScenes = new List<string> {"MainMenu", "SinglePlayerSelect", "Lobby", "MultiplayerSelect"};
        UpdateMusicForScene(SceneManager.GetActiveScene().name);   
    }

    private void Update()
    {
        /* Unused for now, for debugging purposes.
        if (Input.GetButtonDown("Fire1"))
        {
            PlaySound(buttonClickSound);
        }
        */
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    public void playMusic(AudioClip clip, float volume = 1f)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        UpdateMusicForScene(newScene.name);
    }

    private void UpdateMusicForScene(string sceneName)
    {
        if (menuScenes.Contains(sceneName))
        {
            ChangeMusic(menuMusic);
        }
        else if (sceneName.StartsWith("Level"))
        {
            ChangeMusic(levelMusic);
        }
    }

    //Still need to set up audio listeners for sound effects, like clicking buttons and shooting. 
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, sfxSource.volume);
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        if (musicSource.clip == newMusic) return;
        musicSource.Stop();
        playMusic(newMusic, musicSource.volume);
    }

    public void ChangeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }
    public void ChangeSFXVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }
}
