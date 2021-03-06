﻿using System.Configuration;

using BundleTransformer.Core.Configuration;

namespace BundleTransformer.Autoprefixer.Configuration
{
	/// <summary>
	/// Configuration settings of Andrey Sitnik's Autoprefix CSS postprocessor
	/// </summary>
	public sealed class AutoprefixerSettings : ConfigurationSection
	{
		/// <summary>
		/// Gets a list of browser conditional expressions
		/// </summary>
		[ConfigurationProperty("browsers", IsRequired = false)]
		public BrowserConditionalExpressionCollection Browsers
		{
			get { return (BrowserConditionalExpressionCollection)this["browsers"]; }
		}

		/// <summary>
		/// Gets or sets a flag for whether to create nice visual cascade of prefixes
		/// </summary>
		[ConfigurationProperty("cascade", DefaultValue = true)]
		public bool Cascade
		{
			get { return (bool)this["cascade"]; }
			set { this["cascade"] = value; }
		}

		/// <summary>
		/// Gets or sets a flag for whether to add new prefixes
		/// </summary>
		[ConfigurationProperty("add", DefaultValue = true)]
		public bool Add
		{
			get { return (bool)this["add"]; }
			set { this["add"] = value; }
		}

		/// <summary>
		/// Gets or sets a flag for whether to remove outdated prefixes
		/// </summary>
		[ConfigurationProperty("remove", DefaultValue = true)]
		public bool Remove
		{
			get { return (bool)this["remove"]; }
			set { this["remove"] = value; }
		}

		/// <summary>
		/// Gets or sets a flag for whether to add prefixes for <code>@supports</code> parameters
		/// </summary>
		[ConfigurationProperty("supports", DefaultValue = true)]
		public bool Supports
		{
			get { return (bool)this["supports"]; }
			set { this["supports"] = value; }
		}

		/// <summary>
		/// Gets or sets a mode that defines should Autoprefixer add prefixes for flexbox properties
		/// </summary>
		[ConfigurationProperty("flexbox", DefaultValue = "All")]
		public FlexboxMode Flexbox
		{
			get { return (FlexboxMode)this["flexbox"]; }
			set { this["flexbox"] = value; }
		}

		/// <summary>
		/// Gets or sets a mode that defines should Autoprefixer add IE 10-11 prefixes for Grid Layout properties
		/// </summary>
		[ConfigurationProperty("grid", DefaultValue = GridMode.None)]
		public GridMode Grid
		{
			get { return (GridMode)this["grid"]; }
			set { this["grid"] = value; }
		}

		/// <summary>
		/// Gets or sets a flag for whether to do not raise error on unknown browser version in
		/// the <code>Browsers</code> property
		/// </summary>
		[ConfigurationProperty("ignoreUnknownVersions", DefaultValue = false)]
		public bool IgnoreUnknownVersions
		{
			get { return (bool)this["ignoreUnknownVersions"]; }
			set { this["ignoreUnknownVersions"] = value; }
		}

		/// <summary>
		/// Gets or sets a virtual path to file, that contains custom usage statistics for
		/// <code>&gt; 10% in my stats</code> browsers query
		/// </summary>
		[ConfigurationProperty("stats", DefaultValue = "")]
		public string Stats
		{
			get { return (string)this["stats"]; }
			set { this["stats"] = value; }
		}

		/// <summary>
		/// Gets a configuration settings of JS engine
		/// </summary>
		[ConfigurationProperty("jsEngine")]
		public JsEngineSettings JsEngine
		{
			get { return (JsEngineSettings)this["jsEngine"]; }
		}
	}
}