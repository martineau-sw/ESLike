using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ESLike.Entity
{
    [CreateAssetMenu(fileName = "EntityAttributes", menuName = "ScriptableObjects/Entity/Attributes", order = 1)]
    public class EntityAttributes : ScriptableObject
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
            set => _strength = EntityUtility.Clamp(value, ATTRIBUTE_CAP);
        } 
        public int Constitution 
        {
            get => _constitution;
            set => _constitution = EntityUtility.Clamp(value, ATTRIBUTE_CAP);
        } 
        public int Dexterity 
        {
            get => _dexterity;
            set => _dexterity = EntityUtility.Clamp(value, ATTRIBUTE_CAP);
        } 
        public int Intelligence 
        {
            get => _intelligence;
            set => _intelligence = EntityUtility.Clamp(value, ATTRIBUTE_CAP); 
        } 
        public int Wisdom 
        {
            get => _wisdom;
            set => _wisdom = EntityUtility.Clamp(value, ATTRIBUTE_CAP); 
        } 
        public int Charisma 
        {
            get => _charisma;
            set => _charisma = EntityUtility.Clamp(value, ATTRIBUTE_CAP); 
        } 
    }
}