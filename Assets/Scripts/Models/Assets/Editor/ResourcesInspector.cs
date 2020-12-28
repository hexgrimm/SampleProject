using UnityEditor;
using UnityEngine;

namespace Models.Assets.Editor
{
	[CustomEditor( typeof( AssetsConfiguration ) )]
	public class ResourcesInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			
			base.OnInspectorGUI();
			
			var config = (AssetsConfiguration)target;

			foreach (var resourceIdLinkPair in config.Resources)
			{
				if (resourceIdLinkPair.ResourceType == ResourceType.Bundled)
				{
					resourceIdLinkPair.ResourceLink.DirectLink = null;
				}
				else
				{
					resourceIdLinkPair.ResourceLink.DirectLink =
						AssetDatabase.LoadAssetAtPath<GameObject>(resourceIdLinkPair.ResourceLink.PathInAssets);
				}
			}
		}
	}
}