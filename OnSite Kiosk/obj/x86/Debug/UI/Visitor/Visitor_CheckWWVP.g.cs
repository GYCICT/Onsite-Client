﻿#pragma checksum "C:\Users\pprice\source\repos\OnSite Kiosk\OnSite Kiosk\UI\Visitor\Visitor_CheckWWVP.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "63B06D0B434DB94912898E5ACD6F3844"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnSite_Kiosk.UI.Visitor
{
    partial class Visitor_CheckWWVP : 
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
            case 1: // UI\Visitor\Visitor_CheckWWVP.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // UI\Visitor\Visitor_CheckWWVP.xaml line 11
                {
                    this.FadeOutWait = (global::Windows.UI.Xaml.Media.Animation.Storyboard)(target);
                }
                break;
            case 3: // UI\Visitor\Visitor_CheckWWVP.xaml line 14
                {
                    this.FadeInSuccess = (global::Windows.UI.Xaml.Media.Animation.Storyboard)(target);
                }
                break;
            case 4: // UI\Visitor\Visitor_CheckWWVP.xaml line 17
                {
                    this.FadeInWarn = (global::Windows.UI.Xaml.Media.Animation.Storyboard)(target);
                }
                break;
            case 5: // UI\Visitor\Visitor_CheckWWVP.xaml line 22
                {
                    this.pnl_wait = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 6: // UI\Visitor\Visitor_CheckWWVP.xaml line 26
                {
                    this.pnl_success = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 7: // UI\Visitor\Visitor_CheckWWVP.xaml line 30
                {
                    this.pnl_warn = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 8: // UI\Visitor\Visitor_CheckWWVP.xaml line 31
                {
                    this.img_wwvp_warn = (global::Windows.UI.Xaml.Controls.FontIcon)(target);
                }
                break;
            case 9: // UI\Visitor\Visitor_CheckWWVP.xaml line 33
                {
                    this.lbl_wwvpinfo = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // UI\Visitor\Visitor_CheckWWVP.xaml line 27
                {
                    this.img_wwvp_ok = (global::Windows.UI.Xaml.Controls.FontIcon)(target);
                }
                break;
            case 11: // UI\Visitor\Visitor_CheckWWVP.xaml line 23
                {
                    this.prg_wwvp = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 12: // UI\Visitor\Visitor_CheckWWVP.xaml line 24
                {
                    this.lbl_pleasewait = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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

