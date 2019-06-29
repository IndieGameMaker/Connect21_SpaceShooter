using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Play");
    }
}
