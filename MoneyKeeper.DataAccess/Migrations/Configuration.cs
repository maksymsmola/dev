using System.Data.Entity.Migrations;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MoneyKeeperContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyKeeperContext context)
        {
            context.Categories.AddOrUpdate(
                x => x.Name,
                new Category { Name = "Услуги связи", Type = FinOperationType.Expense },
                new Category { Name = "Питание", Type = FinOperationType.Expense },
                new Category { Name = "Комунальные услуги", Type = FinOperationType.Expense },
                new Category { Name = "Транспорт", Type = FinOperationType.Expense },
                new Category { Name = "Развлечения", Type = FinOperationType.Expense },
                new Category { Name = "Одежда", Type = FinOperationType.Expense },
                new Category { Name = "Медицина", Type = FinOperationType.Expense },
                new Category { Name = "Канцелярия", Type = FinOperationType.Expense },
                new Category { Name = "Техника", Type = FinOperationType.Expense },

                new Category { Name = "Стипендия", Type = FinOperationType.Income },
                new Category { Name = "Зарплата", Type = FinOperationType.Income },
                new Category { Name = "Бизнес-сделки", Type = FinOperationType.Income }
                );

            context.SaveChanges();
        }
    }
}