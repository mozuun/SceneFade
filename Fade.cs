using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : Singleton<Fade>
{
    [SerializeField, Header("フェード画像")] private Image _fadeImage;
    [SerializeField, Header("フェード時間")] private float _fadeDuration = 1f;
    protected override bool isDontDestroy => true;

    // フェードイン
    public void FadeIn(System.Action onComplete = null)
    {
        _fadeImage.DOFade(0f, _fadeDuration).SetEase(Ease.InOutSine)
        .OnComplete(() => onComplete?.Invoke()); // フェードイン後に処理があるなら実行
    }

    // フェードアウト
    public void FadeOut(System.Action onComplete = null)
    {
        _fadeImage.DOFade(1f, _fadeDuration).SetEase(Ease.InOutSine)
        .OnComplete(() => onComplete?.Invoke()); // フェードアウト後に処理があるなら実行
    }

    // フェードの色の変更
    public void UpdateFadeImage(Color color)
    {
        _fadeImage.color = color;
    }

    // フェード時間の変更
    public void UpdateFadeDuration(float duration)
    {
        _fadeDuration = duration;
    }
}
