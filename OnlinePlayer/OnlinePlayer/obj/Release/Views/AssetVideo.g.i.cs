﻿#pragma checksum "..\..\..\Views\AssetVideo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E81C9116A63F8F971DDA26264EFDC4C4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OnlinePlayer.Controls;
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


namespace OnlinePlayer.Views {
    
    
    /// <summary>
    /// AssetVideo
    /// </summary>
    public partial class AssetVideo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Views\AssetVideo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spMain;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Views\AssetVideo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement meAssetVideo;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\AssetVideo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spControls;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\AssetVideo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider volumeSlider;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Views\AssetVideo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider timelineSlider;
        
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
            System.Uri resourceLocater = new System.Uri("/OnlinePlayer;component/views/assetvideo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AssetVideo.xaml"
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
            this.spMain = ((System.Windows.Controls.DockPanel)(target));
            
            #line 10 "..\..\..\Views\AssetVideo.xaml"
            this.spMain.Drop += new System.Windows.DragEventHandler(this.meAssetVideo_Drop);
            
            #line default
            #line hidden
            return;
            case 2:
            this.meAssetVideo = ((System.Windows.Controls.MediaElement)(target));
            
            #line 11 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.Drop += new System.Windows.DragEventHandler(this.meAssetVideo_Drop);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.meAssetVideo_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MouseEnter += new System.Windows.Input.MouseEventHandler(this.meAssetVideo_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MouseLeave += new System.Windows.Input.MouseEventHandler(this.meAssetVideo_MouseLeave);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MediaOpened += new System.Windows.RoutedEventHandler(this.Element_MediaOpened);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MediaEnded += new System.Windows.RoutedEventHandler(this.Element_MediaEnded);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.KeyDown += new System.Windows.Input.KeyEventHandler(this.meAssetVideo_KeyDown);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.meAssetVideo_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\Views\AssetVideo.xaml"
            this.meAssetVideo.MouseMove += new System.Windows.Input.MouseEventHandler(this.meAssetVideo_MouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.spControls = ((System.Windows.Controls.StackPanel)(target));
            
            #line 16 "..\..\..\Views\AssetVideo.xaml"
            this.spControls.MouseEnter += new System.Windows.Input.MouseEventHandler(this.meAssetVideo_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 20 "..\..\..\Views\AssetVideo.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDownPlayMedia);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 22 "..\..\..\Views\AssetVideo.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDownPauseMedia);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 24 "..\..\..\Views\AssetVideo.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDownStopMedia);
            
            #line default
            #line hidden
            return;
            case 7:
            this.volumeSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 27 "..\..\..\Views\AssetVideo.xaml"
            this.volumeSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ChangeMediaVolume);
            
            #line default
            #line hidden
            return;
            case 8:
            this.timelineSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 31 "..\..\..\Views\AssetVideo.xaml"
            this.timelineSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SeekToMediaPosition);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

