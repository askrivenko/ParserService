using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace ParseService.Parsers.SeverstalLogoUsingParser
{
	/// <summary>
	/// Представляет SeverstalLogoUsingParser
	/// </summary>
	/// <seealso cref="ParseService.Parsers.PaginationSettings" />
	/// <seealso cref="Site" />
	public class SeverstalLogoUsingParser : PaginationSettings, IParser<IEnumerable<Site>>
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
		/// Инициализирует новый экземпляр <see cref="SeverstalLogoUsingParser" />.
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
			StartPage = 2;
			EndPage = 3;
		}
		#endregion

		#region IParser<IEnumerable<Site>> members
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
		public async Task<IEnumerable<Site>> ParseAsync()
		{
			var sites = new List<Site>();

			var options = new ChromeOptions();
			options.AddArguments(new List<string>() { "headless" });
			var browser = new ChromeDriver(options);

			try
			{
				foreach (var imageUrl in _corporateLogosLink)
				{
					var fullLogoLink = BaseUri + imageUrl;

					sites.AddRange(await GetSitesWithLogoAsync(browser, fullLogoLink));

					var nullPageLink = "";
					var paginationLinks = new List<string>
					{
						nullPageLink,
						nullPageLink
					};

					paginationLinks.AddRange(browser.FindElementsByClassName("fl")
													.Where(a => a.TagName == "a")
													.Where(a => !string.IsNullOrEmpty(a.Text))
													.Where(a => a.GetAttribute("aria-label")
																 ?.Contains("Page") != null)
													.Select(a => a.GetAttribute("href")));

					if (StartPage < paginationLinks.Count &&
						EndPage <= paginationLinks.Count)
					{
						for (var i = StartPage; i < EndPage; i++)
						{
							sites.AddRange(await GetSitesWithLogoAsync(browser, paginationLinks[i]));
						}
					}
					else
					{
						foreach (var page in paginationLinks)
						{
							if (string.IsNullOrWhiteSpace(page))
								continue;

							sites.AddRange(await GetSitesWithLogoAsync(browser, page));
						}
					}
				}
			}
			catch(Exception ex)
			{
				var s = ex.Message;
				return sites.Distinct(new SiteComparer())
							.ToArray();
			}
			finally
			{
				browser.Quit();
			}

			return sites.Distinct(new SiteComparer())
						.ToArray();
		}
		#endregion

		#region Private
		/// <summary>
		/// Получает сведения об интернет ресурсах из поисковой выдачи по заданному изображению. Выполняется асинхронно.
		/// </summary>
		/// <param name="browser">Драйвер браузера.</param>
		/// <param name="url">URL изображения.</param>
		/// <returns></returns>
		private async Task<List<Site>> GetSitesWithLogoAsync(RemoteWebDriver browser, string url)
		{
			return await Task.Run(() =>
				{
					var sites = new List<Site>();
					browser.Navigate()
						   .GoToUrl(url);
					var divs = browser.FindElementsByClassName("rc");
					foreach (var div in divs)
					{
						var a = div.FindElement(By.TagName("a"));
						sites.Add(new Site
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
