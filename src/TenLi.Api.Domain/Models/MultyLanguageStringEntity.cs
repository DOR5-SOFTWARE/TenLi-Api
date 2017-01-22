using TenLi.Api.DataAccess.Mongo;

namespace TenLi.Api.Domain.Models
{
    public abstract class MultyLanguageStringEntity : Entity
    {
        public string HebValue { get; set; }
        public string EngValue { get; set; }
    }

    public static class MultyLanguageStringEntityExtensions
    {
        public static bool IsNullOrEmpty(this MultyLanguageStringEntity entity, bool checkHebrew = true, bool checkEnglish = true)
        {
            if (entity == null)
                return true;

            if (
                (string.IsNullOrEmpty(entity.HebValue) && checkHebrew) ||
                (string.IsNullOrEmpty(entity.EngValue) && checkEnglish)
           )
            {
                return true;
            }
            return false;
        }
    }
}
