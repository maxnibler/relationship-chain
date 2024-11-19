using System.Collections.Generic;
using UnityEngine;

namespace mnibler.RelationshipChain
{
    public class CharacterNode
    {
        private Dictionary<string, int> _interCharacterFeelings;
        private int _feelingForPlayer;

        public CharacterNode(List<string> init_characters)
        {
            _interCharacterFeelings = new Dictionary<string, int>();
            _feelingForPlayer = 0;
            foreach (string id in init_characters)
            {
                _interCharacterFeelings[id] = 0;
            }
        }

        public void AddCharacter(string character_id)
        {
            _interCharacterFeelings[character_id] = 0;
        }

        public void RemoveCharacter(string character_id)
        {

        }

        public int GetFeelingsFor(string character_id)
        {
            // return 0;
            return _interCharacterFeelings[character_id];
        }

        public void SetFeelingsFor(string character_id, int feelings)
        {
            _interCharacterFeelings[character_id] = feelings;
        }

        public int GetFeelingsForPlayer()
        {
            return _feelingForPlayer;
        }

        public void SetFeelingsForPlayer(int feelings)
        {
            _feelingForPlayer = feelings;
        }
    }
}