   M S B A M L   `   `   `  ����D  @AniHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AniHelper	AniHelper  5   	       AniHelper.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:AniHelper     5   	   -   5        $��	AniWindow��5   (     � Title$  
MainWindow��5   	   $	��420��6   $	��700��6)   .��5	      �� -   5         ��Name$ 	MainPanel��5	      .+�5
   
   �� -   5        $��	textBlock��5      
�������
����  �$��Century Gothic)�5      $��12(�5      $��UltraExpanded&�5      $��Italic$�5      $��	UltraBold"�5       �� 
LineHeight$
 Auto��5       �� Padding$ 	0,10,0,10q�5       �� TextAlignment$ Center=�5      $
��Wrap=�5       K� NumeralStyle$ OldStyle=�5       K� SlashedZero . Anime Recommendation Helper5      �� 5    
   
�������
���   �$ Center=�5$      $ 5q�5%      GEWhat sort of genres are you interested in? Choose up to 3 categories.5&   U   � 5'   
   -   5         �Name$ 	genreGrid��5(   '   
������� � ColumnDefinitions 5*      �� 5+      $
��140* �6#   62   �� 5,      $
��140* �6#   62   �� 5-      $
��140* �6#   62   �� 5.      $
��140* �6#   62   �� 5/      $
��140* �6#   62   	 � RowDefinitions	 52      �� 53      $��* �6    6-   �� 54      $��* �6    6-   �� 55      $��* �6    6-   �� 56      $��* �6    6-   �� 57      $��* �6    6-   �� 58      $��* �6    6-   �� 59      $��* �6    6-   5<      �� 5=   
   -   5        $ searchFunctions��5=   (   
�������5?      �� 5@   
   -   5        $ 	submitBox��5@   /   
�������5B      5C      5D      