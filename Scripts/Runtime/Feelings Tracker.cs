using System.Collections.Generic;
using UnityEngine;
using System;
// using Microsoft.MixedReality.Toolkit.Utilities; 

namespace mnibler.RelationshipChain
{
    // [System.Serializable]
    public class FeelingsTracker
    {
        public Dictionary<string, CharacterNode> _characters;
        // public string[] Ids { get { return _all_ids.ToArray(); } }
        private List<string> _all_ids;
        public FeelingsList SaveData;

        public FeelingsTracker()
        {
            _characters = new Dictionary<string, CharacterNode>();
            _all_ids = new List<string>();
            SaveData = new FeelingsList();
        }

        public void AddCharacter(string character_id)
        {
            if (_all_ids.Contains(character_id))
            {
                throw new Exception(
                    string.Format("Character with ID : {0} already exists in the Feelings Tracker")
                );
            }
            _characters[character_id] = new CharacterNode(_all_ids);
            foreach (string id in _all_ids)
            {
                _characters[id].AddCharacter(character_id);
            }
            _all_ids.Add(character_id);

            SaveData.Add(character_id, _characters[character_id]);
        }

        public void RemoveCharacter(string character_id)
        {

        }

        public int GetFeelingsForPlayer(string character_id)
        {
            CharacterNode character = GetCharacter(character_id);
            return character.GetFeelingsForPlayer();
        }

        public void SetFeelingForPlayer(string character_id, int feeling)
        {
            CharacterNode character = GetCharacter(character_id);
            character.SetFeelingsForPlayer(feeling);
        }

        public void AdjustFeelingForPlayer(string character_id, int difference)
        {
            CharacterNode character = GetCharacter(character_id);
            int newFeeling = character.GetFeelingsForPlayer();
            newFeeling += difference;
            newFeeling = NormalizeFeeling(newFeeling);
            character.SetFeelingsForPlayer(newFeeling);
        }

        public int GetCharactersFeelingFor(string character_id, string target_id)
        {
            CharacterNode character = GetCharacter(character_id);
            return character.GetFeelingsFor(character_id);
        }

        public void SetCharactersFeelingFor(string character_id, string target_id, int feeling)
        {
            CharacterNode character = GetCharacter(character_id);
            int newFeeling = NormalizeFeeling(feeling);
            character.SetFeelingsFor(target_id, newFeeling);
        }

        public void AdjustCharactersFeelingFor(string character_id, string target_id, int difference)
        {
            CharacterNode character = GetCharacter(character_id);
            int newFeeling = character.GetFeelingsFor(target_id);
            newFeeling += difference;
            newFeeling = NormalizeFeeling(newFeeling);
            character.SetFeelingsFor(target_id, newFeeling);
        }

        public List<string> GetCharactersWithFeelingsFor(string character_id, Comparator comp, int feeling)
        {
            List<string> characters = new List<string>();

            foreach (string id in _all_ids)
            {
                CharacterNode character = GetCharacter(id);
                if (Compare(comp, character.GetFeelingsFor(character_id), feeling))
                {
                    characters.Add(id);
                }
            }
            return characters;
        }

        private CharacterNode GetCharacter(string character_id)
        {
            if (!_all_ids.Contains(character_id)) 
            {
                throw new Exception(
                    string.Format("Character with ID : {0} not found in the Feelings Tracker")
                );
            }

            return _characters[character_id];
        }
        
        private int NormalizeFeeling(int feeling)
        {
            if (feeling < -100) return -100;
            if (feeling > 100) return 100;
            return feeling;
        }

        private bool Compare(Comparator comp, int x, int y)
        {
            switch(comp)
            {
                case Comparator.GT:
                    return x > y;
                case Comparator.GTE:
                    return x >= y;
                case Comparator.EQ:
                    return x == y;
                case Comparator.LT:
                    return x < y;
                case Comparator.LTE:
                    return x <= y;
                default:
                    return false;
            }
        }

        public string Save()
        {
            return JsonUtility.ToJson(this);
        }

    }


    [System.Serializable]
    public class FeelingsList
    {
        public List<FeelingsData> CharacterFeelings;
        public FeelingsList()
        {
            CharacterFeelings = new List<FeelingsData>();
        }

        public void Add(string id, CharacterNode cn)
        {
            FeelingsData fd = new FeelingsData(id, cn);
            CharacterFeelings.Add(fd);
        }
    }

    [System.Serializable]
    public class FeelingsData
    {
        public string Character_id;
        public CharacterNode Character;
        public FeelingsData(string id, CharacterNode cn)
        {
            Character_id = id;
            Character = cn;
        }
    }
}