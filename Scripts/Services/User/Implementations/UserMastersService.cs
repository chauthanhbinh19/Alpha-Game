using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class UserMastersService : IUserMastersService
{
    private readonly IUserMastersRepository _userMastersRepository;
    private static readonly Dictionary<Type, (string Table, string Column, string CodeName)> ModuleMappings = new()
    {
        { typeof(Books), (DataBaseConstants.Table.USER_BOOKS_MASTER, DataBaseConstants.Column.USER_BOOK_ID, "books") },
        { typeof(CardAdmirals), (DataBaseConstants.Table.USER_CARD_ADMIRALS_MASTER, DataBaseConstants.Column.USER_CARD_ADMIRAL_ID, "card_admirals") },
        { typeof(CardCaptains), (DataBaseConstants.Table.USER_CARD_CAPTAINS_MASTER, DataBaseConstants.Column.USER_CARD_CAPTAIN_ID, "card_captains") },
        { typeof(CardColonels), (DataBaseConstants.Table.USER_CARD_COLONELS_MASTER, DataBaseConstants.Column.USER_CARD_COLONEL_ID, "card_colonels") },
        { typeof(CardGenerals), (DataBaseConstants.Table.USER_CARD_GENERALS_MASTER, DataBaseConstants.Column.USER_CARD_GENERAL_ID, "card_generals") },
        { typeof(CardHeroes), (DataBaseConstants.Table.USER_CARD_HEROES_MASTER, DataBaseConstants.Column.USER_CARD_HERO_ID, "card_heroes") },
        { typeof(CardMilitaries), (DataBaseConstants.Table.USER_CARD_MILITARIES_MASTER, DataBaseConstants.Column.USER_CARD_MILITARY_ID, "card_militaries") },
        { typeof(CardMonsters), (DataBaseConstants.Table.USER_CARD_MONSTERS_MASTER, DataBaseConstants.Column.USER_CARD_MONSTER_ID, "card_monsters") },
        { typeof(CardSoldiers), (DataBaseConstants.Table.USER_CARD_SOLDIERS_MASTER, DataBaseConstants.Column.USER_CARD_SOLDIER_ID, "card_soldiers") },
        { typeof(CardSpells), (DataBaseConstants.Table.USER_CARD_SPELLS_MASTER, DataBaseConstants.Column.USER_CARD_SPELL_ID, "card_spells") },
        { typeof(Pets), (DataBaseConstants.Table.USER_PETS_MASTER, DataBaseConstants.Column.USER_PET_ID, "pets") },
    };

    public UserMastersService(IUserMastersRepository userMastersRepository)
    {
        _userMastersRepository = userMastersRepository;
    }

    public static IUserMastersService Create() => ServiceContainer.GetService<IUserMastersService>();

    public async Task<UserMasters> GetUserMastersAsync(string userId, string masterId, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        return await _userMastersRepository.GetUserMastersAsync(userId, masterId, stat.Id, mapping.Table, mapping.Column);
    }

    public async Task<UserMasters> GetSumUserMastersAsync(string userId, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        return await _userMastersRepository.GetSumUserMastersAsync(userId, stat.Id, mapping.Table, mapping.Column);
    }

    public async Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, IStats stat)
    {
        if (!ModuleMappings.TryGetValue(stat.GetType(), out var mapping))
        {
            throw new NotSupportedException(
                $"Unsupported stat type: {stat.GetType().Name}");
        }
        await _userMastersRepository.InsertOrUpdateUserMastersAsync(userId, Masters, stat.Id, mapping.Table, mapping.Column);
    }

}