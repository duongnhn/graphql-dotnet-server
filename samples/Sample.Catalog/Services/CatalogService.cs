using System.Reactive.Linq;
using System.Reactive.Subjects;
using GraphQL.Server.Sample.Catalog.Schema.Model;

namespace GraphQL.Server.Sample.Catalog.Services;

public class CatalogService : ICatalogService, IDisposable
{
    private readonly List<Item> _items = new();
    private readonly Subject<ItemEvent> _broadcaster = new();
    private readonly List<Manifest> _manifests = new();

    #region Item

    public Item? LastItem { get; private set; }

    public IEnumerable<Item> GetAllItems()
    {
        lock (_items)
            return _items.ToList();
    }

    public Item AddItem(ItemInput item)
    {
        var newItem = new Item
        {
            Id = Guid.NewGuid().ToString(),
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

    public Item? DeleteItem(string id)
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

    public Item? GetItemById(string id)
    {
        Item? targetItem = null;
        lock (_items)
        {
            targetItem = _items.FirstOrDefault(x => x.Id == id);
        }
        return targetItem;
    }

    #endregion

    #region Manifest

    public IEnumerable<Manifest> GetAllManifests()
    {
        lock (_manifests)
            return _manifests.ToList();
    }

    public Manifest AddManifest(ManifestInput manifest)
    {
        var items = new List<Item>();
        lock (_items)
        {
            items = _items.Where(i => manifest.ItemIds.Contains(i.Id)).ToList();
        }
        var newManifest = new Manifest
        {
            Id = Guid.NewGuid().ToString(),
            Name = manifest.Name,
            Description = manifest.Description,
            ItemIds = manifest.ItemIds,
            Items = items,
            CreatedOn = DateTime.UtcNow,
        };
        lock (_manifests)
            _manifests.Add(newManifest);
        return newManifest;
    }

    public Manifest? GetManifestById(string id)
    {
        Manifest? targetManifest = null;
        lock (_manifests)
        {
            targetManifest = _manifests.FirstOrDefault(x => x.Id == id);
        }
        return targetManifest;
    }

    public Manifest AddManifestFromAllItems(ManifestInput manifest)
    {
        var newManifest = new Manifest
        {
            Id = Guid.NewGuid().ToString(),
            Name = manifest.Name,
            Description = manifest.Description,
            ItemIds = _items.Select(i => i.Id).ToList(),
            Items = _items.ToList(),
            CreatedOn = DateTime.UtcNow,
        };
        lock (_manifests)
            _manifests.Add(newManifest);
        return newManifest;
    }

    #endregion

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
