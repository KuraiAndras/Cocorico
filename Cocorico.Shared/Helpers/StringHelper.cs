using System.Linq;

namespace Cocorico.Shared.Helpers
{
    public static class StringHelper
    {
        public static string Create(params string[] strings)
        {
            return strings.Aggregate((sum, item) => sum + item);
        }
    }
}
