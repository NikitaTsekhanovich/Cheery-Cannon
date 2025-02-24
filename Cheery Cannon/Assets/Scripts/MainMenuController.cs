using MusicSystem;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Image _musicImage;
    [SerializeField] private Image _soundImage;
    
    private void Start()
    {
        MusicController.Instance.LoadIcons(_musicImage, _soundImage);
    }

    public void ClickChangeMusic(Image icon)
    {
        MusicController.Instance.CheckMusicState(icon);
    }

    public void ClickChangeSoundEffects(Image icon)
    {
        MusicController.Instance.CheckSoundEffectsState(icon);
    }
}
