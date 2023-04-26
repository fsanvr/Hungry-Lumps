using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem: MonoBehaviour
{
    [SerializeField] private int menuSceneIndex = 0;
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private LevelData levelData;
    [SerializeField] private PlayerData playerData;

    private GridGenerator _generator;

    private void Awake()
    {
        _generator = new GridGenerator(levelData, playerData);
    }

    public void EnableMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void EnableNewLevel()
    {
        var grid = _generator.Generate();
        levelData.grid = grid.Item1;
        levelData.graph = grid.Item2;
        

        SceneManager.LoadScene(gameSceneIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
 }