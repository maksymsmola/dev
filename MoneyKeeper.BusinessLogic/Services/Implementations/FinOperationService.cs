using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
using MoneyKeeper.BusinessLogic.Dto.Tags;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class FinOperationService : IFinOperationService
    {
        private readonly IRepository repository;

        public FinOperationService(IRepository repository)
        {
            this.repository = repository;
        }

        public List<FinOperationListItemDto> GetAllForUser(long userId)
        {
            return
                this.repository.GetByCriteria<FinancialOperation>(finOp => finOp.UserId == userId)
                    .Select(finOp => finOp.ToFinOperationListItemDto())
                    .ToList();
        }

        public void Add(AddEditFinOperationDto model)
        {
            FinancialOperation finOperation = model.ToFinancialOperation();

            long[] existingTagsIds = model.Tags.Where(tag => tag.Id.HasValue).Select(tag => tag.Id.Value).ToArray();
            IEnumerable<SimpleTagDto> newTagsNames = model.Tags.Where(tag => !tag.Id.HasValue);

            List<Tag> existingTags = this.repository.GetByCriteria<Tag>(tag => existingTagsIds.Contains(tag.Id)).ToList();

            foreach (Tag existingTag in existingTags)
            {
                finOperation.Tags.Add(existingTag);
            }

            foreach (SimpleTagDto newTag in newTagsNames)
            {
                finOperation.Tags.Add(newTag.ToTag(model.UserId));
            }

            this.repository.Add(finOperation);
            this.repository.SaveChanges();
        }
    }
}