using Cocorico.Shared.Contract;
using Cocorico.Shared.Helpers;

namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichResultDto : IHashAssertable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GetAssertHash() =>
            CustomHashCode
                .Of(Id)
                .And(Name);
    }
}
