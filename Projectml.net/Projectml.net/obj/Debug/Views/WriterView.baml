   M S B A M L   `   `   `  ����H  DProjectml.net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Projectml.net.ViewsProjectml.net.Views  5   	   $    Projectml.net.Views.WriterView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:Projectml.net.Views     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          �Name$  WriterWindow��5   	    � Title$ 
WriterView��5
   	   $��656.596��6   $	��800��6-   .��5      � 
�������.+�5   
   �� $��Train Model��6   $
��Left=�6'   $��	45,40,0,0q�6B   $	��Top=�6U   $	��291��6m   $��Train��6�   $��35��6�   
���`���
���E��
���������6y   �� 6�   �� 5   
   $
��Left=�6/   $��	45,79,0,0q�6J   $	��Top=�6]   $��32��6u   $	��112��6�   ��6   �� *
IsTraining+6�   �� 5   
   $
��Left=�6,   $��
384,79,0,0q�6G   $	��Top=�6[   $��32��6s   $	��112��6   ��6   �� *	IsSaved+6   �� 5   
   $
��Left=�6   $��32��6-   $��217,149,0,0q�69    �� TextWrapping$
 Wrap=�6N   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ titel��6b   $	��Top=�6�   $	��340��6�   
���������6�   �� *	TextTitle+6�   �� 5   
   $
��Left=�6   $��32��6-   $��217,186,0,0q�69   $
 Wrap=�6N   $ acteurs��6b   $	��Top=�6�   $	��340��6�   
���������6�   �� *
TextActors+6�   �� 5   
   $
��Left=�6   $��98��6-   $��217,223,0,0q�69   $
 Wrap=�6N   $ omschrijving��6b   $	��Top=�6�   $	��340��6�   
���������6�   �� *TextDescription+6�   �� 5   
   $
��Left=�6   $��32��6-   $��217,326,0,0q�69   $
 Wrap=�6N   $ director��6b   $	��Top=�6�   $	��340��6�   
���������6�   �� *TextDirector+6�   �� 5   
   $
��Left=�6   $��32��6-   $��217,363,0,0q�69   $
 Wrap=�6N   $ genre��6b   $	��Top=�6�   $	��340��6�   
���������6�   �� *	TextGenre+6�   �� 5   
   $
��Left=�6   $��32��6-   $��217,400,0,0q�69   $
 Wrap=�6N   $ production company��6b   $	��Top=�6�   $	��340��6�   
���������6�   �� *textProduction_Company+6�   �� 5   
   $��Voorspelling Checken��6   $
��Left=�60   $��217,437,0,0q�6K   $	��Top=�6`   $	��340��6x   $��32��6�   $��CheckSentiment��6�   
���`���
���E��
���������6�   �� 6�   �� 5   
   $
��Left=�6+   $��217,479,0,0q�6F   $	��Top=�6[   $��25��6s   $	��340��6   
���������6   �� *Result+6�   �� 5   
   $��Alles Leeg Maken��6   $
��Left=�6,   $��217,517,0,0q�6H   $	��Top=�6]   $	��340��6u   $��Clear��6�   $��30��6�   
���`���
���E��
���������6�   �� 6�   �� 5   
   $��Model Opslaan��6   $
��Left=�6)   $��
384,40,0,0q�6D   $	��Top=�6X   $	��327��6p   $��	SaveModel��6�   $��35��6�   
���`���
���E��
���������6|   �� 6�   �� 5   
   $��Ga Terug��6   $
��Left=�6$   $��217,552,0,0q�6?   $	��Top=�6T   $	��340��6l   $��30��6�   
���`���
���E��
���������6x   �� *CloseWindowCommand+ �� Mode$ OneWay=���6�   ��  �� ElementName$ WriterWindow��6!  5      5      