using System.Collections.Generic;
using System.Threading.Tasks;
public interface ISSWNsService
{ 
    Task<SSWNs> GetSSWNsAsync(string id);
    Task InsertOrUpdateSSWNsAsync(string user_id, SSWNs SSWNs, string id);
    Task<SSWNs> GetSumSSWNsAsync(string user_id);
    SSWNs EnhanceSSWNs(SSWNs research, int level, int multiplier = 1);
}