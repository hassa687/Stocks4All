using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yolo.CustomControls
{
  public class SortableBindingList<T> : BindingList<T>
  {
     // reference to the list provided at the time of instantiation
    List<T> originalList;
    ListSortDirection sortDirection;
    PropertyDescriptor sortProperty;

    // function that refereshes the contents
    // of the base classes collection of elements
    Action<SortableBindingList<T>, List<T>> 
                   populateBaseList = (a, b) => a.ResetItems(b);

    // a cache of functions that perform the sorting
    // for a given type, property, and sort direction
    static Dictionary<string, Func<List<T>, IEnumerable<T>>> 
       cachedOrderByExpressions = new Dictionary<string, Func<List<T>, 
                                                 IEnumerable<T>>>();

    public SortableBindingList() {
        originalList = new List<T>();
    }

    public SortableBindingList(IEnumerable<T> enumerable) {
        originalList = enumerable.ToList();
        populateBaseList(this, originalList);
    }

    public SortableBindingList(List<T> list)
    {
        originalList = list;
        populateBaseList(this, originalList);
    }

    protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
    {
      List<T> itemsList = (List<T>)this.Items;
      if (property.PropertyType.GetInterface("IComparable") != null)
      {
        itemsList.Sort(new Comparison<T>(delegate(T x, T y)
        {
          // Compare x to y if x is not null. If x is, but y isn't, we compare y
          // to x and reverse the result. If both are null, they're equal.
          if (property.GetValue(x) != null)
            return ((IComparable)property.GetValue(x)).CompareTo(property.GetValue(y)) * (direction == ListSortDirection.Descending ? -1 : 1);
          else if (property.GetValue(y) != null)
            return ((IComparable)property.GetValue(y)).CompareTo(property.GetValue(x)) * (direction == ListSortDirection.Descending ? 1 : -1);
          else
            return 0;
        }));
      }

     // isSorted = true;
      sortProperty = property;
      sortDirection = direction;
    }


    private void CreateOrderByMethod(PropertyDescriptor prop, 
                 string orderByMethodName, string cacheKey) {

        /*
         Create a generic method implementation for IEnumerable<T>.
         Cache it.
        */

        var sourceParameter = Expression.Parameter(typeof(List<T>), "source");
        var lambdaParameter = Expression.Parameter(typeof(T), "lambdaParameter");
        var accesedMember = typeof(T).GetProperty(prop.Name);
        var propertySelectorLambda =
            Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter, 
                              accesedMember), lambdaParameter);
        var orderByMethod = typeof(Enumerable).GetMethods()
                                      .Where(a => a.Name == orderByMethodName &&
                                                   a.GetParameters().Length == 2)
                                      .Single()
                                      .MakeGenericMethod(typeof(T), prop.PropertyType);

        var orderByExpression = Expression.Lambda<Func<List<T>, IEnumerable<T>>>(
                                    Expression.Call(orderByMethod,
                                            new Expression[] { sourceParameter, 
                                                               propertySelectorLambda }),
                                            sourceParameter);

        cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
    }

    protected override void RemoveSortCore() {
        ResetItems(originalList);
    }

    private void ResetItems(List<T> items) {

        base.ClearItems();

        for (int i = 0; i < items.Count; i++) {
            base.InsertItem(i, items[i]);
        }
    }

    protected override bool SupportsSortingCore {
        get {
            // indeed we do
            return true;
        }
    }

    protected override ListSortDirection SortDirectionCore {
        get {
            return sortDirection;
        }
    }

    protected override PropertyDescriptor SortPropertyCore {
        get {
            return sortProperty;
        }
    }

    protected override void OnListChanged(ListChangedEventArgs e) {
        originalList = base.Items.ToList();
    }
  }
}
