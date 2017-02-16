using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Tags;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface ITagsService
    {
        List<SimpleTagDto> GetAllForUser(long userId);
    }
}