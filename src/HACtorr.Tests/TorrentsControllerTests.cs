/*
 * Created by SharpDevelop.
 * User: Hussain
 * Date: 07/08/2012
 * Time: 23:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace HACtorr.Tests
{
    using System.Web.Mvc;
    using System.Linq;

    using HACtorr.Controllers;
    using HACtorr.ViewModels.Torrents;
    using HACtorr.Framework.Torrents;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
	public class TorrentsControllerTests
	{
		[Test]
        public void ShouldUseTorrentServiceToReturnTorrentsFromIndex()
        {
            // Arrange
            Mock<ITorrentService> torrentService = new Mock<ITorrentService>();
            TorrentsController controller = new TorrentsController(torrentService.Object);

            torrentService.Setup(s => s.GetTorrents()).Returns(new[] { new TorrentInfo { Name = "ABC" } });

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            var viewModel = (TorrentsViewModel)result.Model;
            viewModel.Torrents.ElementAt(0);
            Assert.That(viewModel.Torrents.ElementAt(0).Name, Is.EqualTo("ABC"));
        }

        [Test]
        public void ShouldBeAbleToAddTorrentFromUrl()
        {
            // Arrange
            Mock<ITorrentService> torrentService = new Mock<ITorrentService>();
            TorrentsController controller = new TorrentsController(torrentService.Object);

            // Act
            controller.AddUrl(new AddUrlViewModel { TorrentUrl = "abc" });

            // Assert
            torrentService.Verify(t => t.AddTorrent("abc"));
        }

        [Test]
        public void ShouldNotAddTorrentFromUrlIfModelStateIsInvalid()
        {
            // Arrange
            Mock<ITorrentService> torrentService = new Mock<ITorrentService>();
            TorrentsController controller = new TorrentsController(torrentService.Object);

            controller.ModelState.AddModelError("", "The model is invalid");

            // Act
            controller.AddUrl(new AddUrlViewModel { TorrentUrl = "abc" });

            // Assert
            torrentService.Verify(t => t.AddTorrent("abc"), Times.Never());
        }
	}
}