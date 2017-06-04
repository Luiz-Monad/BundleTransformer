﻿using System.Runtime.CompilerServices;

namespace BundleTransformer.Core.Utilities
{
	/// <summary>
	/// Extensions for Char
	/// </summary>
	public static class CharExtensions
	{
		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsWhitespace(this char source)
		{
			return source == ' ' || (source >= '\t' && source <= '\r');
		}

		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsNumeric(this char source)
		{
			return source >= '0' && source <= '9';
		}

		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsAlphaLower(this char source)
		{
			return source >= 'a' && source <= 'z';
		}

		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsAlphaUpper(this char source)
		{
			return source >= 'A' && source <= 'Z';
		}

		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsAlpha(this char source)
		{
			return IsAlphaLower(source) || IsAlphaUpper(source);
		}

		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool IsAlphaNumeric(this char source)
		{
			return IsAlpha(source) || IsNumeric(source);
		}
	}
}