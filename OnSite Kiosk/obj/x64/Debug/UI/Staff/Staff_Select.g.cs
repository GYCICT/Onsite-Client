﻿#pragma checksum "C:\Users\pprice\source\repos\OnSite Kiosk\OnSite Kiosk\UI\Staff\Staff_Select.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E065B9C9AD37D021A2122880B561AAA7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnSite_Kiosk.UI.Staff
{
    partial class Staff_Select : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // UI\Staff\Staff_Select.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // UI\Staff\Staff_Select.xaml line 17
                {
                    this.action_panel = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // UI\Staff\Staff_Select.xaml line 22
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Signin_Click;
                }
                break;
            case 4: // UI\Staff\Staff_Select.xaml line 23
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Signout_Click;
                }
                break;
            case 5: // UI\Staff\Staff_Select.xaml line 14
                {
                    this.txt_search = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.txt_search).TextChanged += this.txt_search_TextChanged;
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.txt_search).KeyUp += this.txt_search_KeyUp;
                }
                break;
            case 6: // UI\Staff\Staff_Select.xaml line 15
                {
                    this.lst_results = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lst_results).Tapped += this.lst_results_Tapped;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

