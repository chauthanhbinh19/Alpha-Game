using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class UserRanksService : IUserRanksService
{
    private readonly IUserRanksRepository _userRanksRepository;
    private static readonly Dictionary<Type, (string Table, string Column, string CodeName)> ModuleMappings = new()
    {
        { typeof(Books), (DataBaseConstants.Table.USER_BOOKS_RANK, DataBaseConstants.Column.USER_BOOK_ID, "books") },
        { typeof(CardAdmirals), (DataBaseConstants.Table.USER_CARD_ADMIRALS_RANK, DataBaseConstants.Column.USER_CARD_ADMIRAL_ID, "card_admirals") },
        { typeof(CardCaptains), (DataBaseConstants.Table.USER_CARD_CAPTAINS_RANK, DataBaseConstants.Column.USER_CARD_CAPTAIN_ID, "card_captains") },
        { typeof(CardColonels), (DataBaseConstants.Table.USER_CARD_COLONELS_RANK, DataBaseConstants.Column.USER_CARD_COLONEL_ID, "card_colonels") },
        { typeof(CardGenerals), (DataBaseConstants.Table.USER_CARD_GENERALS_RANK, DataBaseConstants.Column.USER_CARD_GENERAL_ID, "card_generals") },
        { typeof(CardHeroes), (DataBaseConstants.Table.USER_CARD_HEROES_RANK, DataBaseConstants.Column.USER_CARD_HERO_ID, "card_heroes") },
        { typeof(CardMilitaries), (DataBaseConstants.Table.USER_CARD_MILITARIES_RANK, DataBaseConstants.Column.USER_CARD_MILITARY_ID, "card_militaries") },
        { typeof(CardMonsters), (DataBaseConstants.Table.USER_CARD_MONSTERS_RANK, DataBaseConstants.Column.USER_CARD_MONSTER_ID, "card_monsters") },
        { typeof(CardSoldiers), (DataBaseConstants.Table.USER_CARD_SOLDIERS_RANK, DataBaseConstants.Column.USER_CARD_SOLDIER_ID, "card_soldiers") },
        { typeof(CardSpells), (DataBaseConstants.Table.USER_CARD_SPELLS_RANK, DataBaseConstants.Column.USER_CARD_SPELL_ID, "card_spells") },
        { typeof(Pets), (DataBaseConstants.Table.USER_PETS_RANK, DataBaseConstants.Column.USER_PET_ID, "pets") },
    };

    public UserRanksService(IUserRanksRepository userRanksRepository)
    {
        _userRanksRepository = userRanksRepository;
    }

    public static IUserRanksService Create() => ServiceContainer.GetService<IUserRanksService>();

    public async Task<UserRanks> GetUserRanksAsync(string userId, string rankId, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        return await _userRanksRepository.GetUserRanksAsync(userId, rankId, stat.Id, mapping.Table, mapping.Column);
    }

    public async Task<UserRanks> GetSumUserRanksAsync(string userId, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        return await _userRanksRepository.GetSumUserRanksAsync(userId, stat.Id, mapping.Table, mapping.Column);
    }

    public async Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        await _userRanksRepository.InsertOrUpdateUserRanksAsync(userId, Ranks, stat.Id, mapping.Table, mapping.Column);
    }

}