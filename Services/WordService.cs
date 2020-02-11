using System;
using System.Linq;
using System.Collections.Generic;

namespace Vocabulary
{
    public class WordService : IWordService
    {
        private Context db;
        private int count;
        public WordService(Context context)
        {
            db = context;
            count = db.Words.Count();
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

        public List<Word> GetWords(int count = 20)
        {
            if (count < 20)
                throw new ArgumentException("Count must be 20 and more");
            int newPart = (int)(count * 0.67);
            List<Word> list1 = db.Words.OrderBy(w => w.UpdatedAt).Take(count - newPart).ToList();
            List<Word> list2 = db.Words.OrderBy(w => w.Points).Take(newPart).ToList();
            return list1.Union(list2, new WordComparer()).ToList();
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
