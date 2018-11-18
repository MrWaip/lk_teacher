using System;
using System.Windows;
using System.Windows.Controls;

namespace LK_Teacher.Modules
{
    public interface IEventItem
    {
        Button btAction { get; }

        TextBlock tblTitle { get; }

        //Свойства -----------------------------------

        //Id события
        int IdEvent { get; set; }

        //Тип события
        int TypeOfEvent { get; set; }

        //Статус события
        bool StatusEvent { get; set; }

        //День события
        DateTime DayOfEvent { get; set; }

        //Номер строки при списке
        int Row { get; set; }

        //Относительное позиционирование начиная от (1,1) для сетки
        int RelativeRow { get; set; }
        int RelativeCol { get; set; }

        //Абсолютное позиционирование начиная с (0,0) для сетки
        int AbsoluteRow { get; set; }
        int AbsoluteCol { get; set; }

        //Номер (пары, мероприятия или события) - счет идет от 1
        int NumberClass { get; }

        //Установлено-ли событие
        bool IsSetEvent { get; }

        //Метод инициализации
        void Initialize();

        //Возваращает название дня недели на русском
        string GetNameDay();

        //Возвращает название сеобытия
        string GetTypeEvent();

        //
        object TryFindResource(object resourceKey);

        void PlusButtonClick(object sender, RoutedEventArgs e);

        void EventButtonClick(object sender, RoutedEventArgs e);
    }
}
