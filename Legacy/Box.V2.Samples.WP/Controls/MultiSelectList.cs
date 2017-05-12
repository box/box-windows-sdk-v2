using Microsoft.Phone.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Box.V2.Samples.WP.Controls
{
    public class MultiselectListEx : MultiselectList
    {
        private bool _doNotExecuteCommand;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MultiselectListEx), null);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(MultiselectListEx), null);

        //public DataTemplateSelector DataTemplateSelector
        //{
        //    get { return (DataTemplateSelector)GetValue(DataTemplateSelectorProperty); }
        //    set { SetValue(DataTemplateSelectorProperty, value); }
        //}

        //public static readonly DependencyProperty DataTemplateSelectorProperty =
        //    DependencyProperty.Register("DataTemplateSelector", typeof(DataTemplateSelector), typeof(MultiselectListEx), null);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(MultiselectListEx), null);

        public IList MySelectedItems
        {
            get { return (IList)GetValue(MySelectedItemsProperty); }
            set { SetValue(MySelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty MySelectedItemsProperty =
            DependencyProperty.Register("MySelectedItems", typeof(IList), typeof(MultiselectListEx), null);

        public static readonly DependencyProperty AlwaysEnabledProperty =
            DependencyProperty.Register("AlwaysEnabled", typeof(bool), typeof(MultiselectListEx), new PropertyMetadata(default(bool)));

        public bool AlwaysEnabled
        {
            get { return (bool)GetValue(AlwaysEnabledProperty); }
            set { SetValue(AlwaysEnabledProperty, value); }
        }

        //protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        //{
        //    base.PrepareContainerForItemOverride(element, item);

        //    if (DataTemplateSelector != null && element is ContentControl)
        //    {
        //        var control = (ContentControl)element;
        //        control.ContentTemplate = DataTemplateSelector.SelectTemplate(item, element);
        //    }
        //}

        public MultiselectListEx()
        {
            this.Tap += MultiselectListEx_Tap;

            this.SelectionChanged += new SelectionChangedEventHandler(MultiselectListEx_SelectionChanged);

            if (AlwaysEnabled)
            {
                this.IsSelectionEnabledChanged += MultiselectListEx_IsSelectionEnabledChanged;
            }
        }

        void MultiselectListEx_IsSelectionEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsSelectionEnabled = true;
        }

        void MultiselectListEx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MySelectedItems != null)
            {
                if (e.AddedItems != null)
                {
                    foreach (var item in e.AddedItems)
                    {
                        MySelectedItems.Add(item);
                    }
                }
                if (e.RemovedItems != null)
                {
                    foreach (var item in e.RemovedItems)
                    {
                        MySelectedItems.Remove(item);
                    }
                }
            }

            _doNotExecuteCommand = true;
        }

        void MultiselectListEx_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var tappedElement = e.OriginalSource as FrameworkElement;
            if (tappedElement == null)
            {
                return;
            }

            var tappedItem = tappedElement.GetParentOfType<MultiselectItem>();

            //Fix to prevent the element from being selected then unselected when tapping the hint panel (left section of the MultiselectItem)
            if (IsSelectionEnabled && tappedItem != null && !tappedElement.Name.Contains("HintPanel"))
            {
                tappedItem.IsSelected = !tappedItem.IsSelected;
            }

            if (this.IsSelectionEnabled || _doNotExecuteCommand)
            {
                _doNotExecuteCommand = false;
                return;
            }
            else
            {
                _doNotExecuteCommand = false;
            }

            if (tappedItem != null)
            {
                SelectedItem = tappedItem.DataContext;
                if (Command != null && CommandParameter != null)
                    Command.Execute(CommandParameter);
                SelectedItem = null;
            }
        }
    }
}
