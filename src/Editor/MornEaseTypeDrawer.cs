#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MornLib
{
    [CustomPropertyDrawer(typeof(MornEaseType))]
    public sealed class MornEaseTypeDrawer : PropertyDrawer
    {
        private const float ButtonWidth = 48f;
        private const float Spacing = 4f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var popupRect = new Rect(position.x, position.y, position.width - ButtonWidth - Spacing, position.height);
            var buttonRect = new Rect(position.xMax - ButtonWidth, position.y, ButtonWidth, position.height);

            EditorGUI.PropertyField(popupRect, property, label);

            if (GUI.Button(buttonRect, "確認"))
            {
                var type = (MornEaseType)property.enumValueIndex;
                PopupWindow.Show(buttonRect, new MornEaseCurvePopup(type));
            }
        }
    }

    internal sealed class MornEaseCurvePopup : PopupWindowContent
    {
        private readonly MornEaseType _type;
        private const int Width = 280;
        private const int Height = 280;

        public MornEaseCurvePopup(MornEaseType type)
        {
            _type = type;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(Width, Height);
        }

        public override void OnGUI(Rect rect)
        {
            EditorGUILayout.LabelField(_type.ToString(), EditorStyles.boldLabel);
            var graphRect = GUILayoutUtility.GetRect(Width - 16, Height - 40);
            DrawGraph(graphRect, _type);
        }

        private static void DrawGraph(Rect rect, MornEaseType type)
        {
            EditorGUI.DrawRect(rect, new Color(0.15f, 0.15f, 0.15f));

            const int samples = 128;
            var values = new float[samples];
            var minV = 0f;
            var maxV = 1f;
            for (var i = 0; i < samples; i++)
            {
                var x = (float)i / (samples - 1);
                var v = x.Ease(type);
                values[i] = v;
                if (v < minV) minV = v;
                if (v > maxV) maxV = v;
            }
            var pad = (maxV - minV) * 0.1f;
            minV -= pad;
            maxV += pad;
            var range = maxV - minV;
            if (range < 0.0001f) range = 1f;

            // y=0 / y=1 reference lines
            Handles.color = new Color(1f, 1f, 1f, 0.25f);
            var y0 = rect.yMax - (0f - minV) / range * rect.height;
            var y1 = rect.yMax - (1f - minV) / range * rect.height;
            Handles.DrawLine(new Vector3(rect.xMin, y0), new Vector3(rect.xMax, y0));
            Handles.DrawLine(new Vector3(rect.xMin, y1), new Vector3(rect.xMax, y1));

            // border
            Handles.color = new Color(1f, 1f, 1f, 0.4f);
            Handles.DrawLine(new Vector3(rect.xMin, rect.yMin), new Vector3(rect.xMin, rect.yMax));
            Handles.DrawLine(new Vector3(rect.xMax, rect.yMin), new Vector3(rect.xMax, rect.yMax));

            // curve
            var pts = new Vector3[samples];
            for (var i = 0; i < samples; i++)
            {
                var x = (float)i / (samples - 1);
                var nx = rect.xMin + x * rect.width;
                var ny = rect.yMax - (values[i] - minV) / range * rect.height;
                pts[i] = new Vector3(nx, ny);
            }
            Handles.color = new Color(0.4f, 0.85f, 1f);
            Handles.DrawAAPolyLine(2f, pts);
        }
    }
}
#endif
