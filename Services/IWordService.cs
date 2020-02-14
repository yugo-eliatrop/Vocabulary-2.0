using System.Collections.Generic;

namespace Vocabulary
{
    public interface IWordService {
        Word Add(Word word);
        void ChangePoints(int id, bool inc);
        void Destroy(int id);
        Word Find(int id);
        Word First();
        List<Word> GetAll();
        List<Word> GetAllPage(int page);
        List<Word> GetWords();
        Word Next();
        void Update(Word word);
        int Count();
        WordStatistic Statistic();
    }
}