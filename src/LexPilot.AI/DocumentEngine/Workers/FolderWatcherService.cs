using LexPilot.AI.DocumentEngine.Options;
using LexPilot.AI.DocumentEngine.Queue;
using Microsoft.Extensions.Options;

namespace LexPilot.AI.DocumentEngine.Workers;

public sealed class FolderWatcherService
{
    private readonly DocumentQueue _queue;
    private readonly DocumentOptions _options;

    public FolderWatcherService(
        DocumentQueue queue,
        IOptions<DocumentOptions> options)
    {
        _queue = queue;
        _options = options.Value;
    }

    public int ScanImportFolder()
    {
        Directory.CreateDirectory(_options.ImportFolder);

        var files = Directory.GetFiles(_options.ImportFolder);
        var count = 0;

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file);

            if (!_options.AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                continue;

            _queue.Enqueue(new DocumentQueueItem
            {
                FilePath = file,
                IndexInRag = _options.AutoIndexInRag
            });

            count++;
        }

        return count;
    }
}
