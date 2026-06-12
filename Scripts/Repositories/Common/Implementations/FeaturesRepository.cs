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
    public async Task<Dictionary<string, FeatureScienceFictionDTO>> GetScienceFictionFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureScienceFictionDTO> features = new Dictionary<string, FeatureScienceFictionDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.science_fiction_level, 0) AS current_level
            FROM features f
            LEFT JOIN science_fictions r on f.id = r.id
            LEFT JOIN user_science_fictions ur on r.id = ur.science_fiction_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureScienceFictionDTO feature = new FeatureScienceFictionDTO
                        {
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.research_level, 0) AS current_level
            FROM features f
            LEFT JOIN researchs r on f.id = r.id
            LEFT JOIN user_researchs ur on r.id = ur.research_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.archive_level, 0) AS current_level
            FROM features f
            LEFT JOIN archives r on f.id = r.id
            LEFT JOIN user_archives ur on r.id = ur.archive_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.universe_level, 0) AS current_level
            FROM features f
            LEFT JOIN universes r on f.id = r.id
            LEFT JOIN user_universes ur on r.id = ur.universe_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hiin_level, 0) AS current_level
            FROM features f
            LEFT JOIN hiins r on f.id = r.id
            LEFT JOIN user_hiins ur on r.id = ur.hiin_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.sswn_level, 0) AS current_level
            FROM features f
            LEFT JOIN sswns r on f.id = r.id
            LEFT JOIN user_sswns ur on r.id = ur.sswn_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hitn_level, 0) AS current_level
            FROM features f
            LEFT JOIN hitns r on f.id = r.id
            LEFT JOIN user_hitns ur on r.id = ur.hitn_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hihn_level, 0) AS current_level
            FROM features f
            LEFT JOIN hihns r on f.id = r.id
            LEFT JOIN user_hihns ur on r.id = ur.hihn_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hien_level, 0) AS current_level
            FROM features f
            LEFT JOIN hiens r on f.id = r.id
            LEFT JOIN user_hiens ur on r.id = ur.hien_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hica_level, 0) AS current_level
            FROM features f
            LEFT JOIN hicas r on f.id = r.hica_id
            LEFT JOIN user_hicas ur on r.id = ur.hica_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hirn_level, 0) AS current_level
            FROM features f
            LEFT JOIN hirns r on f.id = r.id
            LEFT JOIN user_hirns ur on r.id = ur.hirn_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hidc_level, 0) AS current_level
            FROM features f
            LEFT JOIN hidcs r on f.id = r.id
            LEFT JOIN user_hidcs ur on r.id = ur.hidc_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hicb_level, 0) AS current_level
            FROM features f
            LEFT JOIN hicbs r on f.id = r.id
            LEFT JOIN user_hicbs ur on r.id = ur.hicb_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
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
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.hisn_level, 0) AS current_level
            FROM features f
            LEFT JOIN hisns r on f.id = r.id
            LEFT JOIN user_hisns ur on r.id = ur.hisn_id
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
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureModuleDTO>> GetModuleFeaturesByTypeAsync(string objectId, string type, string featureCodeName, string userTable, string objectColumn)
    {
        Dictionary<string, FeatureModuleDTO> features = new();

        using MySqlConnection connection = new(DatabaseConfig.ConnectionString);
        await connection.OpenAsync();

        string selectSQL = $@"
        SELECT
            f.id,
            f.feature_name,
            f.required_level,
            f.code_name,
            m.base_multiplier,
            COALESCE(u.current_level, 0) AS current_level
        FROM features f
        LEFT JOIN modules m
            ON f.id = m.id
        LEFT JOIN {userTable} u
            ON m.id = u.module_id
            AND u.{objectColumn} = @object_id
        WHERE f.type = @type
            AND f.code_name = @code_name";

        using MySqlCommand command = new(selectSQL, connection);

        command.Parameters.AddWithValue("@object_id", objectId);
        command.Parameters.AddWithValue("@type", type);
        command.Parameters.AddWithValue("@code_name", featureCodeName);

        using MySqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            FeatureModuleDTO feature = new()
            {
                Id = reader.GetStringSafe("id"),
                FeatureName = reader.GetStringSafe("feature_name"),
                RequiredLevel = reader.GetIntSafe("required_level"),
                CodeName = reader.GetStringSafe("code_name"),
                BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                CurrentLevel = reader.GetIntSafe("current_level")
            };

            features[feature.FeatureName] = feature;
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureUpgradeDTO>> GetUpgradeFeaturesByTypeAsync(string objectId, string type, string featureCodeName, string userTable, string objectColumn)
    {
        Dictionary<string, FeatureUpgradeDTO> features = new();

        using MySqlConnection connection = new(DatabaseConfig.ConnectionString);
        await connection.OpenAsync();

        string selectSQL = $@"
        SELECT
            f.id,
            f.feature_name,
            f.required_level,
            f.code_name,
            m.base_multiplier,
            COALESCE(u.current_level, 0) AS current_level
        FROM features f
        LEFT JOIN upgrades m
            ON f.id = m.id
        LEFT JOIN {userTable} u
            ON m.id = u.upgrade_id
            AND u.{objectColumn} = @object_id
        WHERE f.type = @type
            AND f.code_name = @code_name";

        using MySqlCommand command = new(selectSQL, connection);

        command.Parameters.AddWithValue("@object_id", objectId);
        command.Parameters.AddWithValue("@type", type);
        command.Parameters.AddWithValue("@code_name", featureCodeName);

        using MySqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            FeatureUpgradeDTO feature = new()
            {
                Id = reader.GetStringSafe("id"),
                FeatureName = reader.GetStringSafe("feature_name"),
                RequiredLevel = reader.GetIntSafe("required_level"),
                CodeName = reader.GetStringSafe("code_name"),
                BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                CurrentLevel = reader.GetIntSafe("current_level")
            };

            features[feature.FeatureName] = feature;
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureAnimeDTO>> GetAnimeFeaturesByTypeAsync(string type)
    {
        Dictionary<string, FeatureAnimeDTO> features = new Dictionary<string, FeatureAnimeDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = @"SELECT f.id, 
                f.feature_name, 
                f.required_level,
                f.code_name,
                r.base_multiplier,
                COALESCE(ur.anime_level, 0) AS current_level
            FROM features f
            LEFT JOIN animes r on f.id = r.id
            LEFT JOIN user_animes ur on r.id = ur.anime_id
            WHERE type = @type";

            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        FeatureAnimeDTO feature = new FeatureAnimeDTO
                        {
                            Id = reader.GetStringSafe("id"),
                            FeatureName = reader.GetStringSafe("feature_name"),
                            RequiredLevel = reader.GetIntSafe("required_level"),
                            CodeName = reader.GetStringSafe("code_name"),
                            BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                            CurrentLevel = reader.GetIntSafe("current_level")
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureMasterDTO>> GetMasterFeaturesByTypeAsync(string objectId, string type, string featureCodeName, string userTable, string objectColumn)
    {
        Dictionary<string, FeatureMasterDTO> features = new();

        using MySqlConnection connection = new(DatabaseConfig.ConnectionString);
        await connection.OpenAsync();

        string selectSQL = $@"
        SELECT
            f.id,
            f.feature_name,
            f.required_level,
            f.code_name,
            m.base_multiplier,
            COALESCE(u.current_level, 0) AS current_level
        FROM features f
        LEFT JOIN masters m
            ON f.id = m.id
        LEFT JOIN {userTable} u
            ON m.id = u.master_id
            AND u.{objectColumn} = @object_id
        WHERE f.type = @type
            AND f.code_name = @code_name";

        using MySqlCommand command = new(selectSQL, connection);

        command.Parameters.AddWithValue("@object_id", objectId);
        command.Parameters.AddWithValue("@type", type);
        command.Parameters.AddWithValue("@code_name", featureCodeName);

        using MySqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            FeatureMasterDTO feature = new()
            {
                Id = reader.GetStringSafe("id"),
                FeatureName = reader.GetStringSafe("feature_name"),
                RequiredLevel = reader.GetIntSafe("required_level"),
                CodeName = reader.GetStringSafe("code_name"),
                BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                CurrentLevel = reader.GetIntSafe("current_level")
            };

            features[feature.FeatureName] = feature;
        }

        return features;
    }
    public async Task<Dictionary<string, FeatureRankDTO>> GetRankFeaturesByTypeAsync(string objectId, string type, string featureCodeName, string userTable, string objectColumn)
    {
        Dictionary<string, FeatureRankDTO> features = new();

        using MySqlConnection connection = new(DatabaseConfig.ConnectionString);
        await connection.OpenAsync();

        string selectSQL = $@"
        SELECT
            f.id,
            f.feature_name,
            f.required_level,
            f.code_name,
            m.base_multiplier,
            COALESCE(u.current_level, 0) AS current_level
        FROM features f
        LEFT JOIN ranks m
            ON f.id = m.id
        LEFT JOIN {userTable} u
            ON m.id = u.rank_id
            AND u.{objectColumn} = @object_id
        WHERE f.type = @type
            AND f.code_name = @code_name";

        using MySqlCommand command = new(selectSQL, connection);

        command.Parameters.AddWithValue("@object_id", objectId);
        command.Parameters.AddWithValue("@type", type);
        command.Parameters.AddWithValue("@code_name", featureCodeName);

        using MySqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            FeatureRankDTO feature = new()
            {
                Id = reader.GetStringSafe("id"),
                FeatureName = reader.GetStringSafe("feature_name"),
                RequiredLevel = reader.GetIntSafe("required_level"),
                CodeName = reader.GetStringSafe("code_name"),
                BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                CurrentLevel = reader.GetIntSafe("current_level")
            };

            features[feature.FeatureName] = feature;
        }

        return features;
    }
}