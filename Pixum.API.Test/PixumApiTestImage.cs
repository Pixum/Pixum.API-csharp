using Pixum.API.Services;
using Xunit;

namespace Pixum.API.Test
{
    public class PixumApiTestImage : PixumApiTestBase
    {
        [Fact]
        public void Test_Image_Data_Create_And_Delete()
        {
            // Arrange
            InitApi();
            var imageData = System.IO.File.ReadAllBytes(@"Testfiles\Image.jpg");

            // Act
            var uploadResult = _pixumApi.Image.DataCreate(imageData, "Image").Result;
            
            // Assert
            Assert.NotNull(uploadResult);
            Assert.IsType<int>(uploadResult.imageid);
            Assert.DoesNotThrow(() =>
            {
                var deleteResult = _pixumApi.Image.DataDelete(uploadResult.imageid).Result;
            });
        }

        [Fact]
        public void Test_Image_Data_SetTitle_SetDescription_SetRotation()
        {
            // Arrange
            InitApi();
            var imageData = System.IO.File.ReadAllBytes(@"Testfiles\Image.jpg");
            var uploadResult = _pixumApi.Image.DataCreate(imageData, "Image").Result;

            // Act
            _pixumApi.Image.SetTitle(uploadResult.imageid, "New title").Wait();
            _pixumApi.Image.SetDescription(uploadResult.imageid, "New description").Wait();
            _pixumApi.Image.SetRotation(uploadResult.imageid, ImageRotation.DEGREE_180).Wait();
            var result = _pixumApi.Image.GetImage(uploadResult.imageid).Result;

            // Assert
            Assert.Equal(result.title, "New title");
            Assert.Equal(result.description, "New description");
            Assert.Equal(result.rotation, (int)ImageRotation.DEGREE_180);
        }

        [Fact]
        public void Test_Image_Data_GetImage()
        {
            // Arrange
            InitApi();
            var imageData = System.IO.File.ReadAllBytes(@"Testfiles\Image.jpg");
            var uploadResult = _pixumApi.Image.DataCreate(imageData, "Image").Result;

            // Act
            var result = _pixumApi.Image.GetImage(uploadResult.imageid).Result;

            // Assert
            Assert.Equal(result.imageid, uploadResult.imageid);
        }
    }
}
