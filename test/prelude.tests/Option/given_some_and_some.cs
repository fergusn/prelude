using Xunit;
using static Prelude;

namespace prelude.spec
{
    public class given_some_and_some
    {
        private readonly Option<int> xs = Some(2);
        private readonly Option<int> ys = None;

        [Fact]
        public void when_compose_then_return_none() 
        {
            var zs = from x in xs 
                     from y in ys
                     select x + y;
            Assert.False(zs.Any());
        }

    }
}
