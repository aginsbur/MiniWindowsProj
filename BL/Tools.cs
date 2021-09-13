using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BO
{
    public static class Tools : Object
    {
        /// <summary>
        /// extension method for
        ///ToString
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="t">"this" type</param>
        /// <returns></returns>
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }

        #region***not used in project,can be used for PO***
        //   public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
        //   where T : DependencyObject
        //    {
        //        if (depObj != null)
        //        {
        //            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        //            {
        //                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
        //                if (child != null && child is T)
        //                {
        //                    yield return (T)child;
        //                }

        //                foreach (T childOfChild in FindVisualChildren<T>(child))
        //                {
        //                    yield return childOfChild;
        //                }
        //            }
        //        }
        //    }

        //    public static childItem FindVisualChild<childItem>(DependencyObject obj)
        //        where childItem : DependencyObject
        //    {
        //        foreach (childItem child in FindVisualChildren<childItem>(obj))
        //        {
        //            return child;
        //        }

        //        return null;
        //    }
        //public static List<Control> AllChildren(DependencyObject parent)
        //{
        //    var _List = new List<Control>();
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var _Child = VisualTreeHelper.GetChild(parent, i);
        //        if (_Child is Control)
        //        {
        //            _List.Add(_Child as Control);
        //        }
        //        _List.AddRange(AllChildren(_Child));
        //    }
        //    return _List;
        //}


        //public static  T FindControl<T>(DependencyObject parentContainer, string controlName)
        //{
        //    var childControls = AllChildren(parentContainer);
        //    var control = childControls.OfType<Control>().Where(x => x.Name.Equals(controlName)).Cast<T>().First();
        //    return control;
        //}
        #endregion

    }
}

