using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.Filters;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
using MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation;
using MoneyKeeper.BusinessLogic.Dto.Tags;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs;
using MoneyKeeper.Core;
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

        public List<FinOperationListItemDto> GetByFilter(FinOperationFilterDto filter)
        {
            Specification<FinancialOperation> specification = this.CreateSpecFilter(filter);

            return
                this.repository.GetByCriteriaIncluding(
                        specification.Predicate,
                        finOp => finOp.Category,
                        finOp => finOp.Tags)
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

        public FinOperationSyncDto AddPersistant(AddEditFinOperationDto dto)
        {
            FinancialOperation finOperation = dto.ToFinancialOperation();
            finOperation.State = EntityState.Synchronized;

            this.repository.Add(finOperation);

            this.repository.SaveChanges();

            return finOperation.ToFinOperationSyncDto();
        }

        public FinOperationSyncDto UpdatePersistant(AddEditFinOperationDto dto)
        {
            var finOperation = this.repository.FindById<FinancialOperation>(dto.Id);

            finOperation.Value = dto.Value;
            finOperation.Date = dto.Date;
            finOperation.Description = dto.Description;

            this.repository.SaveChanges();

            return finOperation.ToFinOperationSyncDto();
        }

        public void DeletePersistant(long finOperationId)
        {
            this.repository.DeleteById<FinancialOperation>(finOperationId);
            this.repository.SaveChanges();
        }

        private Specification<FinancialOperation> CreateSpecFilter(FinOperationFilterDto filter)
        {
            return
                new FinOpUserSpec(filter.UserId) &
                new FinOpTypeSpec(filter.Type) &
                new FinOpValueSpec(filter.Value) &
                new FinOpDateSpec(filter.Date) &
                new FinOpCategorySpec(filter.CategoriesIds) &
                new FinOpTagsSpec(filter.TagsIds) &
                new FinOpDescriptionSpec(filter.Description);
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