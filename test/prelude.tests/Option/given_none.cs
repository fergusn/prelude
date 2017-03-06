using Xunit;
using static Prelude;

namespace prelude.spec
{
    public class given_none
    {
        private readonly Option<int> none = None;

        [Fact]
        public void when_get_then_else_is_returned() =>
            Assert.Equal(5, none.GetOrElse(5));


    }
}
