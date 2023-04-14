using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem: MonoBehaviour
{
    [SerializeField] private int menuSceneIndex = 0;
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private LevelData data;
    
    private GridGenerator _generator;

    private void Awake()
    {
        _generator = new GridGenerator();
    }

    public void EnableMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void EnableNewLevel()
    {
        var grid = _generator.Generate();
        data.Grid = grid;
        data.cellPrefab = Resources.Load<GameObject>("Prefabs/Cell");
        data.pet = Resources.Load<Pet>("Data/Pets/Cat/Cat");
        
        SceneManager.LoadScene(gameSceneIndex);
    }
 }