using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public SaveData saveData;
    const string SAVE_KEY = "SaveData";
    protected override bool isDontDestroy => true;

    protected override void Awake()
    {
        base.Awake();
        // 独自の処理
        Load(); // デバッグ用
    }

    //============================
    // Public Methods
    //============================

    // ゲームのロード
    public void Load()
    {
        if (GetSaveData())
        {
            LoadSaveData();
        }
        else
        {
            CreateNewData();
        }
    }

    //　アイテム取得時に保存する
    public void SetItemObtainedFlag(Item.Type item)
    {
        int index = (int)item;
        saveData.itemObtainedFlags[index] = true;
        Save();
    }

    // アイテムの未使用・使用済みを保存
    public void SetItemUsedFlag(Item.Type item)
    {
        int index = (int)item;
        saveData.itemUsedFlags[index] = true;
        Save();
    }

    //　ギミッククリア時に保存する
    public void SetGimmickClearedFlag(Gimmick.Type gimmic)
    {
        int index = (int)gimmic;
        saveData.gimmickClearFlags[index] = true;
        Save();
    }

    // アイテム取得データを返す
    public bool IsItemObtained(Item.Type item)
    {
        int index = (int)item;
        return saveData.itemObtainedFlags[index];
    }

    // アイテム使用データを返す
    public bool IsItemUsed(Item.Type item)
    {
        int index = (int)item;
        return saveData.itemUsedFlags[index];
    }

    // ギミッククリアデータを返す
    public bool IsGimmickCleared(Gimmick.Type gimmic)
    {
        int index = (int)gimmic;
        return saveData.gimmickClearFlags[index];
    }

    //============================
    // Private Methods
    //============================
    private void Save()
    {
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    // 新規データを作成
    private void CreateNewData()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        saveData = new SaveData();
        Save();
    }

    //　既存のデータをロード
    private void LoadSaveData()
    {
        string json = PlayerPrefs.GetString(SAVE_KEY);
        saveData = JsonUtility.FromJson<SaveData>(json);
    }

    // セーブデータがあるかどうか
    private bool GetSaveData()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

}

// ====== セーブデータの種類 ======

[System.Serializable]
public class SaveData
{
    public bool[] itemObtainedFlags = new bool[(int)Item.Type.Max];
    public bool[] itemUsedFlags = new bool[(int)Item.Type.Max];
    public bool[] gimmickClearFlags = new bool[(int)Gimmick.Type.Max];
}
