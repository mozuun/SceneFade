using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void OnClickStart()
    {
        ScenesChanger.instance.ToLoadScene(ScenesChanger.Type.Clear);
    }
}