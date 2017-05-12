using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Box.V2.Controls
{
    public static class VisualTreeExtensions
    {
        public static T GetFirstChildOfType<T>(this UIElement element)
            where T : UIElement
        {
            int count = VisualTreeHelper.GetChildrenCount(element);
            for (var i = 0; i < count; i++)
            {
                var child = (FrameworkElement)VisualTreeHelper.GetChild(element, i);
                if (child is T)
                    return child as T;
                foreach (var frameworkElement in GetChildrenOfType<T>(child))
                    return frameworkElement;
            }

            return null;
        }

        public static IEnumerable<T> GetChildrenOfType<T>(this UIElement element)
            where T : UIElement
        {
            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; i++)
            {
                var child = (FrameworkElement)VisualTreeHelper.GetChild(element, i);
                if (child is T)
                    yield return child as T;
                foreach (var frameworkElement in GetChildrenOfType<T>(child))
                    yield return frameworkElement;
            }
        }

        public static T GetParentOfType<T>(this UIElement element)
            where T : UIElement
        {
            if (element == null) return null;

            var parent = VisualTreeHelper.GetParent(element);
            if (parent == null) return null;

            if (parent is T) return parent as T;
            else return GetParentOfType<T>(parent as UIElement);
        }

        public static T GetParent<T>(this UIElement element, Predicate<UIElement> predicate)
            where T : class
        {
            return element.GetParent(predicate) as T;
        }

        public static UIElement GetParent(this UIElement element, Predicate<UIElement> predicate)
        {
            var parent = VisualTreeHelper.GetParent(element) as UIElement;
            if (parent == null) return null;

            if (predicate(parent)) return parent;
            return parent.GetParent(predicate);
        }
    }

}
