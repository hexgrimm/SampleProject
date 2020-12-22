using EventUtils;
using UnityEngine;

namespace Views
{
	public class LobbyView : ViewBase<LobbyWindowPrefabLinks>, ILobbyView
	{
		private readonly Flag _requestCoinsButton = new Flag();
		private readonly Flag _startGameButton = new Flag();

		public IFlag RequestCoinsButton => _requestCoinsButton;

		public IFlag StartGameButton => _startGameButton;

		public LobbyView(GameObject prefab, Transform parent) : base(prefab, parent)
		{
			
		}

		public void SetCoinsValue(int value)
		{
			PrefabLink.Coins.text = value.ToString();
		}
		
		public void SetDeltaTimeValue(float value)
		{
			PrefabLink.DeltaTime.text = value.ToString("0.00000");
		}


		protected override void ShowInternal()
		{
			PrefabLink.RequestButton.onClick.AddListener(_requestCoinsButton.Raise);
			PrefabLink.StartGameButton.onClick.AddListener(_startGameButton.Raise);
		}

		protected override void HideInternal()
		{
			PrefabLink.RequestButton.onClick.RemoveAllListeners();
			PrefabLink.StartGameButton.onClick.RemoveAllListeners();
		}
	}
}