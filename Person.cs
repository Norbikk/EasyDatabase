using System;

namespace Dictionary
{
    /// <summary>
    /// Класс персон, что имеет все данные, которые будут выводиться
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        public DateTime CurrentDate { get; }
        private readonly string _name;
        private readonly int _age;
        private readonly int _height;
        private readonly string _birthday;
        private readonly string _placeOfBirth;


        /// <summary>
        /// Конструктор класса персон
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentDate"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="birthday"></param>
        /// <param name="placeofBirth"></param>
        public Person(int id,DateTime currentDate, string name, int age, int height, string birthday, string placeofBirth)
        {
            Id = id;
            _name = name;
            _age = age;
            _height = height;
            _birthday = birthday;
            _placeOfBirth = placeofBirth;
            CurrentDate = currentDate;
        }

        /// <summary>
        /// Возвращает строку с информацией о созданном персоне
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{Id}#{CurrentDate}#{_name}#{_age}#{_height}#{_birthday}#{_placeOfBirth}";
    }
}