   M S B A M L   `   `   `  ���� C  ?MainGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LoginLogin      Login.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:Login     U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     �Name$  window�� � Title$ Login��$	��500��$	��350�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency . $��Segoe UI Emoji)�.��� -    �Name$ MainGrid��.+��� -   $	��280��$	��Top=�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.��� 	 � Color$	 	#FF2281D1��� $	 	#FF34268A��
 � Offset$
 1Q�� $	 	#FF33288B��$
 0.546Q��� $	��280��$	��240��$��Bottom=�$
��0,80q� �� RadiusY$ 10�� �� RadiusX$ 10��
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$ 0Q�   ShadowDepth$ 1Q�   Color$ 	#FFBBBBBB��� -   $ GridPrincipal��$��Bottom=�$��35,0,35.4,65q�$	��255���� -   $��Регистрация��$��Center=�$��Bottom=�$��141,0,140.4,40q�
������� � Cursor$
 Hand���� -    ��Name$ ButtonFechar�� 0  ,MaterialDesignFloatingActionMiniAccentButton#��[  $��25��$��25��
�������$��325,0,0.4,475q���Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=��� -   $ ButtonSettings2��#��[  $��25��$��25��
�������$��300,0,25.4,475q���Z� ��Z� .��  $ Settings=�  -     Name$ KindServerStatus��$ SignalVariant=�
���  ��$��20��$��20���� -	   $ ButtonHome2��#��[  $��25��$��25��
�������$��300,0,25.4,475q�$y�Hidden=���Z� ��Z� .��  $
 Home=��� -
   $ ButtonHome1��#��[  $
��Auto��$
��Auto��
�������$��133,38,134.4,379q���Z� ���� $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF0F71DC��$
 0Q�� $	 	#FF200979��$
 1Q�.��  $
 Home=�$��82��$��82���� -   $ ButtonSettings1��#��[  $
��Auto��$
��Auto��
�������$��133,38,134.4,379q�$y�Hidden=���Z� ���� $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF0F71DC��$
 0Q�� $	 	#FF200979��$
 1Q�.��  $ Settings=�$��82��$��82��(�  (� Stretch$ Uniform=�$��80,465,80.4,20q�.���� -    ��Name$ Result��$��Stretch=�
���  ��$��Black"�$��Stretch=�$y�Hidden=�$6��0Не верный логин или пароль��