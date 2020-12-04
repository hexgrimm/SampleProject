namespace Views
{
	public interface ILoadingWindowView
	{
		void EnableSpinnerRotation();
		void ShowOnLayer(int layerIndex);
		void Hide();
	}
}