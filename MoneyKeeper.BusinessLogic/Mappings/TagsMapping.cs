using MoneyKeeper.BusinessLogic.Dto.Tags;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Mappings
{
    internal static class TagsMapping
    {
        internal static SimpleTagDto ToSimpleTagDto(this Tag tag)
        {
            return new SimpleTagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        internal static Tag ToTag(this SimpleTagDto dto, long userId)
        {
            return new Tag
            {
                Name = dto.Name,
                UserId = userId
            };
        }
    }
}