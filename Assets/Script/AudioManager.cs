using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Players")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSourcePrefab;   // ← これをプレハブ化推奨

    private List<AudioSource> activeOneShotSources = new List<AudioSource>();

    [Header("Volumes")]
    [Range(0f, 1f)] public float bgmVolume = 0.7f;
    [Range(0f, 1f)] public float seVolume = 0.8f;

    [Header("Clips")]
    public AudioClip titleBGM;
    public AudioClip gameBGM;
    // 必要ならもっと追加

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 最初にAudioSourceがなければ作る
        if (bgmSource == null)
        {
            var go = new GameObject("BGMPlayer");
            go.transform.SetParent(transform);
            bgmSource = go.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }
    }

    void Update()
    {
        if (bgmSource.volume != bgmVolume) bgmSource.volume = bgmVolume;
    }

    public void PlayBGM(string sceneName)
    {
        AudioClip target = null;

        if (sceneName.Contains("Title")) target = titleBGM;
        else if (sceneName.Contains("Game")) target = gameBGM;
        // else ... 他のシーン用

        if (target == null || bgmSource.clip == target) return;

        bgmSource.Stop();
        bgmSource.clip = target;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void PlaySE(AudioClip clip, float pitch = 1f, float volumeScale = 1f)
    {
        if (clip == null) return;

        // プール方式が理想だが、とりあえず毎回InstantiateでもOK（軽いSEなら）
        var source = Instantiate(seSourcePrefab, transform);
        source.clip = clip;
        source.volume = seVolume * volumeScale;
        source.pitch = pitch;
        source.Play();

        activeOneShotSources.Add(source);

        // 終わったら自動削除（コルーチンでも可）
        StartCoroutine(DestroyWhenFinished(source));
    }

    private System.Collections.IEnumerator DestroyWhenFinished(AudioSource src)
    {
        yield return new WaitWhile(() => src && src.isPlaying);
        if (src) Destroy(src.gameObject);
        activeOneShotSources.Remove(src);
    }

    // シーン読み込み時にBGM切り替え
    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoad;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoad;

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name);
    }
}