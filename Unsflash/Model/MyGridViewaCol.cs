using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Unsflash.Model
{
    public class MyGridViewaCol: GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var myImage = item as GetaCollectionRootObject;
            var itemHeight = 310;
            var cellSize = 10;
            element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, (int)(myImage.Scale * myImage.width * (itemHeight / cellSize) / myImage.height));
            element.SetValue(VariableSizedWrapGrid.RowSpanProperty, (int)(myImage.Scale * itemHeight / cellSize));
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
