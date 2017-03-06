using System.Threading.Tasks;
using Xunit;

public class given_task 
{
    private readonly Task<int> task = Task.FromResult(5);

    [Fact]
    public async Task when_select_return_task_of_selector() 
    {
        var y = await from x in task select x + 5;
        Assert.Equal(10, y);
    }
}
public class givin_multiple_tasks 
{
    private readonly Task<int>[] tasks = new[] 
    { 
        Task.FromResult(5), 
        Task.FromResult(6) 
    };

        [Fact]
    public async Task when_composing_then_add() 
    {
        var z = await from x in tasks[0]
                      from y in tasks[1]
                      select x + y;
        Assert.Equal(11, z);
    }
}