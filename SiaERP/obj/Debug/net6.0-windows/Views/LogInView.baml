   M S B A M L   `   `   `  ����A  =SiaERP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SiaERP.ViewsSiaERP.Views  5   	   5clr-namespace:SiaERP.ViewModelsSiaERP.ViewModels  5          SiaERP.Views.LogInView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SiaERP.Views     5   	   5	ViewModelclr-namespace:SiaERP.ViewModels     5        � Title$  	LogInView��5	   	   $	��240��6   $	��256��6(    � WindowStartupLocation$ CenterScreen=�5
       � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5       � AllowsTransparency$
 True��5      
������  � DataContext 5      &   SiaERP.ViewModels.LogInViewModel  5      .��5      �� -   5        
������� �� CornerRadius$ 0��5      ._�5      �� $	��146��6   .+�5      �� $��Usuario��6   $��0 64 0 0q�6   �� 5      $��Arial)�5      $��14(�5      ��5      �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�.��5      	 J� InputBindings	 5      �� 5      

 �� Key$
 Enter��6!    �� Command 6-   �� *CmdEnterKey+.��5   )   5      �� 5       $��Contraseña��6   $��0 8 0 0q�6*   �� 5!      $��Arial)�5"      $��14(�5#      $��0 0 0 16q�5$      ��5!      �� *
Password+$ PropertyChanged=�$ TwoWay=�.��5$      	 5%      �� 5&      $
 Enter��6!    6-   �� *CmdEnterKey+.��5'   )   5(      �� 5*       �� Orientation$ 
Horizontal=�6   .+�5+      �� -   5         ��Name$ BtnLogIn��5+      $��Entrar��5-      $��64��5.      $��0 0 16 0q�5/      ��5,      �� *
CmdLogIn+5/      �� 50      -   5        $ 	BtnCancel��50      $��Cancelar��52      $��64��53      ��51      �� *	CmdCancel+53      54      55      56      57      