using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Equinox.Infra.CrossCutting.Tools.CloudServices.Storage
{
    public class AzureImageStoreService : IImageStorage
    {
        private IConfiguration _configuration;
        private IConfiguration Configuration => _configuration ?? (_configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json")
                                                    .Build());


        public async Task<string> SaveAsync(ProfilePictureViewModel image)
        {
            var container = await GetBlobContainer();

            await RemovePreviousImage(image.Id.Value.ToString(), container);

            var newPicture = await UploadNewOne(image, container);

            return newPicture.StorageUri.PrimaryUri.AbsoluteUri;
        }

        private async Task<CloudBlobContainer> GetBlobContainer()
        {
            var storageCredentials = new StorageCredentials(Configuration.GetSection("Storage").GetSection("AccountName").Value, Configuration.GetSection("Storage").GetSection("AccountKey").Value);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            return container;
        }

        private static async Task<CloudBlockBlob> UploadNewOne(ProfilePictureViewModel file, CloudBlobContainer container)
        {
            // Upload the new one.
            var newImageName = Guid.NewGuid() + file.FileType.Replace("image/", ".");
            var newPicture = container.GetBlockBlobReference(newImageName);
            byte[] imageBytes = Convert.FromBase64String(file.Value);
            newPicture.Properties.ContentType = file.FileType; //.Replace("image/", "");
            await newPicture.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            return newPicture;
        }

        private static async Task RemovePreviousImage(string picture, CloudBlobContainer container)
        {
            // Remove previous image
            if (!string.IsNullOrEmpty(picture))
            {
                var pictureName = Path.GetFileName(picture);
                var oldImage = container.GetBlockBlobReference(pictureName);
                await oldImage.DeleteIfExistsAsync();
            }
        }
    }
}