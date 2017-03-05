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

            List<Tag> tags = this.ExtractNewTags(model);
            tags.AddRange(this.ExtractExistingTags(model.Tags));

            finOperation.Tags = tags;

            this.repository.AddRange(finOperation.Clone(model.Amount));
            this.repository.SaveChanges();
        }

        public AddEditFinOperationDto GetForCrud(long id)
        {
            return
                this.repository.GetByCriteriaIncluding<FinancialOperation>(
                        finOp => finOp.Id == id,
                        finOp => finOp.Tags)
                    .First()
                    .ToAddEditFinOperationDto();
        }

        public void Edit(AddEditFinOperationDto model)
        {
            var targetFinOperation = this.repository.FindById<FinancialOperation>(model.Id);

            targetFinOperation.Value = model.Value;
            targetFinOperation.Date = model.Date;
            targetFinOperation.Description = model.Description;
            targetFinOperation.CategoryId = model.CategoryId;

            targetFinOperation.Tags.Clear();

            List<Tag> tags = this.ExtractNewTags(model);
            tags.AddRange(this.ExtractExistingTags(model.Tags));

            foreach (Tag tag in tags)
            {
                targetFinOperation.Tags.Add(tag);
            }

            this.repository.SaveChanges();
        }

        public void Delete(long id)
        {
            var finOperation = this.repository.FindById<FinancialOperation>(id);

            finOperation.Tags.Clear();
            finOperation.CategoryId = null;

            this.repository.Delete(finOperation);
            this.repository.SaveChanges();
        }

        private List<Tag> ExtractNewTags(AddEditFinOperationDto model)
        {
            return
                model.Tags.
                    Where(tag => !tag.Id.HasValue)
                    .Select(tagDto => tagDto.ToTag(model.UserId))
                    .ToList();
        }

        private List<Tag> ExtractExistingTags(List<SimpleTagDto> tags)
        {
            long[] existingTagsIds =
                tags.Where(tag => tag.Id.HasValue).Select(tag => tag.Id.Value).ToArray();

            return this.repository.GetByCriteria<Tag>(tag => existingTagsIds.Contains(tag.Id)).ToList();
        }
    }
}