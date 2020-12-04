using EventUtils;
using UnityEngine;

namespace Views
{
	public class LobbyView : ViewBase<LobbyWindowPrefabLinks>, ILobbyView
	{
		private readonly IUpdater _updater;
		private readonly Signal _requestCoinsButton = new Signal();
		
		public ISignal RequestCoinsButton => _requestCoinsButton;

		public LobbyView(GameObject prefab, Transform parent) : base(prefab, parent)
		{
			
		}

		public void ShowOnLayer(int layerIndex)
		{
			base.Show();
			GameObjectInstance.transform.SetSiblingIndex(layerIndex);
			PrefabLink.RequestButton.onClick.AddListener(_requestCoinsButton.Raise);
		}

		public void SetCoinsValue(int value)
		{
			PrefabLink.Coins.text = value.ToString();
		}
		
		public void SetDeltaTimeValue(float value)
		{
			PrefabLink.DeltaTime.text = value.ToString("0.00000");
		}

		public override void Hide()
		{
			PrefabLink.RequestButton.onClick.RemoveAllListeners();
			base.Hide();
		}
	}
}