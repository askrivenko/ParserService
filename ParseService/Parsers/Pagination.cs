﻿using PommaLabs.Thrower;

namespace ParseService.Parsers
{
	/// <summary>
	/// Представляет настройки разбора для ресурсов с постраничной навигацией.
	/// </summary>
	public abstract class PaginationSettings
	{
		#region Data
		#region Fields
		private int _endPage;
		private int _startPage;
		#endregion
		#endregion

		#region Properties
		/// <summary>
		/// Возвращает или устанавливает конечную страницу для разбора.
		/// </summary>
		/// <value>
		/// Конечная страница для разбора.
		/// </value>
		public int EndPage
		{
			get
			{
				return _endPage;
			}

			set
			{
				Raise.ArgumentException.If(EndPage + StartPage != 0 && value <= StartPage, nameof(EndPage),
										   "Номер конечной страницы для разбора не должен быть меньше, или равным номеру начальной страницы!");
				_endPage = value;
			}
		}

		/// <summary>
		/// Возвращает или устанавливает начальную страницу для разбора.
		/// </summary>
		/// <value>
		/// Начальная страница для разбора.
		/// </value>
		public int StartPage
		{
			get
			{
				return _startPage;
			}

			set
			{
				Raise.ArgumentException.If(value <= 1, nameof(StartPage),
										   "Номер начальной страницы для разбора должен должен быть больше 1!");
				Raise.ArgumentException.If(EndPage + StartPage != 0 && value >= EndPage, nameof(StartPage),
										   "Номер начальной страницы для разбора не должен превышать, или быть равным номеру конечной страницы!");
				_startPage = value;
			}
		}
		#endregion
	}
}
