﻿namespace BundleTransformer.Tests.Less.Translators
{
	using System.Collections.Generic;
	using System.IO;

	using Moq;
	using NUnit.Framework;

	using BundleTransformer.Core;
	using BundleTransformer.Core.Assets;
	using BundleTransformer.Core.FileSystem;
	using BundleTransformer.Less.Configuration;
	using BundleTransformer.Less.Translators;

	[TestFixture]
	public class LessTranslatorTests
	{
		private const string STYLES_DIRECTORY_VIRTUAL_PATH = "/Content/";
		private const string STYLES_DIRECTORY_URL = "/Content/";

		[Test]
		public void FillingOfImportedLessFilePathsIsCorrect()
		{
			// Arrange
			var virtualFileSystemMock = new Mock<IVirtualFileSystemWrapper>();


			string mixinsLessAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH, "Mixins.less");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(mixinsLessAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(mixinsLessAssetVirtualPath))
				.Returns(@"// Border Radius
.border-radius(@radius) {
  -webkit-border-radius: @radius;
     -moz-border-radius: @radius;
          border-radius: @radius;
}

// Visible
.visible()
{
	display: block;
}")
				;


			string testLessImportLessAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH,
				"TestLessImport.less");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(testLessImportLessAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(testLessImportLessAssetVirtualPath))
				.Returns(@"@alt-bg-color: #143352;

.translators #less
{
	background-color: @alt-bg-color;
}

.icon-headphone
{
	display: inline;
	background-image: data-uri('headphone.gif');
}

@import (multiple) url(""TestLessSubImport1.less"");
@import 'TestLessSubImport2';")
				;


			string headphoneGifAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH, "headphone.gif");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(headphoneGifAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileStream(headphoneGifAssetVirtualPath))
				.Returns(new MemoryStream(new byte[0]))
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileBinaryContent(headphoneGifAssetVirtualPath))
				.Returns(new byte[0])
				;


			string testLessSubImport1LessAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH,
				"TestLessSubImport1.less");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(testLessSubImport1LessAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(testLessSubImport1LessAssetVirtualPath))
				.Returns(@"@import url(http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext);

@border-color: #143352;

.translators #less
{
	border-color: @border-color;
}

.icon-network
{
	display: inline;
	background-image: data-uri('image/png', ""@network.png"");
}

@import url(""TagIcon.css"");")
				;


			string networkPngAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH, "@network.png");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(networkPngAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileStream(networkPngAssetVirtualPath))
				.Returns(new MemoryStream(new byte[0]))
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileBinaryContent(networkPngAssetVirtualPath))
				.Returns(new byte[0])
				;


			string tagIconCssAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH, "TagIcon.css");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(tagIconCssAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(tagIconCssAssetVirtualPath))
				.Returns(@".icon-tag
{
	display: inline;
	background-image: url(tag.png) !important;
}")
				;


			string testLessSubImport2LessAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH,
				"TestLessSubImport2.less");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(testLessSubImport2LessAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(testLessSubImport2LessAssetVirtualPath))
				.Returns(@"@import 'http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext';
@import (css) ""UsbFlashDriveIcon"";
@import (less) ""ValidationIcon.css"";")
				;


			string usbFlashDriveIconCssAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH, 
				"UsbFlashDriveIcon.css");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(usbFlashDriveIconCssAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(usbFlashDriveIconCssAssetVirtualPath))
				.Returns(@".icon-usb-flash-drive
{
	display: inline;
	background-image: ""usb-flash-drive.png"" !important;
}")
				;


			string validationIconCssAssetVirtualPath = Utils.CombineUrls(STYLES_DIRECTORY_VIRTUAL_PATH, 
				"ValidationIcon.css");
			virtualFileSystemMock
				.Setup(fs => fs.FileExists(validationIconCssAssetVirtualPath))
				.Returns(true)
				;
			virtualFileSystemMock
				.Setup(fs => fs.GetFileTextContent(validationIconCssAssetVirtualPath))
				.Returns(@".icon-validation
{
	display: inline;
	background-image: url('validation.png') !important;
}")
				;

			IVirtualFileSystemWrapper virtualFileSystemWrapper = virtualFileSystemMock.Object;
			IRelativePathResolver relativePathResolver = new MockRelativePathResolver();
			var lessConfig = new LessSettings();

			var lessTranslator = new LessTranslator(virtualFileSystemWrapper,
				relativePathResolver, lessConfig);

			const string assetContent = @"@import (once) ""/Content/Mixins.less"";
@import url(""data:text/css;base64,Ym9keSB7IGJhY2tncm91bmQtY29sb3I6IGxpbWUgIWltcG9ydGFudDsgfQ=="");

@bg-color: #7AC0DA;
@caption-color: #FFFFFF;
@monitor-icon-url: ""monitor.png"";

.translators #less
{
	float: left;
	.visible();
	padding: 0.2em 0.5em 0.2em 0.5em;
	background-color: @bg-color;
	color: @caption-color;
	font-weight: bold;
	border: 1px solid @bg-color;
	.border-radius(5px);
}

.icon-monitor
{
	display: inline;
	background-image: url(""@{monitor-icon-url}"");
}

@import (once) ""/Content/TestLessImport.less"";";
			string assetUrl = Utils.CombineUrls(STYLES_DIRECTORY_URL, "TestLess.less");
			var dependencies = new List<Dependency>();

			// Act
			lessTranslator.FillDependencies(assetUrl, assetContent, assetUrl, dependencies);

			// Assert
			Assert.AreEqual(10, dependencies.Count);
			Assert.AreEqual(mixinsLessAssetVirtualPath, dependencies[0].VirtualPath);
			Assert.AreEqual(headphoneGifAssetVirtualPath, dependencies[1].VirtualPath);
			Assert.AreEqual(testLessImportLessAssetVirtualPath, dependencies[2].VirtualPath);
			Assert.AreEqual(networkPngAssetVirtualPath, dependencies[3].VirtualPath);
			Assert.AreEqual(testLessSubImport1LessAssetVirtualPath, dependencies[4].VirtualPath);
			Assert.AreEqual("http://fonts.googleapis.com/css?family=Limelight&subset=latin,latin-ext",
				dependencies[5].VirtualPath);
			Assert.AreEqual(tagIconCssAssetVirtualPath, dependencies[6].VirtualPath);
			Assert.AreEqual(testLessSubImport2LessAssetVirtualPath, dependencies[7].VirtualPath);
			Assert.AreEqual(usbFlashDriveIconCssAssetVirtualPath, dependencies[8].VirtualPath);
			Assert.AreEqual(validationIconCssAssetVirtualPath, dependencies[9].VirtualPath);
		}

		private class MockRelativePathResolver : IRelativePathResolver
		{
			public string ResolveRelativePath(string basePath, string relativePath)
			{
				return Utils.CombineUrls(STYLES_DIRECTORY_URL, relativePath);
			}
		}
	}
}