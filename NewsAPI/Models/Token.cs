namespace NewsAPI.Models
{
    public class Token
    {
        #region Constructors
        public Token()
        {

        }

        #endregion

        #region Properties
        public string TokenString { get; set; }
        public DateTime ExpDate { get; set; }

        #endregion

    }
}
