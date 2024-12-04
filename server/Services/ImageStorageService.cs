using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using System;
using Microsoft.AspNetCore.Identity;

namespace server.Services;

public class ImageStorageService(AppDbContext dbContext) : IImageStorageService
{
    private const string ImageStorageFolder = "imgs/";
    public async Task<MediaFile> StoreImageAsync(IFormFile? image, User? loggedUser)
    {
        if (!Directory.Exists(ImageStorageFolder)) Directory.CreateDirectory(ImageStorageFolder);

        MediaFile mediaFile = new ();

        var persist = await dbContext.MediaFiles.AddAsync( mediaFile );

        await dbContext.SaveChangesAsync();

        string path = ImageStorageFolder + persist.Entity.Id;

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            if (image == null)
                await stream.WriteAsync(Array.Empty<byte>());
            else 
                await image.CopyToAsync(stream);
        }

        persist.Entity.CreatedDate = DateTime.Now;
        persist.Entity.Path = path;

        persist.Entity.CreatedBy = loggedUser;

        await dbContext.SaveChangesAsync();
        return persist.Entity;
    }

    public void DeleteImage(MediaFile mediaFile)
    {
        if (File.Exists(mediaFile.Path))
        {
            File.Delete(mediaFile.Path);
        }
    }

    public async Task<(byte[], MediaFile?)> RetriveImageAsync(int mediaFileId)
    {
        var mediaFile = await dbContext.MediaFiles.FindAsync(mediaFileId);

        if (mediaFile == null || mediaFile.Path == "") return ([], mediaFile);

        if (File.Exists(mediaFile.Path))
        {
            return (await File.ReadAllBytesAsync(mediaFile.Path), mediaFile);
        }
        else
        {
            throw new FileNotFoundException("Image was not found!");
        }

    }
}

public interface IImageStorageService
{
    public Task<MediaFile> StoreImageAsync(IFormFile? image, User? loggeruser);
    public Task<(byte[], MediaFile?)> RetriveImageAsync(int mediaFile);
    void DeleteImage(MediaFile mediaFile);
}