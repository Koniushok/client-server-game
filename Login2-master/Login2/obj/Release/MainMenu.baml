   M S B A M L   `   `   `  ���� C  ?MainGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LoginLogin      Login.MainMenu   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Login     U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  $	��500��$	��350��  � 
ResizeMode$  NoResize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    OpenMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	CloseMenu��%$	  &	        &	 &    �� .��U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ GridMain��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$	 250Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ GridMain��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 50Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ StackPanelMenu��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$	 250Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ listViewItem��.\�  $ 0��$
 -250Q�  $ 0:0:0.5��$
 -250Q�  $ 0:0:0.7��$ 0Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ listViewItem1��.\�  $ 0��$
 -250Q�  $ 0:0:0.5��$
 -250Q�  $ 0:0:0.9��$ 0Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ listViewItem2��.\�  $ 0��$
 -250Q�  $ 0:0:0.5��$
 -250Q�  $ 0:0:1.1��$ 0Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ listViewItem3��.\�  $ 0��$
 -250Q�  $ 0:0:0.5��$
 -250Q�  $ 0:0:1.3��$ 0Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ listViewItem4��.\�  $ 0��$
 -250Q�  $ 0:0:0.5��$
 -250Q�  $ 0:0:1.5��$ 0Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$ button��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1.5��$ 1Q�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$ button��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 0Q�  $ 0:0:1.5��$ 1Q��� .��U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ GridMain��.\�  $ 0��$	 250Q�  $ 0:0:0.5��$ 0Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�$ GridMain��.\�  $ 0��$ 50Q�  $ 0:0:0.5��$ 0Q�U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�$ StackPanelMenu��.\�  $ 0��$	 250Q�  $ 0:0:0.5��$ 0Q�.��� -   ����  �� EndPoint$ 0.5,1&�	 �� 
StartPoint$	 0.5,0&�.��� 
 � Color$
 	#FF2281D1��� $
 	#FF34268A�� � Offset$ 1Q�� $
 	#FF33288B��$ 0.546Q�.+��� -    ��Name$ StackPanelMenu��$	��250��$��
-250 0 0 0q�$
��Left=� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� T� .+���  �� Orientation$ 
Horizontal=�$	��Top=�$	��100��.+��� -    ��Name$ button�� 0 ,MaterialDesignFloatingActionMiniAccentButton#��[ $��0q�$��80��$��80��$��10q�$ 0.5,0.5&���Z� z�V� .~��� �� �� T� ���� -   MyImage�� j� Stretch$ UniformToFill=�(� $	��106��$��0,0,0,44q�.���� -    ��Name$ 	TextLogin��$��Val.bat��$��MS Office Symbol Semibold)�
��������� -    ��Name$ ListViewMenu��$��18(�
�������$��Champagne & Limousines)�.@��� -    ��Name$ listViewItem��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$
��10 0q�.+�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ GamepadVariant=�$��30��$��30��$��Center=�$��5q�
��������� $��Быстрая игра��$��10q�$��Center=�$��20(�
��������� -   $ listViewItem1��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$
��10 0q�.+�  $ Account=�$��30��$��30��$��Center=�$��5q�
��������� $��Профиль��$��10q�$��20(�$��Center=�
��������� -	   $ listViewItem2��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$
��10 0q�.+�  $ Settings=�$��30��$��30��$��Center=�$��5q�
��������� $��Настройки��$��10q�$��20(�$��Center=�
��������� -
   $ listViewItem3��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$
��10 0q�.+�  $ BookOpenVariant=�$��30��$��30��$��Center=�$��5q�
��������� $��Информация��$��10q�$��20(�$��Center=�
��������� -   $ listViewItem4��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$
��10 0q�.+�  $ Export=�$��30��$��30��$��Center=�$��5q�
��������� $��
Выйти��$��10q�$��20(�$��Center=�
�������� -    �Name$ GridMain��
�������$ 0.5,0.5&�z�V� .~��� �� �� T� .+�� -   $ GridViewMenu��$��0,53,0,0q��� -   $��56��$	��Top=�$��	0,0,0.4,0q����	#FF2196F3 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$ 0Q�   RenderingBias$ Quality=�   ShadowDepth$ 1Q�   Color$ 	#FF090303���� $ 
Horizontal=�$��40,0,88.4,447q�.+�  -     Name$ 	IconTitle��$ Account=�$��30��$��30��$��Center=�$��Center=�$��5q�
��������� -   $ 	TextTitle��$��Быстрая игра��$��10q�$��20(�$��Center=�$��Center=�
�������� -   $ 	BlackGrid�����	#E0000000$y�Hidden=�$��
-1,0,0.4,0q��� -   $ 
ButtonMenu��#��[ $��35��$��35��$��0q�$	��Top=�$
��Left=�
�������$��0,8,0,0q���Z� ��Z� .��  $
 Menu=�
�������$��35��$��35���� -   $ ButtonClose��#��[ $
��Auto��$��30��$��0q�$��320,0,0.4,470q�
���������Z� ��Z� .��  $ Power=�