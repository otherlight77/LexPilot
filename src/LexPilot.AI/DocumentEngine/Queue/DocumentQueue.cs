using System.Collections.Concurrent;

namespace LexPilot.AI.DocumentEngine.Queue;

public sealed class DocumentQueue
{
    private readonly ConcurrentQueue<DocumentQueueItem> _queue = new();

    public void Enqueue(DocumentQueueItem item)
    {
        _queue.Enqueue(item);
    }

    public bool TryDequeue(out DocumentQueueItem? item)
    {
        return _queue.TryDequeue(out item);
    }

    public int Count => _queue.Count;
}
