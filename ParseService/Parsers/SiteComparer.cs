﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseService.Parsers
{
	public class SiteComparer
		: IEqualityComparer<Site>
	{
		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		/// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
		/// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
		public bool Equals(Site x, Site y)
		{
			if (x == null && y == null)
				return true;
			if (x == null | y == null)
				return false;
			if (x.Uri == y.Uri)
				return true;

			return false;
		}

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <returns>A hash code for the specified object.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
		public int GetHashCode(Site obj)
		{
			return obj.Uri.GetHashCode();
		}
	}
}
