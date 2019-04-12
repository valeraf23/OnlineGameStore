namespace OnlineGameStore.Common.Errors
{
    public class SaveError : Error
    {
        public SaveError()
        {
        }

        public SaveError(string errorMsg) => ErrorMsg = errorMsg;
        public string ErrorMsg { get; set; }
    }
}