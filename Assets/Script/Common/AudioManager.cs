using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioSource _sFX;

    [field: Header("        Sound Clips")]
    [field: SerializeField] private AudioClip[] _backgroundMusicSound;
    [field: SerializeField] public AudioClip WinSound { get; private set; }
    [field: SerializeField] public AudioClip DeathSound { get; private set; }

    [field: Header("        SFX Clips")]
    [field: SerializeField] public AudioClip HitClip { get; private set; }
    [field: SerializeField] public AudioClip SelectionClip { get; private set; }
    [field: SerializeField] public AudioClip[] DeathClip { get; private set; }


    public AudioClip CurrentDeathClip { get; private set; }

    private int _currentTrackIndex = -1;

    private void Start()
    {
        _sound.loop = false;
        PlayNextBackgroundMusic();
    }

    private void Update()
    {
        if (!_sound.isPlaying && _backgroundMusicSound.Length > 0)
        {
            Debug.Log("Фоновая песня/спец.клип закончился! Запускаем следующую BGM...");
            PlayNextBackgroundMusic();
        }

        CurrentDeathClip = DeathClip[Random.Range(0, DeathClip.Length)];
    }

    private void PlayNextBackgroundMusic()
    {
        if (_backgroundMusicSound.Length == 0)
        {
            Debug.LogWarning("Список фоновой музыки пуст!");
            return;
        }

        if (_backgroundMusicSound.Length == 1)
        {
            _sound.clip = _backgroundMusicSound[0];
            _sound.Play();
            _currentTrackIndex = 0;
            return;
        }

        int newIndex = _currentTrackIndex;

        int attempts = 0;
        const int MAX_ATTEMPTS = 10;

        while (newIndex == _currentTrackIndex && attempts < MAX_ATTEMPTS)
        {
            newIndex = Random.Range(0, _backgroundMusicSound.Length);
            attempts++;
        }

        if (newIndex != _currentTrackIndex)
        {
            _currentTrackIndex = newIndex;
            _sound.clip = _backgroundMusicSound[_currentTrackIndex];
            _sound.Play();
        }
        else if (_currentTrackIndex == -1 && _backgroundMusicSound.Length > 0)
        {
            _currentTrackIndex = Random.Range(0, _backgroundMusicSound.Length);
            _sound.clip = _backgroundMusicSound[_currentTrackIndex];
            _sound.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _sound.Stop();
        _sound.PlayOneShot(clip);
    }


    public void PlaySFX(AudioClip clip)
    {
        /*var randomPitch = Random.Range(0, DeathClip.Length);

        _sFX.clip = DeathClip[randomPitch];*/

        _sFX.PlayOneShot(clip);
    }
}