   M S B A M L   `   `   `  ����  WpfApp1    WPFTaskManager.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Task Manager��5   	   $	��500��6   $	��650��6+   ���#2E2E2E � 	Resources 5         BoolToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5	      � .+�5
   
   �� $��10q�6   ���#3A3A3A.+�5      �� $��Task Manager��6   $��24(�6,   $
��Bold"�6:   
�������$��Center=�6_   $��10q�6|   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��10q�6O   .+�5      �� -   5        $��Start New Task��5      $��5q�6G   $��40��6R   $	��140��6^   ���#4CAF50
�������$
��Bold"�6�   �� 5      -   5        $��List Running Tasks��5      $��5q�6H   $��40��6S   $	��160��6_   ���#2196F3
�������$
��Bold"�6�   5      �� 5      -   5         ��Name$ TaskList��5      $	��250��6(   $��5q�65   ���#1E1E1E���#4CAF50
���������5      �� 5      .R�5      �� ���#4CAF50$��1q�67   $��5q�6K    �� Padding$ 5q�6V   ._�5      �� $ Vertical=�6)   $��5q�6@   .+�5   "   �� $
��Bold"�6F   
���������6,   �� *
TaskName+6X   �� 5   "   $"��Priority: {Binding Priority}��6,   
��� ���6P   �� 5   "   
���������6,   �� *CompletionTime+ �� StringFormat$  Completed at: {0:HH:mm:ss}��y�6�   �� *TaskCompleted+ �� 	Converter# [  � 5   "   $��20��6I   $	��250��6U    � Value 6.   �� *
Progress+6U   �� 5   "   $ 
Horizontal=�6-   $��Center=�6F   .+�5   &   �� -   5        $��Pause��5   -   $��5q�6O   $��30��6Z   $��75��6f   ���#FF9800
�������$
��Bold"�6�   y�6�   �� *
IsPaused+# [   �� ConverterParameter$ False���� 5   &   -   5        $��Resume��5   -   $��5q�6Q   $��30��6\   $��75��6h   ���#4CAF50
�������$
��Bold"�6�   y�6�   �� *
IsPaused+# [  $
 True��5   #   5      5       5!      5#      �� 5%      $ 
Horizontal=�6   $��Center=�62   .+�5&      �� -   5        $��Stop Selected Task��5&      $��5q�6G   $��40��6R   $	��170��6^   ���#FF5722
�������$
��Bold"�6�   �� 5'      -   5        $
��Exit��5'      $��5q�6@   $��40��6K   $	��120��6W   ���#D32F2F
�������$
��Bold"�6�   5(      5)      5*      5+      