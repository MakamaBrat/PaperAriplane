using UnityEngine;
using UnityEngine.UI;

public class SettingsSwitcher : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource[] sfxSources;

    [Header("UI Image")]
    public Image sfxImage;


    [Header("Transparency")]
    [Range(0f, 1f)] public float offAlpha = 0.5f;
    [Range(0f, 1f)] public float onAlpha = 1f;

    private bool sfxOn = true;

    private const string SFX_KEY = "SFX_ON";

    void Start()
    {
        // 🔹 загружаем сохранённое состояние
        sfxOn = PlayerPrefs.GetInt(SFX_KEY, 1) == 1;
        ApplySfxState();
    }

    // --------------------
    // SWITCH
    // --------------------
    public void SwitchSfx()
    {
        sfxOn = !sfxOn;
        PlayerPrefs.SetInt(SFX_KEY, sfxOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplySfxState();
    }

    // --------------------
    // APPLY
    // --------------------
    void ApplySfxState()
    {
        foreach (AudioSource sfx in sfxSources)
        {
            if (sfx != null)
                sfx.mute = !sfxOn;
        }

        ApplyImageState(sfxImage, sfxOn);
    }

    // --------------------
    // UI HELPER
    // --------------------
    void ApplyImageState(Image img, bool state)
    {
        if (img == null) return;

        // прозрачность
        Color c = img.color;
        c.a = state ? onAlpha : offAlpha;
        img.color = c;

        
    }
}
