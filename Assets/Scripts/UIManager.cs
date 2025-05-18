

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set;}

    public Button resetButton;
    public Button menuButton;
    public Canvas levelsCanvas;
    public Canvas menuCanvas;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        
    }

}