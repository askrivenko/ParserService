using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseService.Parsers.SeverstalLogoUsingParser;

namespace ParseService
{
	class Program
	{
		static void Main(string[] args)
		{
			var result = new SeverstalLogoUsingParser().ParseAsync();

			Console.ReadKey();
		}
	}
}
