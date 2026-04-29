using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class FeaturesRepository : IFeaturesRepository
{
    public async Task<Dictionary<string, Features>> GetFeaturesByTypeAsync(string type)
    {
        Dictionary<string, Features> features = new Dictionary<string, Features>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT id, feature_name, required_level FROM features WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        Features feature = new Features
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureResearchDTO>> GetResearchFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureResearchDTO> features = new Dictionary<string, FeatureResearchDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.research_level, 0) AS research_level
            FROM features f
            LEFT JOIN researchs r on f.id = r.research_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureResearchDTO feature = new FeatureResearchDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureArchiveDTO>> GetArchiveFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureArchiveDTO> features = new Dictionary<string, FeatureArchiveDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.archive_level, 0) AS archive_level
            FROM features f
            LEFT JOIN archives r on f.id = r.archive_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureArchiveDTO feature = new FeatureArchiveDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureUniverseDTO>> GetUniverseFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureUniverseDTO> features = new Dictionary<string, FeatureUniverseDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.universe_level, 0) AS universe_level
            FROM universe f
            LEFT JOIN researchs r on f.id = r.universe_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureUniverseDTO feature = new FeatureUniverseDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHIINDTO>> GetHIINFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHIINDTO> features = new Dictionary<string, FeatureHIINDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hiin_level, 0) AS hiin_level
            FROM features f
            LEFT JOIN hiins r on f.id = r.hiin_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHIINDTO feature = new FeatureHIINDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureSSWNDTO>> GetSSWNFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureSSWNDTO> features = new Dictionary<string, FeatureSSWNDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.sswn_level, 0) AS sswn_level
            FROM features f
            LEFT JOIN sswns r on f.id = r.sswn_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureSSWNDTO feature = new FeatureSSWNDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHITNDTO>> GetHITNFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHITNDTO> features = new Dictionary<string, FeatureHITNDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hitn_level, 0) AS hitn_level
            FROM features f
            LEFT JOIN hitns r on f.id = r.hitn_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHITNDTO feature = new FeatureHITNDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHIHNDTO>> GetHIHNFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHIHNDTO> features = new Dictionary<string, FeatureHIHNDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hihn_level, 0) AS hihn_level
            FROM features f
            LEFT JOIN hihns r on f.id = r.hihn_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHIHNDTO feature = new FeatureHIHNDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHIENDTO>> GetHIENFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHIENDTO> features = new Dictionary<string, FeatureHIENDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hien_level, 0) AS hien_level
            FROM features f
            LEFT JOIN hiens r on f.id = r.hien_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHIENDTO feature = new FeatureHIENDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHICADTO>> GetHICAFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHICADTO> features = new Dictionary<string, FeatureHICADTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hica_level, 0) AS hica_level
            FROM features f
            LEFT JOIN hicas r on f.id = r.hica_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHICADTO feature = new FeatureHICADTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHIRNDTO>> GetHIRNFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHIRNDTO> features = new Dictionary<string, FeatureHIRNDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hirn_level, 0) AS hirn_level
            FROM features f
            LEFT JOIN hirns r on f.id = r.hirn_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHIRNDTO feature = new FeatureHIRNDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHIDCDTO>> GetHIDCFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHIDCDTO> features = new Dictionary<string, FeatureHIDCDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hidc_level, 0) AS hidc_level
            FROM features f
            LEFT JOIN hidcs r on f.id = r.hidc_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHIDCDTO feature = new FeatureHIDCDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHICBDTO>> GetHICBFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHICBDTO> features = new Dictionary<string, FeatureHICBDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hicb_level, 0) AS hicb_level
            FROM features f
            LEFT JOIN hicbs r on f.id = r.hicb_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHICBDTO feature = new FeatureHICBDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureHISNDTO>> GetHISNFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureHISNDTO> features = new Dictionary<string, FeatureHISNDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                COALESCE(r.hicb_level, 0) AS hicb_level
            FROM features f
            LEFT JOIN hicbs r on f.id = r.hicb_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureHISNDTO feature = new FeatureHISNDTO
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2),
                            CurrentLevel = reader.GetInt32(3)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, Features>> GetAnimeFeaturesByTypeAsync(string type)
    {
        Dictionary<string, Features> features = new Dictionary<string, Features>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT DISTINCT type FROM features WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureType = reader.GetString(1);

                        Features feature = new Features
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2)
                        };
                        // Vì selectSQL KHÔNG có cột thứ 2, đặt value mặc định
                        features[featureType] = feature;
                    }
                }
            }
        }

        return features;
    }
}