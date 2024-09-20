using System.Reactive.Linq;
using System.Reactive.Subjects;
using GraphQL.Server.Sample.Catalog.Schema;

namespace GraphQL.Server.Sample.Catalog.Services;

public class CatalogService : ICatalogService, IDisposable
{
    private readonly List<Item> _items = new();
    private int _itemId;
    private readonly Subject<ItemEvent> _broadcaster = new();

    public Item? LastItem { get; private set; }

    public IEnumerable<Item> GetAllItems()
    {
        lock (_items)
            return _items.ToList();
    }

    public Item PostItem(ItemInput item)
    {
        var newItem = new Item
        {
            Id = Interlocked.Increment(ref _itemId),
            Name = item.Name,
            Description = item.Description,
            CreatedOn = DateTime.UtcNow,
        };
        LastItem = newItem;
        lock (_items)
            _items.Add(newItem);
        _broadcaster.OnNext(new ItemEvent { Type = ItemEventType.NewItem, Item = newItem });
        return newItem;
    }

    public Item? DeleteItem(int id)
    {
        Item? deletedItem = null;
        lock (_items)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == id)
                {
                    deletedItem = _items[i];
                    _items.RemoveAt(i);
                    break;
                }
            }
        }
        if (deletedItem != null)
            _broadcaster.OnNext(new ItemEvent { Type = ItemEventType.DeleteItem, Item = deletedItem });
        return deletedItem;
    }

    public IObservable<ItemEvent> SubscribeEvents() => _broadcaster;

    public IObservable<Item> SubscribeNewItem() => _broadcaster.Where(x => x.Type == ItemEventType.NewItem).Select(x => x.Item!);

    public int ClearItems()
    {
        int count;
        lock (_items)
        {
            count = _items.Count;
            _items.Clear();
        }
        _broadcaster.OnNext(new ItemEvent { Type = ItemEventType.ClearItems });
        return count;
    }

    public int Count
    {
        get
        {
            lock (_items)
                return _items.Count;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
