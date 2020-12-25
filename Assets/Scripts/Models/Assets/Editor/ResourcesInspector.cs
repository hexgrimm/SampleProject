using UnityEditor;

namespace Models.Assets.Editor
{
	[CustomEditor( typeof( AssetsConfiguration ) )]
	public class ResourcesInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			
			//base.OnInspectorGUI();
			
			var config = (AssetsConfiguration)target;
			
			SerializedObject serializedObject = new UnityEditor.SerializedObject(config);
			
			SerializedProperty serializedPropertyArray = serializedObject.FindProperty("Resources");

			if (serializedPropertyArray == null)
			{
				UnityEditor.EditorGUILayout.LabelField("NULL");
				return;
			}
			
			var size = serializedPropertyArray.arraySize;
			for (int i = 0; i < size; i++)
			{
				var element = serializedPropertyArray.GetArrayElementAtIndex(i).objectReferenceValue as ResourceIdLinkPair;
				
				UnityEditor.EditorGUILayout.LabelField(element.propertyType.ToString());
				UnityEditor.EditorGUILayout.LabelField(element.name.ToString());

				var objectOfArray = element.GetEndProperty();

				UnityEditor.EditorGUILayout.LabelField(objectOfArray.propertyType.ToString());
				UnityEditor.EditorGUILayout.LabelField(objectOfArray.name.ToString());
				//EditorGUILayout.PropertyField(p2);
			}
		}
	}
}