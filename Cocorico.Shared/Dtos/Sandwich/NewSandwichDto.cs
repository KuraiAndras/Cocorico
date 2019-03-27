using Cocorico.Shared.Contract;
using Cocorico.Shared.Helpers;
using System;

namespace Cocorico.Shared.Dtos.Sandwich
{
    public class NewSandwichDto : IHashAssertable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GetAssertHash() =>
            CustomHashCode
                .Of(Id)
                .And(Name);
    }
}
