
public static class SoundUtil
{
    /// <summary>
    /// SE‚ğÄ¶
    /// ’Z‚¢Œø‰Ê‰¹‚ÌÄ¶‚Ég—p
    /// Ä¶Œã‚É’â~‚·‚é‚±‚Æ‚Í‚Å‚«‚È‚¢
    /// </summary>
    public static void PlaySe(string assetName)
    {
        AudioManager.Instance.PlaySe(assetName);
    }

    /// <summary>
    /// BGM‚ğÄ¶
    /// ƒQ[ƒ€’†1‚Â‚¾‚¯–Â‚éBGM‚ÌÄ¶‚Ég—p
    /// Ä¶Œã‚É’â~‰Â”\‚¾‚ª•¡”‰¹‚ğ“¯‚ÉÄ¶‚Í‚Å‚«‚È‚¢
    /// Ä¶’†‚É•ÊBGM‚ğÄ¶‚·‚é‚ÆÄ¶’†‚ÌBGM‚Í’â~‚³‚ê‚é
    /// </summary>
    public static void PlayBgm(string assetName)
    {
        AudioManager.Instance.PlayBgm(assetName);
    }

    /// <summary>
    /// BGM‚ğ’â~
    /// </summary>
    public static void StopBgm()
    {
        AudioManager.Instance.StopBgm();
    }
}
