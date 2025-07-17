public static class Navigation {
    public static StackNavigator Stack => NavigationContainer.Instance.Stack;
    public static TabNavigator Tab => NavigationContainer.Instance.Tab;
    public static ModalManager Modal => NavigationContainer.Instance.Modal;
}