using System;
using System.IO;
using System.Threading.Tasks;
using Minio;

namespace server.Storage
{
  public interface IStorageClient
  {
    Task<bool> StoreBlob(string uri, Stream blob);
    Task<Stream?> GetBlob(string uri);
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

    public async Task<bool> StoreBlob(string uri, Stream blob)
    {
      try
      {
        if (!await EnsureBucketExists())
        {
          return false;
        }

        await minio.PutObjectAsync(
          bucketName: minioBucketName,
          objectName: uri,
          data: blob,
          size: blob.Length // Rethink this? When can this break?
        );

        return true;
      }
      catch
      {
        return false;
      }
    }

    public async Task<Stream?> GetBlob(string uri)
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

        await minio.StatObjectAsync(bucketName: minioBucketName, objectName: uri);
        await minio.GetObjectAsync(bucketName: minioBucketName, objectName: uri, cb: streamCallback);

        return ms;
      }
      catch
      {
        return null;
      }
    }
  }
}