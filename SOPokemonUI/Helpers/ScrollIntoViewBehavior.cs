using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace SOPokemonUI.Helpers
{
    /// <summary>
    /// From user Ankesh here: https://stackoverflow.com/questions/10135850/how-do-i-scrollintoview-after-changing-the-filter-on-a-listview-in-a-mvvm-wpf
    /// </summary>
    public class ScrollIntoViewBehavior : Behavior<ListView>
    {
        /// <summary>
        ///  When Beahvior is attached
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        /// <summary>
        /// When behavior is detached
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;

        }

        /// <summary>
        /// On Selection Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (sender is ListView)
            {
                ListView listview = (sender as ListView);
                if (listview.SelectedItem != null)
                {
                    listview.Dispatcher.BeginInvoke((Action) (() =>
                    {
                        listview.UpdateLayout();
                        if (listview.SelectedItem != null)
                        {
                            listview.ScrollIntoView(listview.SelectedItem);
                        }
                    }));
                }
            }
        }
    }
}
