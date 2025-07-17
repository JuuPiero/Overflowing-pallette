using UnityEngine;


public class NavigationContainer : MonoBehaviour {
    public static NavigationContainer Instance { get; private set; }

    [Header("Navigation")]
    [SerializeField] private StackNavigator _stack;
    [SerializeField] private TabNavigator _tab;
    [SerializeField] private ModalManager _modal;

    public StackNavigator Stack => _stack;
    public TabNavigator Tab => _tab;
    public ModalManager Modal => _modal;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}