namespace MoneyKeeper.Web.Models
{
    public class SimpleResponseModel
    {
        public bool Success { get; set; }

        public object Data { get; set; }

        public SimpleResponseModel()
        {
        }

        public SimpleResponseModel(bool success)
        {
            this.Success = success;
        }

        public SimpleResponseModel(bool success, object data) : this(success)
        {
            this.Data = data;
        }
    }
}