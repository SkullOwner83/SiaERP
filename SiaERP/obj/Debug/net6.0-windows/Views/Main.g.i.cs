﻿#pragma checksum "..\..\..\..\Views\Main.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E6D42A98AFEF3537F532E34E1B7402D798FA6F5D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SiaERP.Views {
    
    
    /// <summary>
    /// Main
    /// </summary>
    public partial class Main : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition PnlMenu;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel PnlHeader;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnWindowClose;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnWindowMaximize;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Views\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnWindowMinimize;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SiaERP;component/views/main.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Main.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PnlMenu = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 2:
            this.PnlHeader = ((System.Windows.Controls.StackPanel)(target));
            
            #line 44 "..\..\..\..\Views\Main.xaml"
            this.PnlHeader.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MtdDragWindow);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BtnWindowClose = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\..\Views\Main.xaml"
            this.BtnWindowClose.Click += new System.Windows.RoutedEventHandler(this.MtdButtonClicked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BtnWindowMaximize = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\Views\Main.xaml"
            this.BtnWindowMaximize.Click += new System.Windows.RoutedEventHandler(this.MtdButtonClicked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnWindowMinimize = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\..\Views\Main.xaml"
            this.BtnWindowMinimize.Click += new System.Windows.RoutedEventHandler(this.MtdButtonClicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

