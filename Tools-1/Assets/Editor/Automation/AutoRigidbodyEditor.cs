using UnityEditor;

[CustomEditor(typeof(AutoRigidbody))]
public class AutoRigidbodyEditor : Editor
{
    /*
        It is also possible to draw Fire button
        for in play game GUI
    */
    void OnSceneGUI()
    {
        var projectile = target as AutoRigidbody;
        var transform = projectile.transform;
        projectile.damageRadius = Handles.RadiusHandle(
            transform.rotation, 
            transform.position, 
            projectile.damageRadius);
    }
}
