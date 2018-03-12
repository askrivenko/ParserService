using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace ParseService.Parsers.SeverstalLogoUsingParser
{
	/// <summary>
	/// Представляет SeverstalLogoUsingParser
	/// </summary>
	/// <seealso cref="ParseService.Parsers.PaginationSettings" />
	/// <seealso cref="ParseService.IParser{System.Collections.Generic.IEnumerable{ParseService.Parsers.SeverstalLogoUsingParser.SiteWithLogo}}" />
	public class SeverstalLogoUsingParser : PaginationSettings, IParser<IEnumerable<SiteWithLogo>>
	{
		#region Data
		#region Fields
		/// <summary>
		/// The corporate logos link
		/// </summary>
		private readonly List<string> _corporateLogosLink;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="SeverstalLogoUsingParser"/>.
		/// </summary>
		public SeverstalLogoUsingParser()
		{
			_corporateLogosLink = new List<string>
			{
				"http://www.severstal.com/files/4474/2logoeng.png",
				"http://www.severstal.com/files/4471/2logorus.png",
				"http://www.severstal.com/files/4470/2logoslogrus.png",
				"http://www.severstal.com/files/4473/2logoslogeng.png"
			};

			EndPage = 5;
		}
		#endregion

		#region IParser<IEnumerable<SiteWithLogo>> members
		/// <summary>
		/// Возвращает или устанавливает базовый URI ресурса.
		/// </summary>
		/// <value>
		/// Базовый URI ресурса.
		/// </value>
		public string BaseUri
		{
			get;
			set;
		} = "http://images.google.com/searchbyimage?image_url=";

		/// <summary>
		/// Осуществляет разбор интернет ресурса. Выполняется асинхронно.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<SiteWithLogo>> ParseAsync()
		{
			var sites = new List<SiteWithLogo>();
			var browser = new ChromeDriver();

			foreach (var imageUrl in _corporateLogosLink)
			{
				sites = await GetSitesWithLogoAsync(browser, BaseUri + imageUrl);

				var paginationLinks = browser.FindElementsByClassName("fl")
											 .Where(element => element.TagName == "a" && !string.IsNullOrEmpty(element.Text))
											 .Select(element => element.GetAttribute("href"))
											 .ToList();

				if (paginationLinks.Count > EndPage)
				{
					paginationLinks = paginationLinks.Take(EndPage)
													 .ToList();
				}

				foreach (var link in paginationLinks)
				{
					sites.AddRange(await GetSitesWithLogoAsync(browser, link));
				}
			}

			return sites.ToArray();
		}
		#endregion

		#region Private
		/// <summary>
		/// Получает сведения об интернет ресурсах из поисковой выдачи по заданному изображению. Выполняется асинхронно.
		/// </summary>
		/// <param name="browser">Драйвер браузера.</param>
		/// <param name="url">URL изображения.</param>
		/// <returns></returns>
		private async Task<List<SiteWithLogo>> GetSitesWithLogoAsync(RemoteWebDriver browser, string url)
		{
			return await Task.Run(() =>
				{
					var sites = new List<SiteWithLogo>();
					browser.Navigate()
						   .GoToUrl(url);
					var divs = browser.FindElementsByClassName("rc");
					foreach (var div in divs)
					{
						var a = div.FindElement(By.TagName("a"));
						sites.Add(new SiteWithLogo
						{
							Title = a.Text,
							Uri = a.GetAttribute("href"),
							Description = div.FindElement(By.ClassName("st"))
											 .Text
						});
					}

					return sites;
				});
		}
		#endregion
	}
}
