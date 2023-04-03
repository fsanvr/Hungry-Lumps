using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem: MonoBehaviour
{
    [SerializeField] private LevelData data;
    [SerializeField] private int menuSceneIndex = 0;
    [SerializeField] private int gameSceneIndex = 1;

    public void EnableMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void EnableLevel(int level)
    {
        data.enabledLevel = level;
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void EnableNewLevel()
    {
        //TODO: как сообщить о генерации нового уровня?
        //создать тут новый ключ сцены?
        data.enabledLevel = -1;
        SceneManager.LoadScene(gameSceneIndex);
    }
 }