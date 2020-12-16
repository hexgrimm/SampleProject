using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Models.AssetsManagement.Editor
{
	[CustomPropertyDrawer(typeof(ResourceAddressAttribute))]
	public class ResourceAddressDrawer : PropertyDrawer
    {
		private const string _tooltip = "Prefab has to be in resource folder and included in resource manifest!";
        //private readonly IResourceManifestProvider _resourceManifestProvider = new ResourceManifestProvider();
        private Dictionary<string, Object> _objects = new Dictionary<string, Object>();
        private GUIStyle _redStyle = new GUIStyle(EditorStyles.label);
        private GUIStyle _greenStyle = new GUIStyle(EditorStyles.label);
        private GUIStyle _yellowStyle = new GUIStyle(EditorStyles.label);
        //private ResourceFolderManifest _manifest;

        public ResourceAddressDrawer()
        {
            _redStyle.normal.textColor = Color.red;
            _greenStyle.normal.textColor = Color.green;
            _yellowStyle.normal.textColor = Color.yellow;
            //_manifest = _resourceManifestProvider.GetManifest(true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.isArray && property.propertyType != SerializedPropertyType.String)
            {
                int size = property.arraySize;
                for (int i = 0; i < size; i++)
                {
                    var prop = property.GetArrayElementAtIndex(i);
                    DrawString(prop, position);
                }
            }
            else
            {
                DrawString(property, position);
            }
        }

        private void DrawString(SerializedProperty property, Rect position)
        {
            var rect = position;
            rect.xMax /= 1.5f;
            if (!string.IsNullOrEmpty(property.stringValue) && !_objects.ContainsKey(property.stringValue.ToLower()) 
                                                            //&& _manifest.ContainsKey(property.stringValue.ToLower())
                                                            )
            {
                //var path = _manifest[property.stringValue.ToLower()];
                //var asset = TryGetAsset(path);
                //_objects[property.stringValue.ToLower()] = asset;
            }

            if (!string.IsNullOrEmpty(property.stringValue) && !_objects.ContainsKey(property.stringValue.ToLower()))
            {
                EditorGUI.LabelField(rect, new GUIContent(property.name + ": " + property.stringValue + " (manifest key not found)", _tooltip), _redStyle);
            }
            else if (!string.IsNullOrEmpty(property.stringValue) && _objects.ContainsKey(property.stringValue.ToLower()) && _objects[property.stringValue.ToLower()] == null)
            {
                EditorGUI.LabelField(rect, new GUIContent(property.name + ": " + property.stringValue + " (manifest value not found)", _tooltip), _redStyle);
            }
            else
            {
                EditorGUI.LabelField(rect, property.name + ": " + property.stringValue, !string.IsNullOrEmpty(property.stringValue) ? _greenStyle : _yellowStyle);
            }

            rect.xMin = rect.xMax;
            rect.xMax = position.xMax;
            Object toDraw;
            _objects.TryGetValue(property.stringValue.ToLower(), out toDraw);
            ResourceAddressAttribute cast = (ResourceAddressAttribute) attribute;
            var obj = EditorGUI.ObjectField(rect, "", toDraw, cast.Type, false);
            if (obj != toDraw)
            {
                var assetPath = AssetDatabase.GetAssetPath(obj);
                if (!assetPath.Contains("Resources"))
                {
                    EditorUtility.DisplayDialog("Error", "This asset is not located in the resource folder", "Ok");
                    return;
                }

                var index = assetPath.IndexOf("Resources");
                assetPath = assetPath.Remove(0, index + 10);
                var fileName = Path.GetFileNameWithoutExtension(assetPath);
                index = assetPath.IndexOf(fileName);
                assetPath = assetPath.Remove(index + fileName.Length);
                //foreach (var pair in _manifest)
                {
                    //if (pair.Value == assetPath)
                    {
                        property.stringValue = Path.GetFileName(AssetDatabase.GetAssetPath(obj))?.ToLower();
                        return;
                    }
                }

                Debug.LogError("Can't find key by path. Check the resource manifest. Fallback by prefab name.");
                property.stringValue = Path.GetFileName(AssetDatabase.GetAssetPath(obj))?.ToLower();
            }
        }

        private Object TryGetAsset(string path)
        {
            return Resources.Load(path);
        }
    }
}