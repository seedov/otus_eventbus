using System.Threading.Tasks;

public class TurnPipeline : Pipeline
{
    private VisualPipeline _visualPipeline;
    public TurnPipeline(VisualPipeline visualPipeline)
    {
        _visualPipeline = visualPipeline;
    }
    public override async Task Run()
    {
        await base.Run();
        await _visualPipeline.Run();
        await Run();
    }
}

public class VisualPipeline: Pipeline
{
    public override async Task Run()
    {
        await base.Run();
        Clear();
    }
}
