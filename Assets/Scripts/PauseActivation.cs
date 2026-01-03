using UnityEngine;

public class PauseActivation : MonoBehaviour
{
    public Transform pause;

    private void OnEnable()
    {
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void TogglePause()
    {
        pause.gameObject.SetActive(!pause.gameObject.activeInHierarchy);
        if(pause.gameObject.activeInHierarchy)
        {
            
            Time.timeScale = 0;
        }
        else
        {

            Time.timeScale = 1;
        }
    }
}
