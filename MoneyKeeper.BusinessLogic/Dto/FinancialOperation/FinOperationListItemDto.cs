using System;
using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Tags;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Dto.FinancialOperation
{
    public class FinOperationListItemDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public FinOperationType Type { get; set; }

        public double Value { get; set; }

        public List<SimpleTagDto> Tags { get; set; }

        public FinOperationListItemDto()
        {
            this.Tags = new List<SimpleTagDto>();
        }
    }
}