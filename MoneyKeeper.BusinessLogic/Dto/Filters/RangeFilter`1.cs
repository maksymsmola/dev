namespace MoneyKeeper.BusinessLogic.Dto.Filters
{
    public class RangeFilter<T>
    {
        public T From { get; set; }

        public T To { get; set; }

        public bool ExactValue { get; set; }
    }
}