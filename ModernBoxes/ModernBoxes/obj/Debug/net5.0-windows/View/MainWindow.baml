   M S B A M L   `   `   `  ����  ModernBoxes    ModernBoxes.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   S OHandyControl, Version=3.2.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5          �Name$  window��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	   
������  � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	    � Title$ 
MainWindow��5   	   $
��1000��6   $
��1400��6*    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $2 ,/Resource/dictionary/MainWindowResource.xaml?�6%   %    5      .��5      7  1HandyControl.Controls.TransitioningContentControl  .��5   
   �� ._�5      � 	 � ColumnDefinitions	 5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   �� 5      $
��auto �6'   .+�5#      � $��5q�6   $��1��6#   .+�5%         HandyControl.Controls.Card  -   5        $��MiddleWindow��5'      )  #HandyControl.Controls.BorderElement
   CornerRadius$
 8��5&      
�������   EffectShadow1 J� Effect# [  5)   ,   ��5(      ��  �� ElementName$ Toggle�� �� Path$ 	IsChecked�  	toconvert �� 	Converter# [ .��5*      �  � RowDefinitions 5+      �� 5,   "   �� 5-   "   $
��auto �60   .+�5/      ��  �� Orientation$ Vertical=�6)   .+�50   "   d� -   5        $��Toggle��50   /     MainMenuToggleButton#���  6=   $	��Top=�6l   $��40��6�   $��0 16 0 0q�6�    � ContextMenu 51   &   �� 52   *   .@�53   .   m� $��层级��67   .@�54   2   m� -   5        $��topItem��54   Z   $��顶层��6;    m� IsCheckable$
 True��6G    m� 	IsChecked$ False��6�   m� 55   2   -   5        $��
BottomItem��55   [   $��底层��6;   $
 True��6G   $
 True��6�   56   /   m� 57   .   -   5        $��	btn_fixed��57   C   $��固定��67   $ False��6t   m� 58   .   -   5        $��	btn_close��58   C   $��关闭��67   6T   59   +   5;   #   (  "HandyControl.Controls.ScrollViewer  5=   "      Orientation$ Vertical=�62   .��5?   &   �� -   5        $��items��5?   T   ��63   �� *
MenuList+��5@   *   �� 5A   .   .R�5B   2   �� $ Vertical=�6=   5C   /   ��5F   *   �� 5G   .   .R�5H   2   �� $ 
Horizontal=�6=   $��0 6 0 0q�6W   .+�5K   6   	� $	��200��5L   :   $��Center=�5O   :     	MenuStyle#��[ 5P   :   $��Center=�5Q   :    	� 	GroupName$
 menu��5R   :   ��5M   :   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ItemsControl$ DataContext.OpenApp���5N   :   �� *Target+��5S   :   �� *
MenuName+ � ToolTip 5T   :   �� *
MenuName+ 5U   6   �� 5V   :   .@�5W   >   m� $��
AddCardApp��6W   -	   5        $��添加卡片应用��5W   G   y�6�   �� *
MenuName+  IsShowContextMenu# [ m� 5X   >   $��BtnDeleteMenuItem��6S   -
   5        $��删除��5X   G    m� CommandParameter 6�   �� *
MenuName+5Y   ;   5[   3   5\   /   5^   '   5_   #   �� 5`   "   $ 添加��6,   $	����6:   $��/Resource/#iconfont)�6J   $��Center=�6k   $��25(�6�   $��0 10 0 0q�6�   
�������.  (System.Windows.Interactivity.Interaction   Triggers 5a   &   /  )System.Windows.Interactivity.EventTrigger  5b   *      	EventName$ MouseLeftButtonDown��69   .  (System.Windows.Interactivity.TriggerBase   Actions. 5c   .   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6D   �� *AddMenuDialog+5d   +   5f   #   5g      �� 5i      $��1��6)   .+�5j   "   �� $	����5k   %   $��Center=�5l   %   $��16q�5m   %   $��/Resource/#iconfont)�5n   %   
�������$��20(�5p   %    5q   &     5r   *   $ MouseLeftButtonDown��69   . 5s   .      6D   �� *OpenSetDialog+5t   +   5v   #   5w      5x      5y      5{        5~      -   5        $��compontentLayoutLeft��5~   a   $��
10 5 10 10q�61   $	��440��6E   $��0��6Q   $y�Hidden=�6}   .��5      �� -   5         ��Name$ LeftContent��5   %   5�        5�      -   5        $��compontentLayoutRight��5�   a   $��
10 5 10 10q�61   $	��440��6E   $��2��6Q   $y�Hidden=�6~   .��5�      �� -   5        $ RightContent��5�   %   5�      &	   HandyControl.Controls.NotifyIcon	  5�      -   5          	 Name$  NotifyIconContextContent��5�       ! 	  Text$! 木函��6B   $y�Visible=�6L   " 	  Icon$" /Resource/logo.ico��6a    5�      �� 5�      .@�5�      m� # m� Icon$	# ��6'   $��Center=�67   $��Center=�6R   $��/Resource/#iconfont)�6o   $ m� Command$$$ hc:ControlCommands.ShutdownApp��6�   $��退出程序��6�   5�      ,
  &HandyControl.Interactivity.Interaction% 
  Triggers% 5�      -  'HandyControl.Interactivity.EventTrigger  5�      &   	EventName$& Click��6*   ,  &HandyControl.Interactivity.TriggerBase'   Actions.' 5�      /  )HandyControl.Interactivity.EventToCommand  (   Command$+( %hc:ControlCommands.PushMainWindow2Top��60   5�      5�      5�      5�      5�      5�      