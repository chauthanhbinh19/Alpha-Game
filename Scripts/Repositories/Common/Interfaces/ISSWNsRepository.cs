using System.Collections.Generic;
using System.Threading.Tasks;
public interface ISSWNsRepository
{
    Task<SSWNs> GetSSWNsAsync(string type);
    Task InsertOrUpdateSSWNsAsync(string user_id, SSWNs SSWNs, string id);
    Task<SSWNs> GetSumSSWNsAsync(string user_id);
}