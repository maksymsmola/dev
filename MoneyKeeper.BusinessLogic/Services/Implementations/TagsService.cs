using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.Tags;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class TagsService : ITagsService
    {
        private readonly IRepository repository;

        public TagsService(IRepository repository)
        {
            this.repository = repository;
        }

        public List<SimpleTagDto> GetAllForUser(long userId)
        {
            return this.repository.GetByCriteria<Tag>(tag => tag.UserId == userId)
                .Select(tag => tag.ToSimpleTagDto())
                .ToList();
        }
    }
}