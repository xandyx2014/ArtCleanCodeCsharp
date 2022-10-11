using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Business
{
    public enum Type
    {
        Elf,
        Ork
    }

    public class Character : INotifyPropertyChanged, IDisposable
    {
        private string _name;
        public Type Type { get; }

        public List<string> Weaponry { get; }

        public Character(Type type)
        {
            Type = type;
            Weaponry = new List<string>();
        }

        public Character(Type type, string name) : this(type)
        {
            Name = name;
        }

        public int Armor
        {
            get
            {
                switch (Type)
                {
                    case Type.Elf:
                        return 60;
                    case Type.Ork:
                        return 100;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public bool IsDead => Health <= 0;

        public double Speed
        {
            get
            {
                switch (Type)
                {
                    case Type.Elf:
                        return 1.7;
                    case Type.Ork:
                        return 1.4;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int Wear { get; private set; } = 15;
        public int Health { get; private set; } = 100;

        public int Defense => Wear >= Armor ? 0 : Armor - Wear;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public void Damage(int damage)
        {
            if (damage > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }
            Health -= damage - Defense;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
        }
    }
}