   M S B A M L   `   `   `  ����N  JPreddiplom_practice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Preddiplom_practice.WindowsPreddiplom_practice.Windows  5   	   -    'Preddiplom_practice.Windows.AdminWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Preddiplom_practice.Windows     5   	     � Title$  AdminWindow��5   	   $	��450��6   $	��800��6*   ���#76e383$	��800��6K   $	��450��6Z    � WindowStartupLocation$ CenterScreen=�6j   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��80* �6   �� 5      $
��320* �6   �� 5      $	��50* �6   .+�5   
   � $��0��6   $��Center=�6   $��	10,0,10,0q�67    � ColumnDefinitions 5      �� 5      $
��200* �6#   �� 5      $
��400* �6#   �� 5      $
��200* �6#   .+�5      �� -   5         ��Name$ 	Cathegory��5      $��0��6*    �� 
IsEditable .  �� 
IsReadOnly .  �� Text$, &Все диапазоны скидок��6^   $��Center=�6z   .@�5      �� $,��&Все диапазоны скидок��6   �� 5      $(��"Диапазон от 0 до 9,99%��6   �� 5      $*��$Диапазон от 10 до 14,99%��6   �� 5      $-��'Диапазон от 15% и более��6   5      �� 5      -   5         ��Name$ Search��5      $��1��6&   $��	10,0,10,0q�66   $��Center=�6I   6k   �� 5      -   5        	 ��Name$	 Quantity��5      $��2��6&   $��0 из 0��66   $��Right=�6G   $��0,0,10,0q�6c   $��Center=�6u   $��16(�6�   5      �� 5   
   -   5        
 ��Name$
 myList��5      $��Hidden=�6#   $��1��6W   ��5      �� 5        �� 
TargetType" ��6   .�5!      ��  �� Property � �� �� Value$ Stretch=�6C   5"      ��5$      �� 5%      .R�5&      �� 
���   �$��1q�6[   $��11��6o   $��2��6�   ��61   �� *ListItemBackground+._�5'      �  5(      �� 5)   "   $
��150* �63   �� 5*   "   $
��350* �63   �� 5+   "   $
��100* �63   .+�5.      �� $	��100��6$   $	��150��61   $��0��6=   $��Center=�6m   $��Center=�6�   $
��Fill=�6�   ��6M   �� *ProductPhoto+6�   �� 50       �� Orientation$ Vertical=�6)   $��1��6@   $��Center=�6P   $
��10,5q�6k   .+�51   "   �� $��22(�6I   $
��Bold"�6W   ��6,   �� *ProductName+6k   �� 52   "   ��6,   �� *ProductDescription+6S   �� 53   "    ��Name$ ManufacturerLabel��6,   ��6G   �� *-+ProductManufacturer.ProductManufacturerName+ �� StringFormat$% Производитель: {0}��6�   �� 54   "   ��6,   �� *Decorations+��6T   �� *ProductCost+$ Цена: {0}��6�   �� 55   "   
���  ����6,   �� *DiscountPrice+y�6\   �� *	IsVisible+6   56      �� 57      $��2��6)   $��Center=�69   .+�58   "   �� ��6,   �� *ProductDiscountAmount+$% Размер скидки: {0}%��6y   59      5:      5;      5<      5>      � 5?   
   $��2��6   $��Center=�6   $��	10,0,10,0q�67    5@      �� 5A      $
��400* �6#   �� 5B      $
��400* �6#   .+�5D      �� -   5        $ 	SortingBy��5D      $
��Left=�6+   $��10,0,0,0q�6F   $��0��6X   $��Center=�6h   $	��200��6�    .  . $! Без сортировки��5E   7   .@�5F      �� $!��Без сортировки��6   �� 5G      $8��2Сортировать по возрастанию��6   �� 5H      $2��,Сортировать по убыванию��6   5I      �� 5J      -   5         ��Name$ button1��5J      $!��Добавить товар��6&   $��Right=�6?   $��Center=�6[   $��25��6v   $	��240��6�   $��20(�6�   $
��Bold"�6�   $��Times New Roman)�6�   ���#498c51$��1��6�   $��0,0,10,0q�6	   � 	Resources 5K      ��%_   '��       �� 5L      " ��6   .�5M      ��  �@��CornerRadius$ 10��69   5N      5O      5P      5Q      5R      5S      