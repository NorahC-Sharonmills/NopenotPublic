﻿#pragma checksum "C:\Users\God\Documents\GitHub\NopenotPublic\Unsflash\View\ExplorePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "40C12AF85C18B3597DBF86192205B4B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Unsflash.View
{
    partial class ExplorePage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\ExplorePage.xaml line 37
                {
                    this.Autosg = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 3: // View\ExplorePage.xaml line 47
                {
                    this.btbusiness = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btbusiness).Click += this.btbusiness_Click;
                }
                break;
            case 4: // View\ExplorePage.xaml line 50
                {
                    this.btcomputer = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btcomputer).Click += this.btcomputer_Click;
                }
                break;
            case 5: // View\ExplorePage.xaml line 53
                {
                    this.btnature = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnature).Click += this.btnature_Click;
                }
                break;
            case 6: // View\ExplorePage.xaml line 56
                {
                    this.btlove = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btlove).Click += this.btlove_Click;
                }
                break;
            case 7: // View\ExplorePage.xaml line 59
                {
                    this.bthouse = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.bthouse).Click += this.bthouse_Click;
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

