﻿[module:
    System.Diagnostics.CodeAnalysis.SuppressMessage(
        "StyleCop.CSharp.DocumentationRules", "*",
        Justification = "Reviewed. Suppression of all documentation rules is OK here.")]

namespace Vendord.SmartDevice.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    internal static class FormHelper
    {
        internal static List<T> GetControlsByType<T>(Control controlToSearch, bool searchDescendents) where T : class
        {
            List<T> result;
            result = new List<T>();
            foreach (Control c in controlToSearch.Controls)
            {
                if (c.GetType() == typeof(T))
                {
                    result.Add(c as T);
                }

                if (searchDescendents)
                {
                    result.AddRange(GetControlsByType<T>(c, true));
                }
            }

            return result;
        }

        internal static List<T> GetControlsByName<T>(
            Control controlToSearch, string nameOfControlsToFind, bool searchDescendants)
            where T : class
        {
            List<T> result;
            result = new List<T>();
            foreach (Control c in controlToSearch.Controls)
            {
                if (c.Name == nameOfControlsToFind && c.GetType() == typeof(T))
                {
                    result.Add(c as T);
                }

                if (searchDescendants)
                {
                    result.AddRange(GetControlsByName<T>(c, nameOfControlsToFind, true));
                }
            }

            return result;
        }

        internal static void WhiteListControlKeys(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                // it's a control; ergo allow it
                e.Handled = false;
            }
        }

        internal static void WhiteListDigitKeys(KeyPressEventArgs e)
        {
            // whitelist digits
            if (char.IsDigit(e.KeyChar))
            {
                // it's a digit
                try
                {
                    ////Convert.ToInt32((sender as TextBox).Text + e.KeyChar);

                    // the method didn't throw an overflow exception; so it's within 32 bits; ergo allow it                    
                    e.Handled = false;
                }
                catch (OverflowException)
                {
                    // catch and continue
                }
            }
        }
    }
}
