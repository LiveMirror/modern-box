   M S B A M L   `   `   `  ����S  OHandyControl, Version=3.2.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5bS9clr-namespace:HandyControl.Controls;assembly=HandyControlHandyControl.Controls  5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   CbCardAppStyle  	BaseStyle  �� BasedOn �� 
TargetType �� Property �� Value  BorderBrush �� 
TargetType ��Name �� CornerRadius �Name J� SnapsToDevicePixels	 � ColumnDefinitions     HandyControl.Controls.Card)   #HandyControl.Controls.BorderElement
   CornerRadius  EffectShadow1 J� Effect    Header)   #HandyControl.Controls.PreviewSlider � Minimum � Maximum �� Orientation
 � Tag � Value   PreviewContent  LabelPrimary �� ContentStringFormat �� Path �� RelativeSource.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions6  0System.Windows.Interactivity.InvokeCommandAction   Command �� Mode �� AncestorType   CommandParameter �� ElementName �� Triggers P� Property  P� Value! �� 
TargetName  EffectShadow5�� 5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      @hc9clr-namespace:HandyControl.Controls;assembly=HandyControl  5      %8  &	        7  7  7  7  �� 5      8    " ��6H   .�5   
   ��  � ��
 �����6'   �� 5   
    � ��# �  6(   �� 5	   
    � ��$ 1q�6,   �� 5
   
    � ��$ Center=�60   �� 5   
    � ��$ Center=�6.   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ 6,0,0,0q�6$   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ border��6   $ 16��6-   $��5q�6?   ._�5      � $ templateRoot��6   
������ $��5q�6N   $
 True��6Y   	 5      �� 5   "   $
��Auto �63   �� 5   "   $��* �63   .+�5         $
��card��62   $	��210��6&   $	��125��6?   $��0��6L   $
 16��6\   8   5   "   �� 5   &   $��	10 10 5 5q�6J   $
��Left=�6]   $��Center=�6x   ��60   �� *
CardName+6x   .��5   "   �� $
��Fill=�6C   $��4q�6R   ��6(   �� *	Priview+6R   5      �� 5      $��1��6%   
������ $��1q�6O   $ 16��6c   ._�5    "     $��slider��5"   %   $	��125��5#   %   $	 100Q�5&   %   $	 500Q�5'   %   $ Vertical=�5(   %   $��5q�5)   %   $��Center=�5*   %   {�5!   %   �� *	IsChecked+ 5$   %   �� *CardID+ 5%   %   �� *
CardHeight+ 5+   &   �� 5,   *   8�� $
 #0.0��6�   ��6W   �� $( "(hc:PreviewSlider.PreviewPosition)� �� *Self+6�    5.   &     5/   *   $ ValueChanged��69   . 50   .      6D   ��  �� $ FindAncestor=� ItemsControl$" DataContext.CardHeightChange� 6�   �� $ slider��51   +   53   #   54      55      56       58      P� 59       �@d�	IsChecked$
  TrueY�67   . �5:      �� $! border��6;    � �� �#adc4fa6P   5;      P� 5<       �@J�IsMouseOver$
  True��69   . �5=      �� $
! card��67   8   �@J�Effect6J   5>      5@      5B      5C      5D      