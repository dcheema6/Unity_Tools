using UnityEditor;

[CustomEditor(typeof(AutoObjectInstantiation))]
public class AutoObjectEditor : Editor
{

    /*
        It is also possible to  Draw dotted lines to show path of the projectile using Gizmos
    */
    void OnSceneGUI()
    {
        var launcher = target as AutoObjectInstantiation;
        var transform = launcher.transform;
        launcher.offset = transform.InverseTransformPoint(
            Handles.PositionHandle(
                transform.TransformPoint(launcher.offset), 
                transform.rotation));
    }
}
