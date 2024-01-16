namespace UI.Helpers;

public static class SelectionHelper
{
    public static async Task<SelectionResult<T>> GetSelections<T>(IAsyncEnumerable<T> dataSource, float train, float test) where T: class
    {
        var selectionResult = new SelectionResult<T>();

        if (Math.Abs(train + test - 1f) > 0.001f)
        {
            throw new ArgumentException("train + test must be equal to 1");
        }

        var trainSelection = new List<T>();
        var testSelection = new List<T>();
        await foreach (var item in dataSource)
        {
            if (Random.Shared.NextSingle() < train)
            {
                trainSelection.Add(item);
            }
            else
            {
                testSelection.Add(item);
            }
        }

        selectionResult.TrainSelection = trainSelection;
        selectionResult.TestSelection = testSelection;
        return selectionResult;
    }
}

public class SelectionResult<T> where T: class
{
    /// <summary>
    /// Train selection.
    /// </summary>
    public IReadOnlyList<T> TrainSelection { get; set; }
    
    /// <summary>
    /// Test selection.
    /// </summary>
    public IReadOnlyList<T> TestSelection { get; set; }
}