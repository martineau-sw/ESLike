using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ESLike.Actor
{
    [CreateAssetMenu(fileName = "ActorAttributes", menuName = "ScriptableObjects/Actor/Attributes", order = 1)]
    public class ActorAttributes : ScriptableObject
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
            set => _strength = ActorUtility.Clamp(value, ATTRIBUTE_CAP);
        } 
        public int Constitution 
        {
            get => _constitution;
            set => _constitution = ActorUtility.Clamp(value, ATTRIBUTE_CAP);
        } 
        public int Dexterity 
        {
            get => _dexterity;
            set => _dexterity = ActorUtility.Clamp(value, ATTRIBUTE_CAP);
        } 
        public int Intelligence 
        {
            get => _intelligence;
            set => _intelligence = ActorUtility.Clamp(value, ATTRIBUTE_CAP); 
        } 
        public int Wisdom 
        {
            get => _wisdom;
            set => _wisdom = ActorUtility.Clamp(value, ATTRIBUTE_CAP); 
        } 
        public int Charisma 
        {
            get => _charisma;
            set => _charisma = ActorUtility.Clamp(value, ATTRIBUTE_CAP); 
        } 
    }
}