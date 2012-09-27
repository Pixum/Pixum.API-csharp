using Xunit;

namespace Pixum.API.Test
{
    public class PixumApiTestAlbum : PixumApiTestBase
    {
        [Fact]
        public void Test_Album_Info_GetAlbums()
        {
            // Arrange, Act
            InitApi();
            var albums = _pixumApi.Album.GetAlbums().Result;

            // Assert
            Assert.NotNull(albums);
        }

        [Fact]
        public void Test_Album_Info_GetAlbum()
        {
            // Arrange
            InitApi();
            var login = Login();
            var albums = _pixumApi.Album.GetAlbums().Result;
            var albumId = albums.album.items[0].album_id;

            // Act
            var album = _pixumApi.Album.GetAlbum(albumId).Result;

            // Assert
            Assert.Equal(albumId, album.album_id);
        }

        [Fact]
        public void Test_Album_Info_CreateAlbum_And_DeleteAlbum()
        {
            // Arrange
            InitApi();
            var login = Login();

            // Act
            var album = _pixumApi.Album.CreateAlbum("Test Album").Result;

            // Assert
            Assert.NotNull(album);
            Assert.IsType<int>(album.albumId);

            Assert.DoesNotThrow(() =>
            {
                var result = _pixumApi.Album.DeleteAlbum(album.albumId).Result;
            });
        }

        [Fact]
        public void Test_Album_Info_SetData()
        {
            // Arrange
            InitApi();
            var login = Login();
            var album = _pixumApi.Album.CreateAlbum("Test Album").Result;

            // Act, Assert
            Assert.DoesNotThrow(() =>
            {
                var res = _pixumApi.Album.SetAlbumData(album.albumId, "New Title", "New Description").Result;
            });

            var albumInfo = _pixumApi.Album.GetAlbum(album.albumId.ToString()).Result;
            var deleteResult = _pixumApi.Album.DeleteAlbum(album.albumId).Result;

            Assert.Equal("New Title", albumInfo.title);
            Assert.Equal("New Description", albumInfo.description);
        }

        [Fact]
        public void Test_Album_Images_GetAlbumImages()
        {
            // Arrange
            InitApi();
            var imageData = System.IO.File.ReadAllBytes(@"Testfiles\Image.jpg");
            var uploadResult = _pixumApi.Image.DataCreate(imageData, "Image").Result;

            // Act
            var images = _pixumApi.Album.GetAlbumImages("inbox", 0, 1).Result;

            // Assert
            Assert.Equal(images.meta.limit, 1);
            Assert.Equal(images.meta.start, 0);
            Assert.Equal(images.items.Count, 1);
            Assert.Equal(images.items[0].filename, "Image");
        }
    }
}
