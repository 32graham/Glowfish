   M S B A M L   `   `   `  ����  Glowfish    Glowfish.EditClientDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	      windowStyle#��[  5   	   $	��600��6.   $	��400��6;     � ShowInTaskbar  .   � WindowStartupLocation$ CenterScreen=�6]    � 
ResizeMode$ 	CanResize=�6�   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Right=�6-     darkColorStyle#��[ 6I   .��6q   
First Name6}   �� 5   
   $��1��6   $��0��6   $��Right=�6-   #��[ 6I   .��6q   	Last Name6|   �� 5   
   $��2��6   $��0��6   $��Right=�6-   #��[ 6I   .��6q   Level 1 Minutes6�   �� 5   
   $��3��6   $��0��6   $��Right=�6-   #��[ 6I   .��6q   Level 2 Minutes6�   �� 5   
   $��4��6   $��0��6   $��Right=�6-   #��[ 6I   .��6q   Level 1 Tan Through Date6�   �� 5   
   $��5��6   $��0��6   $��Right=�6-   #��[ 6I   .��6q   Level 2 Tan Through Date6�   �� 5   
   -   5        $��firstNameTextBox��5      $��0��6*   $��1��67   $��Stretch=�6G     lightColorStyle#��[ 6e   $��Center=�6�   $��3q�6�   �� 5   
   -   5        $��lastNameTextBox��5      $��1��6)   $��1��66   $��Stretch=�6F   #��[ 6d   $��Center=�6�   $��3q�6�   �� 5   
   -   5        $��level1MinutesTextBox��5      $��2��6.   $��1��6;   $��Stretch=�6K   #��[ 6i   $��Center=�6�   $��3q�6�   �� 5   
   -   5        $��level2MinutesTextBox��5      $��3��6.   $��1��6;   $��Stretch=�6K   #��[ 6i   $��Center=�6�   $��3q�6�   &   System.Windows.Controls.Calendar  5   
   -   5        $"��level1TanThroughDateCalendar��5      $��4��67   $��1��6D   #��[ 6T   $��Stretch=�6}   $��0q�6�   ��6�   Z� 6�     5    
   -   5        $"��level2TanThroughDateCalendar��5       $��5��67   $��1��6D   #��[ 6T   ��6}   Z� 6�   �� 5"   
   $��6��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Bottom=�6_   $��Right=�6z   .+�5#      �� -   5        $��okButton��5#   :     buttonStyle#��[ 6    �� 	IsDefault . .��6r   _Ok6w   �� 5$      -   5        $��applyButton��5$   :   #��[ 6   .��6g   _Apply6o   �� 5%      -	   5        $��cancelButton��5%   :   #��[ 6    �� IsCancel . .��6^   	_Cancel6g   5&      5*      5+      