   M S B A M L   `   `   `  ����D  @Project 1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Project_1	Project_1  5   	       Project_1.ClientWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Project_1     5   	     � Title$  ClientWindow��5   	   $	��450��6   $	��800��6+    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Padding	 �� Triggers
 P� Property P� Value �� 
TargetName��%w  '��       �� 5
   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ border��6!   $ 17��61   $��1q�6C   
���   �
�������$ 10q�5   -   ._�5      �� $��Center=�6/   $��Center=�6J   5      	 5      P� 5      
 �@J�IsMouseOver$
 True��6=   . �5   "   �� $ border��6)    � ��
 �����6S   5      P� 5      
 � ��$
 True��6;   . �5   "   �� $ border��6)    � ��
 �����6S   5      P� 5      
 � {�$ False��6;   . �5   "   �� $ border��6)    � ��
 �����6S   5      5      5      5       5!      .��5"      � -   5        .+�5#   
   �� -   5         ��Name$ btnFindTranspost��5#      $��Find Transport��6+   $��30(�6D   $
��Left=�6R   $��492,108,0,0q�6m   $	��Top=�6�   $	��109��6�   $	��270��6�   6�   �� 5$   
   -   5        $ btnContractPayments��5$      $��Contract Payments��6.   $��30(�6J   $
��Left=�6X   $��492,251,0,0q�6s   $	��Top=�6�   $	��109��6�   $	��270��6�   6�   �� 5%   
   -   5         ��Name$ lvClient��5%      $��
0,50,332,0q�6=   ��6%   ��  �� View 5&      �� 5'      .P�5(      �� $
��Name��6%    �� Width$	 200��63    �� DisplayMemberBinding 6?   �� *Name+�� 5)      $
��CCCD��6%   $	 150��63    6?   �� *
CCCDClient+�� 5*      $
��Vote��6%   $	 113��63    6?   �� *Vote+5+      5-      5.      5/      