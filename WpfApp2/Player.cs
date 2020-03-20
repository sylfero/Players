using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Player
    {
        private string _firstName;
        private string _secondName;
        private int _age;
        private int _weight;

        public Player(string firstName, string secondName, int age, int weight)
        {
            _firstName = firstName;
            _secondName = secondName;
            _age = age;
            _weight = weight;
        }

        public override string ToString()
        {
            return $"Imię: {_firstName}  Nazwisko: {_secondName}  Wiek: {_age}  Waga: {_weight}";
        }


        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        public string SecondName
        {
            get => _secondName;
            set => _secondName = value;
        }
        public int Age
        {
            get => _age;
            set => _age = value;
        }
        public int Weight
        {
            get => _weight;
            set => _weight = value;
        }
    }
}
