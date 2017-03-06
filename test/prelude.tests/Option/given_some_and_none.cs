using Xunit;
using static Prelude;

namespace prelude.spec
{
    class given_some_and_none
    {
        private readonly Option<int> xs = Some(2);
        private readonly Option<int> ys = Some(6);

        [Fact]
        public void when_compose_then_values_add()
        {
            var zs = from x in xs
                     from y in ys
                     select x + y;

            Assert.Equal(8, zs.Get());
        }

    }
}
