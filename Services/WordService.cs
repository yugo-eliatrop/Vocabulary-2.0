using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Vocabulary
{
    public enum TaskScope {
        All,
        Learned,
        NotLearned
    }
    public class WordService : IWordService
    {
        private Context db;
        private int count;
        private Dictionary<int, int> scheme;
        
        public WordService(Context context)
        {
            db = context;
            count = db.Words.Count();
            scheme = new Dictionary<int, int>();
            scheme[0] = 5;
            scheme[1] = 4;
            scheme[2] = 3;
            scheme[3] = 2;
            scheme[4] = 1;
            scheme[5] = 5;
        }

        public Word Add(Word word)
        {
            db.Add(word);
            db.SaveChanges();
            return word;
        }

        public void ChangePoints(int id, bool inc)
        {
            Word word = Find(id);
            if (inc)
                word.UpPoints();
            else
                word.DownPoints();
            word.UpdatedAt = DateTime.UtcNow;
            db.SaveChanges();
        }

        public void Destroy(int id)
        {
            Word word = Find(id);
            if (word != null) {
                db.Words.Remove(word);
                db.SaveChanges();
            }
        }

        public Word Find(int id) => db.Words.Find(id);

        public Word First() => db.Words.First();

        public List<Word> GetAll() =>
            db.Words.OrderBy(w => w.Points).ThenBy(w => w.Id).ToList();

        public List<Word> GetAllPage(int page = 1) =>
            db.Words.OrderBy(w => w.Points).ThenBy(w => w.Id).Skip(10 * (page - 1)).Take(10).ToList();

        public List<Word> GetTask(TaskScope scope)
        {
            var result = new List<Word>(25);
            int rem = 0;
            
            for (int i = 0; i <= 5; ++i)
            {
                List<Word> list = db.Words
                    .Where(x => x.Points == i && (scope == TaskScope.All || (x.IsLearned == (scope == TaskScope.Learned))))
                    .OrderBy(w => w.UpdatedAt)
                    .Take(scheme[i] + rem)
                    .ToList();
                rem += scheme[i] - list.Count();
                foreach (Word w in list)
                    result.Add(w);
            }
            return result;
        }

        public Word Next()
        {
            return First();
        }

        public void Update(Word word)
        {
            db.Words.Update(word);
            db.SaveChanges();
        }

        public int Count() => db.Words.Count();

        public WordStatistic Statistic()
        {
            int[] stat = new int[6];
            for (int i = 0; i < 6; ++i)
                stat[i] = db.Words.Where(x => x.Points == i).Count();
            return new WordStatistic(stat);
        }
    }
}
