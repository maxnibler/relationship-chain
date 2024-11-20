using System.Collections.Generic;
using UnityEngine;

namespace mnibler.RelationshipChain
{
    [System.Serializable]
    public class CharacterNode
    {
        private Dictionary<string, CharacterFeelings> _interCharacterFeelings;
        private int _feelingForPlayer;
        public int FeelingsForPlayer;
        public List<CharacterFeelings> FeelingsList;

        public CharacterNode(List<string> init_characters)
        {
            _interCharacterFeelings = new Dictionary<string, CharacterFeelings>();
            FeelingsList = new List<CharacterFeelings>();
            _feelingForPlayer = 0;
            foreach (string id in init_characters)
            {
                AddCharacter(id);
            }
        }

        public void AddCharacter(string character_id)
        {
            CharacterFeelings cf = new CharacterFeelings(character_id, 0);
            // _interCharacterFeelings[character_id] = 0;
            _interCharacterFeelings[character_id] = cf;
            FeelingsList.Add(cf);
        }

        public void RemoveCharacter(string character_id)
        {

        }

        public int GetFeelingsFor(string character_id)
        {
            // return 0;
            return _interCharacterFeelings[character_id].Feeling;
        }

        public void SetFeelingsFor(string character_id, int feelings)
        {
            _interCharacterFeelings[character_id].Feeling = feelings;
        }

        public int GetFeelingsForPlayer()
        {
            return _feelingForPlayer;
        }

        public void SetFeelingsForPlayer(int feelings)
        {
            _feelingForPlayer = feelings;
            FeelingsForPlayer = feelings;
        }
    }

    [System.Serializable]
    public class CharacterFeelings
    {
        public string Character_id;
        public int Feeling;
        public CharacterFeelings(string id, int f)
        {
            Character_id = id;
            Feeling = f;
        }
    }
}