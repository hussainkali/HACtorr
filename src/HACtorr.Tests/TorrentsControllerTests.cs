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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using HACtorr.Controllers;
    using HACtorr.Framework;
    using HACtorr.Framework.Torrents;
    using HACtorr.ViewModels.Torrents;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
	public class TorrentsControllerTests
	{
        private Mock<ITorrentService> torrentService;

		[Test]
        public void ShouldUseTorrentServiceToReturnTorrentsFromIndex()
        {
            // Arrange
            TorrentsController controller = this.CreateController();

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
            TorrentsController controller = this.CreateController();

            // Act
            controller.AddUrl(new AddUrlViewModel { TorrentUrl = "abc" });

            // Assert
            torrentService.Verify(t => t.AddTorrent("abc"));
        }

        [Test]
        public void ShouldNotAddTorrentFromUrlIfModelStateIsInvalid()
        {
            // Arrange
            var controller = this.CreateController();

            controller.ModelState.AddModelError("", "The model is invalid");

            // Act
            controller.AddUrl(new AddUrlViewModel { TorrentUrl = "abc" });

            // Assert
            torrentService.Verify(t => t.AddTorrent("abc"), Times.Never());
        }

        [Test]
        public void ShouldBeAbleToAddMultipleTorrentsFromFile()
        {
            // Arrange
            var controller = this.CreateController();
            var file1 = new Mock<HttpPostedFileBase>();
            file1.Setup(f => f.FileName).Returns("FileName1");
            file1.Setup(f => f.ContentLength).Returns(100);
            var file2 = new Mock<HttpPostedFileBase>();
            file2.Setup(f => f.FileName).Returns("FileName2");
            file2.Setup(f => f.ContentLength).Returns(100);

            // Act
            controller.AddTorrents(new[] { file1.Object, file2.Object });

            // Assert
            torrentService.Verify(
                s => s.AddTorrents(
                    It.Is<IEnumerable<IFileContainer>>(
                    files => files.ElementAt(0).FileName == "FileName1" &&
                        files.ElementAt(1).FileName == "FileName2")));
        }

        [Test]
        public void ShouldNotAddAFileIfTheContentIsEmpty()
        {
            // Arrange
            var controller = this.CreateController();
            var file1 = new Mock<HttpPostedFileBase>();
            file1.Setup(f => f.ContentLength).Returns(0);
            
            var file2 = new Mock<HttpPostedFileBase>();
            file2.Setup(f => f.ContentLength).Returns(100);

            // Act
            controller.AddTorrents(new[] { file1.Object, file2.Object });

            // Assert
            torrentService.Verify(
                s => s.AddTorrents(It.Is<IEnumerable<IFileContainer>>(files => files.Count() == 1)));
        }

        private TorrentsController CreateController()
        {
            this.torrentService = new Mock<ITorrentService>();

            return new TorrentsController(torrentService.Object);
        }
	}
}