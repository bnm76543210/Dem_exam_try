   M S B A M L   `   `   `  ����N  JPreddiplom_practice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Preddiplom_practicePreddiplom_practice  5   	   $    Preddiplom_practice.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Preddiplom_practice     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   ���#76e383$	��800��6J   $	��450��6Y    � WindowStartupLocation$ CenterScreen=�6i   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��80* �6   �� 5      $
��177* �6   �� 5      $
��177* �6   �� 5      $
��177* �6   �� 5      $	��80* �6   .+�5   
   �� $��Stretch=�6   ���#498c51$��Stretch=�6B   $��0��6^   �� 5   
   -   5         ��Name$ Welcome��5      $
��Wrap=�6%   $&�� ДОБРО ПОЖАЛОВАТЬ!��69   $��Times New Roman)�6R   $��24(�6o   $
��Bold"�6}    �� TextAlignment$
 Left=�6�   $��Center=�6�   $��38��6�   $��Center=�6�   �� 5   
   $��Stretch=�6   ���#498c51$��Stretch=�6B   $��4��6^   � 5   
   $��4��6    � ColumnDefinitions 5      �� 5      $	��50* �6#   �� 5      $	��50* �6#   .+�5      �� $
��Wrap=�6   $ ��Made by Fakhrutdinov Marat��6,   $��Times New Roman)�6N   $��18(�6k   $
��Bold"�6y   $ Center=�6�   $��Center=�6�   $��0,0,0,0q�6�   $��0��6�   $��Center=�6�   �� 5      -   5        $ EnterAsGuest��5      $
��Wrap=�6.   $"��Войти как гость��6B   $��Times New Roman)�6Y   $��18(�6v   $
��Bold"�6�   $ Center=�6�   $��Center=�6�   $��0,0,20,0q�6�   $��1��6�   ���	#FF001AFF$��Right=�6  $��	Underline{�5      64   5      �� 5   
   -   5         ��Name$ TextBox1��5      $��0,0,0,0q�6$   $��1��65    �� TextWrapping$
 Wrap=�6B   $	��480��6V   $��25��6b   $
��Left=�6n   $��Center=�6�   �� 5   
   -   5        $ TextBox2��5      $��Stretch=�6$   $��2��6B   $
 Wrap=�6O   $��Stretch=�6c   $	��480��6   $��25��6�   $��Center=�6�   �� 5   
   -   5         ��Name$ button1��5      $��
ВОЙТИ��6"   $��Stretch=�62   $��Stretch=�6P   $��35��6l   $	��240��6x   $��20(�6�   $
��Bold"�6�   $��Times New Roman)�6�   ���#498c51$��3��6�   	 � 	Resources	 5       
 �� 
TargetType �� Property �� Value��%_   '��       �� 5!      "
 ��6   .�5"      ��  �@��CornerRadius$ 20��65   5#      5$      5%      5&      5'      