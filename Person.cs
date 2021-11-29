using System;

namespace Dictionary
{
    /// <summary>
    /// Класс персон, что имеет все данные, которые будут выводиться
    /// </summary>
    public class Person
    {
        private readonly string _name;
        private readonly int _age;
        private readonly int _height;
        private readonly string _birthday;
        private readonly string _placeOfBirth;
        private readonly DateTime _currentDate;

        /// <summary>
        /// Конструктор класса персон
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="birthday"></param>
        /// <param name="placeofBirth"></param>
        public Person(DateTime currentDate, string name, int age, int height, string birthday, string placeofBirth)
        {
            _name = name;
            _age = age;
            _height = height;
            _birthday = birthday;
            _placeOfBirth = placeofBirth;
            _currentDate = currentDate;
        }

        /// <summary>
        /// Возвращает строку с информацией о созданном персоне
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"Дата добавления: {_currentDate} Имя: {_name} Возраст: {_age} Рост: {_height} Дата рождения: {_birthday} Город: {_placeOfBirth}";
    }
}