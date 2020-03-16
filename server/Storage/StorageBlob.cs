using System;
using System.IO;

namespace server.Storage
{
  public class StorageBlob
  {
    public string Uri {get;}
    public string ContentType {get;}
    public Stream Contents {get;}

    public StorageBlob(string uri, string contentType, Stream contents)
    {
      Uri = uri;
      ContentType = contentType;
      Contents = contents;
    }
  }
}