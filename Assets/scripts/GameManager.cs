using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public  void PauseMenu()
    {
        if(Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Pause");
        }
    }
    private void Update()
    {
        PauseMenu();
    }

}
