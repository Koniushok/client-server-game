   M S B A M L   `   `   `  ���� B  >GameMap, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GameGame  R NCircularProgressBar.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullR>clr-namespace:DesignInControl;assembly=CircularProgressBar.WPFDesignInControl     Game.MainGameView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:Game     ZDesignInControl>clr-namespace:DesignInControl;assembly=CircularProgressBar.WPF     U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency .  � Title$ MainGameView��$	��600��$	��600�� � 	Resources ��%    .���  � RowDefinitions �� $	��37* ��� $
��163* �.+�� -    �Name$ GridMap��$��
0,25.4,0,0q�$��1��.+�(� -    (�Name$ 
MapViewbox��.��   Game.MainMap  -   	  Name$	 MapView��� -   $ 	BlackGrid�����	#CC000000$y�Hidden=�$��2��(� $��50,25.8,50,10q�$��1��.��� -   $ MainTaskView���� -   $	��131��$	��Top=�$��0,0,1,0q����	#FF089CFF$��2��
 J� Effect
 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$ 0Q�   ShadowDepth$ 1Q�   Color$ 	#FF090303���� -    ��Name$ 
MyPointAdd��$��Stretch=����	#FF0BF741$��Stretch=�$��+3000��$��Playbill)�$
��Bold"�$��104,66,385,486q�$��55(�$��2����Z� �� -   $ EnemyPointAdd��$��Stretch=����	#FF0BF741$	��Top=�$��+3000��$��Playbill)�$
��Bold"�$��455,66,42,0q�$��55(�$��52��$��2����Z� � -	   $ 
InfPlayer1��$��351,0,0,469q�$��2��.+��� $��97��$��Bottom=� �� RadiusY$ 10�� �� RadiusX$ 10��
�������$��15q�
   $ 30Q�$ 0Q�$ 1Q��� -
    0  ,MaterialDesignFloatingActionMiniAccentButton#��[  $��0q�$
��Auto��$
��Auto�� J� RenderTransformOrigin$ 0.5,0.5&�$��23,29,151,27q���Z� z�V� .~��� �� �� T� ���� -   
EnemyImage�� j� Stretch$ UniformToFill=�(�  (� Stretch$ Uniform=�$��103,65,20,10q�.���� -   $ EnemyPoints��$��Stretch=����	#FFB0360D$��Stretch=�$
��3500��$��Playbill)�$
��Bold"���Z� (� $ Uniform=�$��103,19,20,66q�.���� -   $ 	EnemyName��$��Stretch=����	#FFA8220B$��Stretch=�$��Val.bat��$��Impact)���Z� � -   $ EnemyGridTime��$y�Hidden=�.+��� $��27��$��Bottom=�$ 10��$ 10��
�������$��23,0,150.8,2.2q�
   $ 30Q�$ 0Q�$ 1Q��� -   $ TextTimeEnemy��$��02:34��$��33,100,159.8,4.2q�$��23(�� -   $ 
InfPlayer2��$��0,0,351,469q�$��2��.+��� $��97��$��Bottom=�$ 10��$ 10��
�������$��15q�
   $ 30Q�$ 0Q�$ 1Q��� -   #��[  $��0q�$
��Auto��$
��Auto��$ 0.5,0.5&�$��23,29,151,27q���Z� z�V� .~��� �� �� T� ���� -   MyImage��$ UniformToFill=�(� $ Uniform=�$��103,65,20,10q�.���� -   $ MyPoints��$��Stretch=����	#FF00910C$��Stretch=�$
��3500��$��Playbill)�$
��Bold"���Z� (� $ Uniform=�$��103,19,20,66q�.���� -   $ MyName��$��Stretch=����	#FF348E12$��Stretch=�$��Val.bat2��$��Impact)���Z� � -   $ 
MyGridTime��$y�Hidden=�.+��� $��27��$��Bottom=�$ 10��$ 10��
�������$��23,0,150.8,2.2q�
   $ 30Q�$ 0Q�$ 1Q��� -   $ 
TextTimeMy��$��02:34��$��33,100,159.8,4.2q�$��23(�)  #DesignInControl.CircularProgressBar  $��Center=�$��Center=�   
Percentage$	 100Q�   StrokeThickness$ 9��   SegmentColor �	#4C000D40   Radius$ 48��$��244,13,238,470q�$	��117��$	��118��$��2��  -     Name$ 
TimeStatus��$��Center=�$��Center=�$	 100Q�$ 9�� �	#FF7400FF$ 48��$��244,13,238,470q�$	��117��$	��118��$��2���� -   #��[  $��0q�$
��Auto��$
��Auto��$ 0.5,0.5&�$��250,29,248,0.2q���Z� z�V� .~��� �� �� T� ���� -   	StatusImg��$ Uniform=�� -   $ 
ResultGrid��$��-1,60.8,1,69q�$��1�����	#4C000000$y�Hidden=�� -   $ GridProfile��$	��447��$��350.4��$��124,19.8,126,22q�$y�Visible=�$��1���� -    ��Name$ ButtonClose��#��[  $
��Auto��$
��Auto��$��0q�$��577,0,0,92.2q�
���������Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�� -   $ GridExit�����	#CC000000$y�Hidden=�$��2��.+��� $	��150q�$ 10��$ 10��
�������$	��177��$ 0.5,0.5&�z�V� .~���  �� ScaleY$ -1Q��� �� T� 
   $ 40Q�$ 0Q�$ 1Q��� $��Выйти из игры?��$��40(�$��156,220,144,215q��� -   $��166,337,334,231q�$
��ДА��$��15(��� -   $��335,337,165,231q�$��НЕТ��$��15(�'  !MaterialDesignThemes.Wpf.Snackbar  -      Name$ Snackbar��   IsActive .  ���	#E5000000$��156,0q�$��1��   Message. .  (MaterialDesignThemes.Wpf.SnackbarMessage  -!       ActionContent$  OK��.��(  "MaterialDesignThemes.Wpf.RatingBar  -"   !  Name$! BasicRatingBar��"   Value$" 0��
�������