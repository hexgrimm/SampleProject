namespace Presenters
{
	public interface IUpdateablePresenter
	{
		void PreModelUpdate();
		void PostModelUpdate();
	}
}