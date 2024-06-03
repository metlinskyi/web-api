using Microsoft.AspNetCore.DataProtection.KeyManagement;

public interface ICacheReader<TValue>
{
   bool TryGet(ICacheKey key, out  TValue value);
}