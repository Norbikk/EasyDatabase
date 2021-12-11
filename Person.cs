using System;

namespace Dictionary
{
    /// <summary>
    /// Класс персон, что имеет все данные, которые будут выводиться
    /// </summary>
    public class Person
    {
        public int Id;
        public DateTime CurrentDate;
        private readonly string _name;
        private readonly int _age;
        private readonly int _height;
        private readonly string _birthday;
        private readonly string _placeOfBirth;


        /// <summary>
        /// Конструктор класса персон
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="currentDate">Текущая дата</param>
        /// <param name="name">Имя</param>
        /// <param name="age">Возраст</param>
        /// <param name="height">Рост</param>
        /// <param name="birthday">Дата рождения</param>
        /// <param name="placeofBirth">Место рождения</param>
        /// <param name="data">PersonData</param>
        public Person(PersonData data)
        {
            Id = data.Id;
            _name = data.Name;
            _age = data.Age;
            _height = data.Height;
            _birthday = data.Birthday;
            _placeOfBirth = data.PlaceOfBirth;
            CurrentDate = data.CurrentDate;
        }

        /// <summary>
        /// Возвращает строку с информацией о созданном персоне
        /// </summary>
        /// <returns>Возвращает данные в строку с разделителем</returns>
        public override string ToString() =>
            $"{Id}#{CurrentDate}#{_name}#{_age}#{_height}#{_birthday}#{_placeOfBirth}";
    }

    /// <summary>
    /// Структура для работы с Person
    /// </summary>
    public struct PersonData
    {
        public int Id;
        public string Name;
        public int Age;
        public int Height;
        public string Birthday;
        public string PlaceOfBirth;
        public DateTime CurrentDate;
    }
}