using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RotateTrap))]
public class TrapEditor3 : Editor
{
    private RotateTrap _trap;
    private Editor _trapEditor;
    public override void OnInspectorGUI()
    {
        // Checks if something in the GUI changed
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
            }
        }

        DrawSettingsEditor(_trap.TrapData, _trap.OnTrapSettingsUpdated, ref _trapEditor, ref _trap.TrapSettingsFoldout);
    }
    
    private void OnEnable()
    {
        _trap = (RotateTrap)target;
    }
    
    private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref Editor editor, ref bool foldout)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            // Checks if something in the Editor changed
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    // Saving Editor and only creating a new one if needed
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                            onSettingsUpdated.Invoke();
                    }
                }
            }
        }
    } 
}
