using System.Threading.Tasks;

namespace ParseService
{
	/// <summary>
	/// Предоставляет механизм для разбора интернет ресурса.
	/// </summary>
	/// <typeparam name="TParseResult">Тип возвращаемого значения парсера.</typeparam>
	public interface IParser<TParseResult> where TParseResult : class
	{
		/// <summary>
		/// Возвращает или устанавливает базовый URI ресурса.
		/// </summary>
		/// <value>
		/// Базовый URI ресурса.
		/// </value>
		string BaseUri
		{
			get;
			set;
		}

		/// <summary>
		/// Осуществляет разбор интернет ресурса. Выполняется асинхронно.
		/// </summary>
		/// <typeparam name="TParseResult">Тип возвращаемого значения парсера.</typeparam>
		Task<TParseResult> ParseAsync();
	}
}
