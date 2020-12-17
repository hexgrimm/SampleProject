namespace Presenters.CoreLoop
{
	public interface IUpdateablePresenter
	{
		void PreModelUpdate();
		void PostModelUpdate();
	}
}