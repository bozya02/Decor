   M S B A M L   `   `   `  ????@  <Decor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Decor.PagesDecor.Pages  5      #    Decor.Pages.AuthorizationPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Decor.Pages     5        N? Title$  Авторизация??5	      .??5      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      ?? 5      $
??auto ?6    ? ColumnDefinitions 5   
   ?? 5      ?? 5      .+?5   
   ?? $??
Логин??6   ?? 5   
   -   5         ??Name$ tbLogin??5      $??1??6#   ?? 5   
   $??Пароль??6   $??1??6!   G? 5   
   -   5         G?Name$ 
pbPassword??5      $??1??6*   $??1??67   ?? 5   
   -   5         ??Name$ btnLogin??5      $??
Войти??6#   $??2??63   $??2??6@   6T   ?? 5   
   -   5        $ btnGuest??5      $"??Войти как гость??6#   $??2??6=   $??Right=?6J   $??2??6f   6z   5      5       