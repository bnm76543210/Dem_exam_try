   M S B A M L   `   `   `  ����N  JPreddiplom_practice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Preddiplom_practice.WindowsPreddiplom_practice.Windows  5   	   (    "Preddiplom_practice.Windows.Basket   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Preddiplom_practice.Windows     5   	     � Title$  Basket��5   	   $	��450��6   $	��800��6%   ���#76e383$	��800��6F   $	��450��6U    � WindowStartupLocation$ CenterScreen=�6e   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��320* �6   �� 5      $	��50* �6   .+�5   
   �� -   5         ��Name$ myList��5      $��Hidden=�6#   $��0��6W   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$ Stretch=�6C   5      ��5      �� 5      .R�5      �� 
���   �$��1q�6[   $��11��6o   $��2��6�   ��61   �� *ListItemBackground+._�5      �  � ColumnDefinitions 5      �� 5   "   $
��150* �63   �� 5   "   $
��350* �63   �� 5   "   $
��100* �63   .+�5      �� $	��100��6$   $	��150��61   $��0��6=   $��Center=�6m   $��Center=�6�   $
��Fill=�6�   ��6M   �� *ProductPhoto+6�   �� 5        �� Orientation$ Vertical=�6)   $��1��6@   $��Center=�6P   $
��10,5q�6k   .+�5!   "   �� $��22(�6I   $
��Bold"�6W   ��6,   �� *ProductName+6k   �� 5"   "   ��6,   �� *ProductDescription+6S   �� 5#   "   	 ��Name$	 ManufacturerLabel��6,   ��6G   �� *-+ProductManufacturer.ProductManufacturerName+
 �� StringFormat$%
 Производитель: {0}��6�   �� 5$   "   ��6,   �� *Decorations+��6T   �� *ProductCost+$
 Цена: {0}��6�   �� 5%   "   
���  ����6,   �� *DiscountPrice+y�6\   �� *	IsVisible+6   5&      �� 5'      $��2��6)   $��Center=�69   .+�5(   "   �� ��6,   �� *ProductDiscountAmount+$%
 Размер скидки: {0}%��6y   5)      5*      5+      5,      5.      � 5/   
   $��1��6    50      �� 51      $	��50* �6#   �� 52      $	��50* �6#   �� 53      $	��50* �6#   .+�55      �� -   5         ��Name$ OrderResult��55      $)��#Сформировать заказ��6*   $��Center=�6G   $��Center=�6d   $��25��6   $	��240��6�   $��20(�6�   $
��Bold"�6�   $��Times New Roman)�6�   ���#498c51$��1��6   $��0,0,10,0q�6   � 	Resources 56      ��%_   '��       �� 57      " ��6   .�58      ��  �@��CornerRadius$ 10��69   59      5:      5;      �� 5<      -   5         ��Name$ PointOfIssue��5<      $��Stretch=�6.   $��-3,0,0,0q�6L   $��2��6^   $��0��6k   $��Center=�6|   $	��200��6�    �� 
IsEditable .  �� 
IsReadOnly .  �� Text$ Пункт выдачи��5=   7   $
��Left=�6K   �� 5>      -   5         ��Name$ 	UserLabel��5>      $��2��6'   $��Marat��67   $��Right=�6G   $��0,0,10,0q�6c   $��Center=�6u   $��14(�6�   5?      5@      5A      