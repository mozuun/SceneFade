using UnityEngine.SceneManagement;

public class ScenesChanger : Singleton<ScenesChanger>
{
    private bool _loading = false;// シーン移動中かどうか
    protected override bool isDontDestroy => true;

    // シーンの種類
    public enum Type
    {
        Title,
        Main,
        Clear,
    }

    // シーン移動
    public void ToLoadScene(Type sceneType)
    {
        if (_loading || Fade.instance == null) return; // 連打防止 && nullチェック

        _loading = true; // フラグセット

        // フェードアウトが終わったらシーン遷移
        Fade.instance.FadeOut(() =>
        {
            SceneManager.sceneLoaded += OnSceneLoaded; // シーン移動後のイベント登録
            SceneManager.LoadScene(sceneType.ToString()); // シーンを読み込む
        });
    }

    // シーン移動が終わってから呼ばれる処理
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade.instance.FadeIn(); // フェードイン
        _loading = false; // フラグ解除
        SceneManager.sceneLoaded -= OnSceneLoaded; // イベント登録解除
    }
}
