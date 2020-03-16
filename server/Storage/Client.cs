using System;
using System.IO;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel;

namespace server.Storage
{
  public interface IStorageClient
  {
    Task<bool> StoreBlob(StorageBlob blob);
    Task<bool> DeleteBlob(string uri);
    Task<StorageBlob?> GetBlob(string uri);
  }

  public class StorageClient : IStorageClient
  {
    // Yes this should be in a config file or some secret store but I'm not setting that up right now
    private const string minioEndpoint = "minio:9000";
    private const string minioAccessKey = "minio";
    private const string minioSecretKey = "minio123";
    private const string minioBucketName = "vacation-archive";
    private const string minioRegion = "";
    private readonly MinioClient minio = new MinioClient(
      endpoint: minioEndpoint,
      accessKey: minioAccessKey,
      secretKey: minioSecretKey,
      region: minioRegion
    );

    private async Task<bool> EnsureBucketExists()
    {
      try
      {
        bool found = await minio.BucketExistsAsync(minioBucketName);
        if (!found)
        {
          await minio.MakeBucketAsync(minioBucketName, location: minioRegion);
        }

        return true;
      }
      catch(Exception e)
      {
        Console.WriteLine(e);
        return false;
      }
    }

    public async Task<bool> StoreBlob(StorageBlob blob)
    {
      try
      {
        if (!await EnsureBucketExists())
        {
          return false;
        }

        await minio.PutObjectAsync(
          bucketName: minioBucketName,
          objectName: blob.Uri,
          data: blob.Contents,
          size: blob.Contents.Length, // Rethink this? When can this break?
          contentType: blob.ContentType
        );

        return true;
      }
      catch
      {
        return false;
      }
    }

    public async Task<StorageBlob?> GetBlob(string uri)
    {
      try
      {
        if (!await EnsureBucketExists())
        {
          return null;
        }

        MemoryStream ms = new MemoryStream();
        Action<Stream> streamCallback = (Stream stream) => {
          stream.CopyTo(ms);
        };

        var metadata = await minio.StatObjectAsync(bucketName: minioBucketName, objectName: uri);
        await minio.GetObjectAsync(bucketName: minioBucketName, objectName: uri, cb: streamCallback);

        return new StorageBlob(uri: uri, contentType: metadata.ContentType, contents: ms);
      }
      catch
      {
        return null;
      }
    }
  
    public async Task<bool> DeleteBlob(string uri)
    {
      try
      {
        await minio.RemoveObjectAsync(minioBucketName, uri);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}