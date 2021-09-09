namespace Persistence
{
  public static class ItemFilter
  {
    public const int GET_ALL = 0;
    public const int FILTER_BY_ITEM_NAME = 1;
  }
  public static class ItemStatus
  {
    public const int NOT_ACTIVE = 0;
    public const int ACTIVE = 1;
  }

  public class Item
  {
    public int? ItemId { set; get; }
    public string ItemName { set; get; }
    public decimal ItemPrice { set; get; }
    public int? Amount { set; get; }
    public int? Status { set; get; }
    public string Description { set; get; }

    public override bool Equals(object obj)
    {
      if (obj is Item)
      {
        return ((Item)obj).ItemId.Equals(ItemId);
      }
      return false;
    }

    public override int GetHashCode()
    {
      return ItemId.GetHashCode();
    }
  }
}
