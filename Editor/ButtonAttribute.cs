using UnityEngine;
using System;
using UnityEditor;

namespace Editor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string Label { get; private set; }
        public Color ButtonColor { get; private set; } 

        public ButtonAttribute(string label = "", float r = 1.0f, float g = 1.0f, float b = 1.0f, float a = 1.0f)
        {
            Label = label;
            ButtonColor = new Color(r, g, b, a);
        }
        public ButtonAttribute(float r = 1.0f, float g = 1.0f, float b = 1.0f, float a = 1.0f)
        {
            Label = "";
            ButtonColor = new Color(r, g, b, a);
        }
        public ButtonAttribute()  
        {
            Label = "";
            ButtonColor = Color.white;
        }
    }

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonAttributeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var targetObj = target as MonoBehaviour;
            if (!targetObj) return;

            var methods = targetObj.GetType().GetMethods();

            foreach (var method in methods)
            {
                var attributes = (ButtonAttribute[])method.GetCustomAttributes(typeof(ButtonAttribute), false);
                if (attributes.Length <= 0) continue;

                var buttonLabel = attributes[0].Label;
                if (buttonLabel == "") buttonLabel = method.Name;


                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUI.backgroundColor = attributes[0].ButtonColor;

                if (GUILayout.Button(buttonLabel, GUILayout.Width(200)))
                {
                    method.Invoke(targetObj, null);
                }

                GUI.backgroundColor = Color.white;
                GUILayout.EndHorizontal();
            }
        }
    }

}