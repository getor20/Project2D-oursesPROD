using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioSource _sFX;

    [field: SerializeField] public AudioClip BackgroundMusicUn { get; private set; }
    [field: SerializeField] public AudioClip BackgroundMusicTwo { get; private set; }

    [field: SerializeField] public AudioClip DeathClip { get; private set; }
    [field: SerializeField] public AudioClip WinClip { get; private set; }

    private bool _isMusicPlaying = false;

    private void Start()
    {
        // Убедимся, что AudioSource для музыки не зациклен
        _sound.loop = false;

        // Запускаем первый трек
        StartNextBackgroundMusic();
    }

    private void Update()
    {
        // Проверяем, если флаг говорит, что музыка должна играть, НО AudioSource остановился
        // (это означает, что трек закончился, а не был поставлен на паузу)
        if (_isMusicPlaying && !_sound.isPlaying && _sound.time == 0)
        {
            // Здесь ваш код, который выполнится, когда песня закончится
            Debug.Log("Фоновая песня закончилась! Запускаем следующую...");

            // Запускаем логику переключения
            StartNextBackgroundMusic();
        }
    }

    /// <summary>
    /// Логика переключения треков (взаимное переключение Un <-> Two)
    /// </summary>
    private void StartNextBackgroundMusic()
    {
        // 1. Проверяем, какой клип только что закончился
        if (_sound.clip == BackgroundMusicUn)
        {
            // 2. Устанавливаем следующий клип
            _sound.clip = BackgroundMusicTwo;
        }
        else if (_sound.clip == BackgroundMusicTwo)
        {
            // 3. Устанавливаем первый клип
            _sound.clip = BackgroundMusicUn;
        }
        else // Случай, когда запускается первый раз в Start()
        {
            // Рандомный выбор первого трека
            _sound.clip = (Random.Range(0, 2) == 0) ? BackgroundMusicUn : BackgroundMusicTwo;
        }

        // 4. Начинаем воспроизведение нового клипа
        _sound.Play();
        _isMusicPlaying = true; // Устанавливаем флаг, что музыка играет
    }

    /// <summary>
    /// Воспроизводит SFX, ставит BGM на паузу, ждет завершения SFX и возобновляет BGM.
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        // 1. Останавливаем все ранее запущенные корутины возобновления музыки, чтобы избежать конфликтов
        StopCoroutine("WaitForSFXToFinishAndResumeBGM");

        // 2. Если BGM играет, ставим ее на паузу
        if (_sound.isPlaying)
        {
            _sound.Pause();
        }

        // 3. Запускаем SFX (Используем Play, чтобы статус _sFX.isPlaying был корректным)
        _sFX.clip = clip;
        _sFX.Play();

        // 4. Запускаем корутину, которая возобновит BGM после SFX
        StartCoroutine(WaitForSFXToFinishAndResumeBGM());
    }

    /// <summary>
    /// Корутина для ожидания завершения SFX и возобновления фоновой музыки.
    /// </summary>
    private IEnumerator WaitForSFXToFinishAndResumeBGM()
    {
        // Ждем, пока _sFX.isPlaying не станет false (то есть пока SFX не закончится)
        yield return new WaitWhile(() => _sFX.isPlaying);

        // Если фоновая музыка должна играть (она не была остановлена вручную командой Stop/другим кодом)
        if (_isMusicPlaying)
        {
            // Возобновляем фоновую музыку с того же места
            _sound.UnPause();
        }
    }

    // Добавьте этот метод, если хотите иметь контроль над возобновлением музыки
    public void ResumeBackgroundMusic()
    {
        // Возобновляем, только если флаг _isMusicPlaying установлен (т.е. мы хотим, чтобы музыка играла),
        // и если она в данный момент не играет. UnPause() работает, только если была Pause().
        if (_isMusicPlaying && !_sound.isPlaying && _sound.time > 0)
        {
            _sound.UnPause();
        }
    }
}
