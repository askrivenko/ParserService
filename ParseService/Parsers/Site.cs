namespace ParseService.Parsers
{
	/// <summary>
	/// Представляет Site
	/// </summary>
	public class Site
	{
		/// <summary>
		/// Возвращает или устанавливает заголовок сайта.
		/// </summary>
		/// <value>
		///Заголовок.
		/// </value>
		public string Title
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает Uri сайта.
		/// </summary>
		/// <value>
		/// URI.
		/// </value>
		public string Uri
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает описание.
		/// </summary>
		/// <value>
		/// Описание.
		/// </value>
		public string Description
		{
			get;
			set;
		}
	}
}
