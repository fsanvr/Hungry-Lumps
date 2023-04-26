using TMPro;
using UnityEngine;

public class LevelEndSystem : MonoBehaviour
{
    [SerializeField] private InputSystem input;

    [SerializeField] private MenuSystem menu;
    [SerializeField] private GameObject winView;
    [SerializeField] private GameObject loseView;

    [SerializeField] private TextMeshProUGUI winSatietyView;
    
    private bool _levelEnded;

    public bool IsLevelEnded()
    {
        return _levelEnded;
    }

    public void LevelWin(int satiety)
    {
        _levelEnded = true;
        input.Block();
        
        winSatietyView.text = satiety.ToString();
        
        menu.ActiveOnly(winView);
        
    }

    public void LevelLose()
    {
        _levelEnded = true;
        input.Block();
        menu.ActiveOnly(loseView);
    }
}