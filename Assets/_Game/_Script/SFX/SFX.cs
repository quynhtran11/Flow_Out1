using System.Collections.Generic;
using UnityEngine;
public class SFX : Singleton<SFX>
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sound;

    private Queue<AudioSource> allSoundSources = new Queue<AudioSource>();
    private Queue<AudioSource> allMusicSources = new Queue<AudioSource>();
    protected override bool dondestroy => true;

    private void Start()
    {
        PlayMusic();
    }
    private AudioSource GetSource()
    {
        if (allSoundSources == null || allSoundSources.Count <= 0)
        {
           AudioSource audio = Instantiate(sound);
        }
        return GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        if (!UserData.IsMusic()) return;
        music.clip = GameData.Instance.SoundData.GetData(ESoundKey.Music).clip;
        music.Play();
    }
    public void PlaySound(ESoundKey key, float volume = 1)
    {
        if (!UserData.IsSound()) return;
        sound.clip = GameData.Instance.SoundData.GetData(key).clip;
        sound.Play();
    }
    public void PlayVibration()
    {
        //Handheld.Vibrate();
    }
    public void StopMusic()
    {
        music.Stop();
    }
}
public enum ESoundKey
{
    Music,
    Click,
    Win,
    Lose,
    Coin,
    CoinTick,
}
