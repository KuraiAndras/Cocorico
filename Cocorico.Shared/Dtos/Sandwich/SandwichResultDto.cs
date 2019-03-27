using Cocorico.Shared.Contract;
using System;

namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichResultDto : IHashAssertable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GetAssertHash()
        {
            throw new NotImplementedException();
        }
    }
}
