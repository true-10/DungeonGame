using UnityEngine;

namespace DungeonGame
{

    [CreateAssetMenu(fileName = "CharacterStaticData", menuName = GlobalConstants.UIPath.ProjectFolder + "CharacterStaticData")]
    public class CharacterStaticDataScriptableObject : ScriptableObject
    {
        [SerializeField]
        private CharacterStaticData character;

        public CharacterStaticData GetCharacter()
        {
            return character;
        }

    }
}
