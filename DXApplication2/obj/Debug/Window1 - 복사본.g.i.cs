﻿#pragma checksum "..\..\Window1 - 복사본.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F353935DF36F9BEB42BB4C202B74452240ABE4A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using DXApplication2;
using DevExpress.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.DXBinding;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.LayoutControl;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace DXApplication2 {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\Window1 - 복사본.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtBox_depart;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Window1 - 복사본.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstView_depart;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\Window1 - 복사본.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtBox_name;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Window1 - 복사본.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstView_name;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Window1 - 복사본.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_ok;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Window1 - 복사본.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_close;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DXApplication2;component/window1%20-%20%eb%b3%b5%ec%82%ac%eb%b3%b8.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1 - 복사본.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TxtBox_depart = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\Window1 - 복사본.xaml"
            this.TxtBox_depart.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtBox_depart_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lstView_depart = ((System.Windows.Controls.ListView)(target));
            
            #line 28 "..\..\Window1 - 복사본.xaml"
            this.lstView_depart.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstView_depart_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TxtBox_name = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\Window1 - 복사본.xaml"
            this.TxtBox_name.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TxtBox_name_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lstView_name = ((System.Windows.Controls.ListView)(target));
            
            #line 39 "..\..\Window1 - 복사본.xaml"
            this.lstView_name.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstView_name_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Btn_ok = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\Window1 - 복사본.xaml"
            this.Btn_ok.Click += new System.Windows.RoutedEventHandler(this.Btn_ok_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Btn_close = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Window1 - 복사본.xaml"
            this.Btn_close.Click += new System.Windows.RoutedEventHandler(this.Btn_close_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

