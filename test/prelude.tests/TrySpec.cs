using System;
using Xunit;
using static Prelude;

public class TrySpec 
{
    public class given_success_and_failure 
    {
        private readonly Try<int> success = Try(() => 5);
        private readonly Try<int> failure = Try<int>(() => throw new Exception());

        [Fact]
        public void when_compose_then_result_should_be_failure() 
        {
            var xy = from x in success
                     from y in failure
                     select x + y;
            Assert.True(xy.IsFailure);
        }
    }

    public class given_multi_successes 
    {
        private readonly Try<int>[] tries = new [] 
        {
            Try(() => 2),
            Try(() => 3),
            Try(() => 4)
        };

        [Fact]
        public void when_compose_result_should_succeed() 
        {
            var xyz = from x in tries[0]
                      from y in tries[1]
                      from z in tries[2]
                      select x + y + z;

            Assert.Equal(9, xyz.Get());
        }   
    }
}