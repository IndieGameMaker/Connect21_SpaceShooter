using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnClickStart()
    {
        //기존 씬을 삭제한 후에 로딩
        SceneManager.LoadScene("Level01", LoadSceneMode.Single); 
        //기존 씬을 그대로 둔채로 중첩해서 로딩(Merge)
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
        //UI 씬 로딩
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
