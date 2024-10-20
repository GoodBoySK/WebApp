using server.Data;
using server.Models;
using System;

namespace server.Services;

public class ImageStorageService(AppDbContext dbContext) : IImageStorageService
{
    public Task<MediaFile?> StoreImageAsync(IFormFile image)
    {
            throw new NotImplementedException();
    }

    public void DeleteImage(MediaFile mediaFile)
    {
        if (File.Exists(mediaFile.Path))
        {
            File.Delete(mediaFile.Path);
        }
    }

    public async Task<byte[]> RetriveImageAsync(MediaFile mediaFile)
    {
            if (File.Exists(mediaFile.Path))
            {
                return await File.ReadAllBytesAsync(mediaFile.Path);
            }
            else
            {
                throw new FileNotFoundException("Image was not found!");
            }

    }
}

public interface IImageStorageService
{
    public Task<MediaFile?> StoreImageAsync(IFormFile image);
    public Task<byte[]> RetriveImageAsync(MediaFile mediaFile);
    void DeleteImage(MediaFile mediaFile);
}