namespace MoneyKeeper.Core.Entities
{
    public class StatefulEntity : BaseEntity
    {
        public EntityState State { get; set; }

        public StatefulEntity()
        {
            this.State = EntityState.Added;
        }
    }
}