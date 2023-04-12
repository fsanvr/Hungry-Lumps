using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndSystem : MonoBehaviour
{
    [SerializeField] private InputSystem input;

    public void LevelWin()
    {
        input.Block();
        //TODO: show win screen
        Debug.Log("You win ^^");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelLose()
    {
        input.Block();
        //TODO: show lose screen
        Debug.Log("You lose :(");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}