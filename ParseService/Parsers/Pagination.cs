using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseService.Parsers
{
	/// <summary>
	/// Представляет настройки разбора для ресурсов с постраничной навигацией.
	/// </summary>
	public abstract class PaginationSettings
	{
		/// <summary>
		/// Возвращает или устанавливает начальную страницу для разбора.
		/// </summary>
		/// <value>
		/// Начальная страница для разбора.
		/// </value>
		public int StartPage
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает конечную страницу для разбора.
		/// </summary>
		/// <value>
		/// Конечная страница для разбора.
		/// </value>
		public int EndPage
		{
			get;
			set;
		}
	}
}
