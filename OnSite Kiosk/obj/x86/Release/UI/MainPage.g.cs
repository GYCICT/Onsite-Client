﻿#pragma checksum "C:\Users\pprice\source\repos\OnSite Kiosk\OnSite Kiosk\UI\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B6D5A924DCA5B702701F4BDDB2B982A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnSite_Kiosk.UI
{
    partial class MainPage : 
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
            case 1: // UI\MainPage.xaml line 1
                {
                    this.page_home = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)this.page_home).Loaded += this.page_home_Loaded;
                }
                break;
            case 2: // UI\MainPage.xaml line 12
                {
                    this.btn_Back = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btn_Back).Click += this.btn_Back_Click;
                }
                break;
            case 3: // UI\MainPage.xaml line 14
                {
                    this.Logo = (global::Windows.UI.Xaml.Controls.Image)(target);
                    ((global::Windows.UI.Xaml.Controls.Image)this.Logo).Holding += this.Logo_Holding;
                }
                break;
            case 4: // UI\MainPage.xaml line 15
                {
                    this.PageTitleLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // UI\MainPage.xaml line 25
                {
                    this.ContentFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                    ((global::Windows.UI.Xaml.Controls.Frame)this.ContentFrame).Navigated += this.ContentFrame_Navigated;
                }
                break;
            case 6: // UI\MainPage.xaml line 17
                {
                    this.TitleFadeOut = (global::Windows.UI.Xaml.Media.Animation.Storyboard)(target);
                }
                break;
            case 7: // UI\MainPage.xaml line 20
                {
                    this.TitleFadeIn = (global::Windows.UI.Xaml.Media.Animation.Storyboard)(target);
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

