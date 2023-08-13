
using UnityEngine;


namespace ESLike.Actor
{
    [System.Serializable]
    public class Attributes 
    {
        const int ATTRIBUTE_CAP = 50;

        [SerializeField]
        int _strength;
        [SerializeField]
        int _constitution;
        [SerializeField]
        int _dexterity;
        [SerializeField]
        int _intelligence;
        [SerializeField]
        int _wisdom;
        [SerializeField]
        int _charisma;

        public int Strength 
        {
            get => _strength;
            set => _strength = value;
        } 
        public int Constitution 
        {
            get => _constitution;
            set => _constitution = value;
        } 
        public int Dexterity 
        {
            get => _dexterity;
            set => _dexterity = value;
        } 
        public int Intelligence 
        {
            get => _intelligence;
            set => _intelligence = value; 
        } 
        public int Wisdom 
        {
            get => _wisdom;
            set => _wisdom = value; 
        } 
        public int Charisma 
        {
            get => _charisma;
            set => _charisma = value; 
        } 

        public Attributes(int strength, int constitution, int dexterity, int intelligence, int wisdom, int charisma)
        {
            _strength = strength;
            _constitution = constitution;
            _dexterity = dexterity;
            _intelligence = intelligence;
            _wisdom = wisdom;
            _charisma = charisma;
        }

        public Attributes(int start) 
        {
            _strength = start;
            _constitution = start;
            _dexterity = start;
            _intelligence = start;
            _wisdom = start;
            _charisma = start;
        }
    }
}