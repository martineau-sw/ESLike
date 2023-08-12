
using UnityEngine;

namespace ESLike.Actor.Skills
{
    public static class SkillParser 
    {

        public static int Evaluate(Attributes actorAttributes, string expression)
        {
            // Replace attribute tags in string with actor values
            string[] attributes = {"str", "con", "dex", "int", "wis", "cha"};
            string before = expression;

            foreach(string attribute in attributes)
            {
                if(!expression.Contains(attribute)) continue;
                int actorAttributeValue = GetAttributeFromTag(actorAttributes, attribute);
                expression = expression.Replace(attribute, actorAttributeValue.ToString());
            }

            // Evaluate clean expression
            ExpressionEvaluator.Evaluate(expression, out int result);
            Debug.Log($"{before} => {expression} = {result}");

            return result;
        }

        private static int GetAttributeFromTag(Attributes attributes, string attributeTag) 
        {
            switch(attributeTag)
            {
                case "str":
                    return attributes.Strength;
                case "con":
                    return attributes.Constitution;
                case "dex":
                    return attributes.Dexterity;
                case "int":
                    return attributes.Intelligence;
                case "wis":
                    return attributes.Wisdom;
                case "cha":
                    return attributes.Charisma;
            }

            return 0;
        }
    }
}