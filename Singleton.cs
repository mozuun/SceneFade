using UnityEngine;

// シングルトンの共通基底クラス
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance; // インスタンスの定義
    protected virtual bool isDontDestroy => false; // DontDestroyOnLoadを適応するかどうか(デフォルトはしない)

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)FindAnyObjectByType(typeof(T)); //探したコンポーネントをインスタンスに設定
            if (isDontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject); // 複数存在していた場合、自身を破棄
        }
    }
}
