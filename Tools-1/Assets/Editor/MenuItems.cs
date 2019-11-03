using UnityEngine;
using UnityEditor;

public class MenuItems
{
    // Add a new menu item under an existing or new menu
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    // Add a menu item with multiple levels of nesting
    [MenuItem("Tools/SubMenu/Option")]
    private static void NewNestedOption()
    {
    }

    /*
        There’s no validation for overlapping hotkeys!
        defining multiple menu items with the same hotkey results in
        only 1 option being called by hitting the key combination.

        Priorty number controls the ordering of menu items under the root menu.
    */

    // Add a new menu item with hotkey CTRL-SHIFT-A
    [MenuItem("Tools/New Option %#a")]
    private static void NewMenuOption2()
    {
    }

    // Add a new menu item with hotkey CTRL-G
    [MenuItem("Tools/Item %g")]
    private static void NewNestedOption2()
    {
    }

    // Add a new menu item with hotkey G
    [MenuItem("Tools/Item2 _g")]
    private static void NewOptionWithHotkey()
    {
    }

    /*
        Assets: items will be available under the “Assets” menu,
            as well using right-click inside the project view.

        Assets/Create: items will be listed when clicking on the “Create” button
            in the project view (useful when adding new types that can be added to the project)

        CONTEXT/ComponentName: items will be available by right-clicking
            inside the inspector of the given component. 
    */

    // Add a new menu item that is accessed by right-clicking on an asset in the project view
    [MenuItem("Assets/Load Additive Scene")]
    private static void LoadAdditiveScene()
    {
        var selected = Selection.activeObject;
        EditorApplication.OpenSceneAdditive(AssetDatabase.GetAssetPath(selected));
    }

    // Adding a new menu item under Assets/Create
    [MenuItem("Assets/Create/Add Configuration")]
    private static void AddConfig()
    {
        // Create and add a new ScriptableObject for storing configuration
    }

    // Add a new menu item that is accessed by right-clicking inside the RigidBody component
    [MenuItem("CONTEXT/Rigidbody/New Option")]
    private static void NewOpenForRigidBody()
    {
    }

    /*
        Menu item Validation to display in right context
    */

    [MenuItem("Assets/ProcessTexture")]
    private static void DoSomethingWithTexture()
    {
    }

    // Note that we pass the same path, and also pass "true" to the second argument.
    [MenuItem("Assets/ProcessTexture", true)]
    private static bool NewMenuOptionValidation()
    {
        // This returns true when the selected object is a Texture2D (the menu item will be disabled otherwise).
        return Selection.activeObject.GetType() == typeof(Texture2D);
    }

    /*
        sometimes it is necessary to get a reference to the actual component (e.g: to modify its data).
        This can be done by adding a MenuCommand argument to the static method that defines the new menu item
    */
    [MenuItem("CONTEXT/Rigidbody/New Option 2")]
    private static void NewMenuOption3(MenuCommand menuCommand)
    {
        // The Rigidbody component can be extracted from the menu command using the context field.
        var rigid = menuCommand.context as Rigidbody;
    }
}
