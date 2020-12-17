using EventUtils;
using UnityEngine;

namespace Views
{
	public class GameWindow : ViewBase<GameWindowPrefabLinks>, IGameWindow
	{
		private readonly Signal _quitGameButton = new Signal();
		
		public ISignal QuitGameButton => _quitGameButton;
		
		public GameWindow(GameObject prefab, Transform parent) : base(prefab, parent)
		{
			
		}

		public void ShowOnLayer(int layerIndex)
		{
			base.Instantiate();
			PrefabLink.QuitGameButton.onClick.AddListener(_quitGameButton.Raise);
		}

		public override void Hide()
		{
			if (GameObjectInstance == null)
				return;
			
			PrefabLink.QuitGameButton.onClick.RemoveAllListeners();
			base.Hide();
		}
	}
}