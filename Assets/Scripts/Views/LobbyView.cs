using EventUtils;
using UnityEngine;

namespace Views
{
	public class LobbyView : ViewBase<LobbyWindowPrefabLinks>, ILobbyView
	{
		private readonly IUpdater _updater;
		private readonly Signal _requestCoinsButton = new Signal();
		private readonly Signal _startGameButton = new Signal();

		public ISignal RequestCoinsButton => _requestCoinsButton;

		public ISignal StartGameButton => _startGameButton;

		public LobbyView(GameObject prefab, Transform parent) : base(prefab, parent)
		{
			
		}

		public void ShowOnLayer(int layerIndex)
		{
			base.Instantiate();
			GameObjectInstance.transform.SetSiblingIndex(layerIndex);
			PrefabLink.RequestButton.onClick.AddListener(_requestCoinsButton.Raise);
			PrefabLink.StartGameButton.onClick.AddListener(_startGameButton.Raise);
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
			if (GameObjectInstance == null)
				return;
			
			PrefabLink.RequestButton.onClick.RemoveAllListeners();
			PrefabLink.StartGameButton.onClick.RemoveAllListeners();
			base.Hide();
		}
	}
}