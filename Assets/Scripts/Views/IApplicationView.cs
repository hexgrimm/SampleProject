namespace Views
{
	public interface IApplicationView //endpoint to reach any view in application. Encapsulates Unity inside every TView
	{
		TView GetView<TView>();
	}
}