   M S B A M L   `   `   `  ����A  =Login2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GameGame  5   	       Game.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Game     5   	   U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          �Name$  window��5   	    � Title$ Login��5
   	   $	��500��5   	   $	��350��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . $��Segoe UI Emoji)�5   	   .��5      � -   5         �Name$ MainGrid��5      .+�5   
   �� -   5        $	��280��5      $	��Top=�5      ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�5   &   .��5      � 	 � Color$	 	#FF2281D1��6#   � 5      $	 	#FF34268A��6#   
 � Offset$
 1Q�5   #   � 5      $	 	#FF33288B��6#   $
 0.546Q�5    #   5!      5#      �� 5%   
   $	��280��6   $	��240��5&      $��Bottom=�5'      $
��0,80q�5(       �� RadiusY$ 10��5)       �� RadiusX$ 10��5*      
������� J� Effect 5,      3  -System.Windows.Media.Effects.DropShadowEffect  5-         
BlurRadius$ 15Q�6#      	Direction$ 0Q�5.   #      ShadowDepth$ 1Q�5/   #      Color$ 	#FFBBBBBB��50   #   52      � 54   
   -   5        $ GridPrincipal��57      $��Bottom=�54      $��35,0,35.4,65q�55      $	��255��56      58      �� 5:   
   -   5        $��Регистрация��5:      $��Center=�5;      $��Bottom=�5<      $��141,0,140.4,40q�5=      
������� � Cursor$
 Hand��5?      5@      �� 5B   
   -   5         ��Name$ ButtonFechar��5B       0  ,MaterialDesignFloatingActionMiniAccentButton#��[  5C      $��25��5D      $��25��5E      
�������$��325,0,0.4,475q�5J      ��5F      Z� ��5G      Z� .��5K      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�6&   5L      �� 5N   
   -   5        $ ButtonSettings2��5N      #��[  5O      $��25��5P      $��25��5Q      
�������$��300,0,25.4,475q�5U      ��5R      Z� ��5S      Z� .��5W        $ Settings=�6&   5X        5Z   
   -   5          Name$ KindServerStatus��5[   "   $ SignalVariant=�5Z   "   
���  ��$��20��5]   "   $��20��5^   "   �� 5`   
   -	   5        $ ButtonHome2��5`      #��[  5a      $��25��5b      $��25��5c      
�������$��300,0,25.4,475q�5g      $y�Hidden=�5i      ��5d      Z� ��5e      Z� .��5j        $
 Home=�6&   5k      �� 5m   
   -
   5        $ ButtonHome1��5m      #��[  5n      $
��Auto��5o      $
��Auto��5p      
�������$��133,38,134.4,379q�5s      ��5r      Z� ��5t      �� 5u      $ 0.5,1&�6&   $ 0.5,0&�5v   &   .��5w      � $	 	#FF0F71DC��6#   $
 0Q�5x   #   � 5y      $	 	#FF200979��6#   $
 1Q�5z   #   5{      .��5}        $
 Home=�6&   $��82��5~   &   $��82��5   &   5�      �� 5�   
   -   5        $ ButtonSettings1��5�      #��[  5�      $
��Auto��5�      $
��Auto��5�      
�������$��133,38,134.4,379q�5�      $y�Hidden=�5�      ��5�      Z� ��5�      �� 5�      $ 0.5,1&�6&   $ 0.5,0&�5�   &   .��5�      � $	 	#FF0F71DC��6#   $
 0Q�5�   #   � 5�      $	 	#FF200979��6#   $
 1Q�5�   #   5�      .��5�        $ Settings=�6&   $��82��5�   &   $��82��5�   &   5�      (� 5�   
    (� Stretch$ Uniform=�6   $��80,465,80.4,20q�5�      .��5�      �� -   5         ��Name$ Result��5�      $��Stretch=�5�      
���  ��$��Black"�5�      $��Stretch=�5�      $y�Hidden=�5�      $6��0Не верный логин или пароль��5�      5�      5�      5�      