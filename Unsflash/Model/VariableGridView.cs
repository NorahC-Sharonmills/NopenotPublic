using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Unsflash.Model
{
    class VariableGridView: ListView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            //var viewModel = item as IResizable;


            ////element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, 100);
            ////element.SetValue(VariableSizedWrapGrid.RowSpanProperty, viewModel.height);

            //base.PrepareContainerForItemOverride(element, item);

            try
            {
                dynamic gridItem = item;
                
                var typeItem = item as RootObject;
                
                if (typeItem != null)
                {
                    //var heightPecentage = (300.0 / bi.PixelHeight);
                    //var itemWidth = bi.PixelWidth * heightPecentage;
                    //var itemHeight = bi.PixelHeight * heightPecentage;
                    //var columnSpan = Convert.ToInt32(itemWidth / 10.0);


                    if (gridItem != null)
                    {
                        element.SetValue(VariableSizedWrapGrid.ItemWidthProperty, (300 * typeItem.width) / typeItem.height);
                        //element.SetValue(VariableSizedWrapGrid.ItemHeightProperty, typeItem.height / 10);
                        element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, 0);
                        element.SetValue(VariableSizedWrapGrid.RowSpanProperty, 0.5);
                        
                    }
                }
            }
            catch
            {
                element.SetValue(VariableSizedWrapGrid.ItemWidthProperty, 100);
                element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, 1);
                element.SetValue(VariableSizedWrapGrid.RowSpanProperty, 1);
            }
            finally
            {
                base.PrepareContainerForItemOverride(element, item);
            }
        }
    }
}
