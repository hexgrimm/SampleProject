using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Models.AssetsManagement.Editor
{
	[CustomPropertyDrawer(typeof(ResourceLink))]
	public class ResourceAddressDrawer : PropertyDrawer
    {
        private GUIStyle _redStyle = new GUIStyle(EditorStyles.label);
        private GUIStyle _greenStyle = new GUIStyle(EditorStyles.label);
        private GUIStyle _yellowStyle = new GUIStyle(EditorStyles.label);

        public ResourceAddressDrawer()
        {
            _redStyle.normal.textColor = Color.red;
            _greenStyle.normal.textColor = Color.green;
            _yellowStyle.normal.textColor = Color.yellow;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawLink(property, position);
        }

        private void DrawLink(SerializedProperty property, Rect position)
        {
            var rect = position;

            EditorGUI.LabelField(rect, property.name + ": ");

            rect.xMin = rect.xMax / 2;
            rect.xMax = position.xMax;

            GameObject toDraw = null;

            var pathProp = property.FindPropertyRelative("PathInAssets");
            var path = pathProp.stringValue;
            
            var idProp = property.FindPropertyRelative("Id");
            var id = idProp.stringValue;

            toDraw = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            var obj = (GameObject)EditorGUI.ObjectField(rect, "", toDraw, typeof(GameObject), false);
            
            if (obj != toDraw)
            {
                var assetPath = AssetDatabase.GetAssetPath(obj);
                var fileName = Path.GetFileNameWithoutExtension(assetPath);
                
                pathProp.stringValue = assetPath;
                idProp.stringValue = fileName.ToLower();
            }
        }

        private Object TryGetAsset(string path)
        {
            return Resources.Load(path);
        }
    }
}