using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using server.Models;
using server.Models.Context;
using server.Storage;

namespace server.Services
{
  public interface IActivityService
  {
    Task<IEnumerable<VacationActivityMedia>?> GetAllActivityMedia(int vacationId, int activityId);
    Task<VacationActivityMedia?> GetMedia(int vacationId, int activityId, int mediaId);

    Task<StorageBlob?> GetMediaContents(int vacationId, int activityId, int mediaId);
    Task<VacationActivityMedia?> SaveMediaContents(int vacationId, int activityId, Stream contents, string name, string contentType);
  }

  public class ActivityService : IActivityService
  {
    private AppDbContext _context;
    private IStorageClient _storageClient;
    private ILogger _logger;

    public ActivityService(IStorageClient storageClient, AppDbContext context, ILogger<ActivityService> logger)
    {
      _context = context;
      _storageClient = storageClient;
      _logger = logger;
    }

    public async Task<VacationActivityMedia?> SaveMediaContents(int vacationId, int activityId, Stream contents, string name, string contentType)
    {
      // First, save contents to disk
      string uri = name + RandomString(10);
      var blob = new StorageBlob(uri: uri, contentType: contentType, contents: contents);
      bool saveSuccessful = await _storageClient.StoreBlob(blob);

      if (!saveSuccessful)
      {
        _logger.LogError($"Storage client error when trying to save blob: ${name}");
        return null;
      }

      // Then, if that was successful we save to the database and return the new entity
      VacationActivityMedia newMediaRecord = new VacationActivityMedia()
      {
        Id = default,
        VacationId = vacationId,
        ActivityId = activityId,
        Uri = uri,
        ContentType = contentType,
      };

      try
      {
        var result = await _context.VacationActivityMedia.AddAsync(newMediaRecord);
        var saveResult = await result.Context.SaveChangesAsync();

        return result.Entity;
      }
      catch(Exception e)
      {
        _logger.LogError(e, $"Generic error when trying to save new media contents for name: ${name}");

        var _ = await _storageClient.DeleteBlob(uri);
        return null;
      }
    }

    public async Task<StorageBlob?> GetMediaContents(int vacationId, int activityId, int mediaId)
    {
      try
      {
        VacationActivityMedia? mediaRecord = await _context.VacationActivityMedia.FirstOrDefaultAsync((mediaRecord) => mediaRecord.Id == mediaId);
        if (mediaRecord is null)
        {
          _logger.LogError($"Media record for mediaId: ${mediaId} not found");
          return null;
        }

        StorageBlob? blob = await _storageClient.GetBlob(mediaRecord.Uri);
        if (blob is null)
        {
          // something really went wrong. we should probably work on resolving this somehow
          _logger.LogError($"Media record for mediaId: ${mediaId} found in DB, not found in storage client");
          return null;
        }

        return blob;
      }
      catch(Exception e)
      {
        _logger.LogError(e, $"General exception cought when getting media contents for mediaId: ${mediaId}");
        return null;
      }
      
    }

    public async Task<VacationActivityMedia?> GetMedia(int vacationId, int activityId, int mediaId)
    {
      try
      {
        VacationActivityMedia? mediaRecord = await _context.VacationActivityMedia.FirstOrDefaultAsync((mediaRecord) => mediaRecord.Id == mediaId);
        if (mediaRecord is null)
        {
          _logger.LogError($"Media record for mediaId: ${mediaId} not found");
          return null;
        }

        return mediaRecord;
      }
      catch(Exception e)
      {
        _logger.LogError(e, $"General exception cought when getting media record for mediaId: ${mediaId}");
        return null;
      }
    }

    public async Task<IEnumerable<VacationActivityMedia>?> GetAllActivityMedia(int vacationId, int activityId)
    {
      try
      {
        return await _context.VacationActivityMedia.Where((mediaRecord) => mediaRecord.ActivityId == activityId).ToListAsync();
      }
      catch(Exception e)
      {
        _logger.LogError(e, $"DB error when fetching all media records for activity: ${activityId}");
        return null;
      }
    }


    private static Random random = new Random();
    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
  }
}