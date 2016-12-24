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
                new Category { Name = "������ �����", Type = FinOperationType.Expense },
                new Category { Name = "�������", Type = FinOperationType.Expense },
                new Category { Name = "����������� ������", Type = FinOperationType.Expense },
                new Category { Name = "���������", Type = FinOperationType.Expense },
                new Category { Name = "�����������", Type = FinOperationType.Expense },
                new Category { Name = "������", Type = FinOperationType.Expense },
                new Category { Name = "��������", Type = FinOperationType.Expense },
                new Category { Name = "����������", Type = FinOperationType.Expense },
                new Category { Name = "�������", Type = FinOperationType.Expense },

                new Category { Name = "���������", Type = FinOperationType.Income },
                new Category { Name = "��������", Type = FinOperationType.Income },
                new Category { Name = "������-������", Type = FinOperationType.Income }
                );

            context.SaveChanges();
        }
    }
}