using Extensions;

public class ItemsProvider : Singleton<ItemsProvider>
{
    public Item[,] Items { get; set; }
}
