using Xunit;
using static Prelude;

namespace prelude.spec
{
    public class given_some
    {
        private readonly Option<int> some = Some(1);

        [Fact]
        public void when_get_then_value_is_returned() =>
            Assert.Equal(1, some.GetOrElse(5));

        [Fact]
        public void when_filter_math_return_some() {
            var zs = from x in some 
                     where x > 0 
                     select x;
            Assert.Equal(1, zs.Get());
        }


        void test() 
        {
            var options = new[] { Some(2), Some(4), None };
            var ab = from a in options[0]
                     from b in options[1]
                     where b != 0
                     select a / b;

            var c = ab.GetOrElse(0); 

        }
        [Fact]
        public void when_filter_not_match_return_none() 
        {
            var zs = from x in some 
                     where x < 0 
                     select x;

            Assert.False(zs.Any());    
        }
    }
}
