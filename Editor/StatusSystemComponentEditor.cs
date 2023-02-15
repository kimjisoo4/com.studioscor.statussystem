using UnityEngine;
using UnityEditor;
using StudioScor.Utilities;
using StudioScor.Utilities.Editor;

namespace StudioScor.StatusSystem.Editor
{
    
    [CustomEditor(typeof(StatusSystemComponent))]
    [CanEditMultipleObjects]
    public class StatusSystemComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                GUILayout.Space(5f);
                SEditorUtility.GUI.DrawLine(4f);
                GUILayout.Space(5f);

                var statusSystem = (StatusSystemComponent)target;

                var statuses = statusSystem.Statuses;

                GUIStyle title = new();
                GUIStyle full = new();
                GUIStyle zero = new();
                GUIStyle normal = new();

                title.normal.textColor = Color.white;
                title.alignment = TextAnchor.MiddleCenter;
                title.fontStyle = FontStyle.Bold;

                normal.normal.textColor = Color.white;
                full.normal.textColor = Color.green;
                zero.normal.textColor = Color.red;

                GUILayout.Label("[ Status ]", title);

                if (statuses is not null)
                {
                    GUIStyle style;

                    foreach (var status in statuses)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(status.Key.Name, normal);
                        GUILayout.FlexibleSpace();

                        switch (status.Value.CurrentState)
                        {
                            case EStatusState.Fulled:
                                style = full;
                                break;
                            case EStatusState.Emptied:
                                style = zero;
                                break;
                            default:
                                style = normal;
                                break;
                        }

                        GUILayout.Label(" [ " + status.Value.CurrentValue.ToString("N0") + " / " + status.Value.MaxValue.ToString("N0") + " ] ", style);

                        GUILayout.Space(10f);
                        GUILayout.EndHorizontal();

                        SEditorUtility.GUI.DrawLine(1f);
                    }
                }
            }
        }
    }
}
